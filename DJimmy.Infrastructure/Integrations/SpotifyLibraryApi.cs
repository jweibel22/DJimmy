using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using DJimmy.Domain;
using DJimmy.Domain.Spotify;
using Newtonsoft.Json;
using RestSharp;

namespace DJimmy.Infrastructure.Integrations
{
    public class SpotifyLibraryApi
    {                
        private readonly string accessToken;

        public SpotifyLibraryApi(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public IEnumerable<SpotifySong> GetLibrary(ProgressStatus progressStatus, CancellationToken cancellationToken)
        {
            const int limit = 50;
            int total;
            GetLibraryPage(0, out total);
            
            for (int i = 0; i < total; i += limit)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                progressStatus.OnProgress(i*100/total);

                foreach (var song in GetLibraryPage(i, out total))
                {
                    yield return song;
                }
            }
        }

        public IEnumerable<SpotifySong> GetLibraryPage(int offset, out int total)
        {
            var client = new RestClient("https://api.spotify.com/");

            var request = new RestRequest("v1/me/tracks?limit=50&offset=" + offset, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddUrlSegment("user_id", "113179990");
            request.AddUrlSegment("playlist_id", "0L1E0WejTOigPcSuPvRs3c");
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You're not logged into Spotify");
            }

            else if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }
            
            dynamic stuff = JsonConvert.DeserializeObject(response.Content);
            total = stuff.total;

            return ParseSongs(response.Content);
        }

        private IEnumerable<SpotifySong> ParseSongs(string response)
        {
            dynamic stuff = JsonConvert.DeserializeObject(response);

            //var uris = new List<Song>();
            foreach (var t in stuff.items)
            {
                string title = t.track.name;
                string artist = t.track.artists[0].name;
                string album = t.track.album.name;
                string url = t.track.uri;

                yield return new SpotifySong
                {
                    Album = album.Trim(),
                    Artist = artist.Trim(),
                    Title = title.Trim(),
                    Url = url
                };
            }

            //return uris;
        }

        public void AddToLibrary(string id)
        {
            var client = new RestClient("https://api.spotify.com/");

            var request = new RestRequest("v1/me/tracks?ids=" + id, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            //request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + accessToken);
            //request.AddUrlSegment("user_id", "113179990");
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You're not logged into Spotify");
            }

            else if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }            
        }
    }
}
