using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain.Spotify;
using Newtonsoft.Json;
using RestSharp;

namespace DJimmy.Infrastructure.Integrations
{
    public class SpotifySearchApi
    {
        //private readonly string accessToken;

        //public SpotifySearchApi(string accessToken)
        //{
        //    this.accessToken = accessToken;
        //}
        
        public IEnumerable<SpotifySong> Search(string artist, string title)
        {
            var client = new RestClient("https://api.spotify.com/");

            var request = new RestRequest(String.Format("v1/search?q=artist:{0}+track:{1}&type=track", artist, title), Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + accessToken);
            //request.AddUrlSegment("user_id", "113179990");
            //request.AddUrlSegment("playlist_id", "0L1E0WejTOigPcSuPvRs3c");
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You're not logged into Spotify");
            }

            else if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }

            return ParseSongs(response.Content);
        }


        private IEnumerable<SpotifySong> ParseSongs(string response)
        {
            dynamic stuff = JsonConvert.DeserializeObject(response);

            //var uris = new List<Song>();
            foreach (var t in stuff.tracks.items)
            {
                string title = t.name;
                string artist = t.artists[0].name;
                string album = t.album.name;
                string url = t.uri;

                yield return new SpotifySong
                {
                    Album = album,
                    Artist = artist,
                    Title = title,
                    Url = url
                };
            }
        }
    }
}
