using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using DJimmy.Domain.LastFM;

namespace DJimmy.Infrastructure.Integrations
{
    public interface ILastfmPlaybacksFetcher
    {
        IEnumerable<Playback> Fetch(DateTime from);
    }

    public class LastfmPlaybacksFetcher : ILastfmPlaybacksFetcher
    {
        private readonly string username;
        private readonly HttpClient httpClient = new HttpClient();

        private const string ApiKey = "73a8121d8ac53e94f73885c6486481ba";

        public LastfmPlaybacksFetcher(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ApplicationException("No last.FM username has been specified");
            }

            this.username = username;
        }

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        static Guid? ToGuid(string guid)
        {
            return String.IsNullOrEmpty(guid) ? new Nullable<Guid>() : Guid.Parse(guid);
        }

        private IEnumerable<Playback> ReadPage(string pageXml)
        {
            var xml = new XmlDocument();
            xml.LoadXml(pageXml);

            return from XmlNode node in xml.SelectNodes("lfm/recenttracks/track")
                let artistNode = node.SelectSingleNode("artist")
                let albumNode = node.SelectSingleNode("album")                
                let titleNode = node.SelectSingleNode("name")                
                let timestampNode = node.SelectSingleNode("date")
                   //let artistId = ToGuid(artistNode.Attributes["mbid"].InnerText)
                   //let albumId = ToGuid(albumNode.Attributes["mbid"].InnerText)
                   //let trackId = ToGuid(node.SelectSingleNode("mbid").InnerText)
                where timestampNode != null
                let timestamp = ConvertFromUnixTimestamp(long.Parse(timestampNode.Attributes["uts"].InnerText))
                orderby timestamp
                select new Playback
                {
                    Album = albumNode.InnerText.Trim(),
                    Artist = artistNode.InnerText.Trim(),
                    Title = titleNode.InnerText.Trim(),
                    Timestamp = timestamp
                };
        }

        private string ConstructUrl(int pageIdx, DateTime from)
        {
            var lastUpdated = ConvertToUnixTimestamp(from);
            return String.Format("http://ws.audioscrobbler.com/2.0/?method=user.getrecenttracks&user={0}&api_key={3}&from={1}&page={2}&limit=200",
                username, lastUpdated, pageIdx, ApiKey);
        }

        public int TotalCount(DateTime from)
        {
            var response = httpClient.GetAsync(ConstructUrl(1, from));

            var msg = response.Result.Content.ReadAsStringAsync().Result;
            var xml = new XmlDocument();
            xml.LoadXml(msg);

            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                var errorNode = xml.SelectSingleNode("lfm/error");
                var errorText = errorNode != null ? errorNode.InnerText : "Unknown connection error";
                throw new ApplicationException(errorText);
            }

            return Int32.Parse(xml.SelectSingleNode("lfm/recenttracks").Attributes["total"].InnerText);            
        }

        private int GetPageCount(DateTime from)
        {
            var response = httpClient.GetAsync(ConstructUrl(1, from));

            var msg = response.Result.Content.ReadAsStringAsync().Result;
            var xml = new XmlDocument();
            xml.LoadXml(msg);

            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                var errorNode = xml.SelectSingleNode("lfm/error");
                var errorText = errorNode != null ? errorNode.InnerText : "Unknown connection error";
                throw new ApplicationException(errorText);
            }

            return Int32.Parse(xml.SelectSingleNode("lfm/recenttracks").Attributes["totalPages"].InnerText);
        }


        static int ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (int)Math.Floor(diff.TotalSeconds);
        }

        public IEnumerable<string> Read()
        {
            return Read(new DateTime(1970, 1, 1, 0, 0, 0, 0));
        }

        private IEnumerable<string> Read(DateTime from)
        {
            var pageCount = GetPageCount(from);

            for (int pageIdx = pageCount; pageIdx > 0; pageIdx--)
            {
                Task<HttpResponseMessage> response = null;
                try
                {
                    response = httpClient.GetAsync(ConstructUrl(pageIdx, from));
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Loading of Page {0} failed", pageIdx), ex);
                }

                if (response != null)
                {
                    yield return response.Result.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public IEnumerable<Playback> Fetch(DateTime from)
        {
            return Read(@from).SelectMany(xml => ReadPage(xml));
        }
    }
}
