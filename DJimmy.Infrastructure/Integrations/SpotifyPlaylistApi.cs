using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DJimmy.Domain.Library;
using Newtonsoft.Json;
using RestSharp;

namespace DJimmy.Infrastructure.Integrations
{
    public class SpotifyPlaylistApi
    {
        private readonly string accessToken;

        public SpotifyPlaylistApi(string accessToken)
        {
            this.accessToken = accessToken;
        }

        private RestRequest BuildRequest()
        {
            var request = new RestRequest("v1/users/{user_id}/playlists/{playlist_id}/tracks", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddUrlSegment("user_id", "113179990");
            request.AddUrlSegment("playlist_id", "0L1E0WejTOigPcSuPvRs3c");

            return request;
        }

        private IRestResponse Execute(RestRequest request)
        {
            var client = new RestClient("https://api.spotify.com/");
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You're not logged into Spotify");
            }

            if (response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }

            return response;
        }

        public void AddTracks(IList<Song> tracks)
        {            
            var request = BuildRequest();
            request.Method = Method.POST;

            var urls = tracks.Where(t => t.SpotifyUrls.Any(u => !string.IsNullOrEmpty(u)))
                             .Select(t => t.SpotifyUrls.First(u => !string.IsNullOrEmpty(u))).ToList();

            var urlsAsJson = JsonConvert.SerializeObject(urls);
            request.AddParameter("application/json", urlsAsJson, ParameterType.RequestBody);

            Execute(request);
        }

        private IEnumerable<string> ParseUris(string response)
        {
            dynamic stuff = JsonConvert.DeserializeObject(response);

            var uris = new List<string>();
            foreach (var t in stuff.items)
            {
                uris.Add((string)t.track.uri);
            }

            return uris;
        }

        public void Clear()
        {
            var request = BuildRequest();
            request.Method = Method.GET;
            var response = Execute(request);

            var uris = ParseUris(response.Content).Select(t => new { uri = t }).Cast<object>().ToList();
            var urlsAsJson = JsonConvert.SerializeObject(new { tracks = uris });

            var deleteRequest = BuildRequest();
            deleteRequest.Method = Method.DELETE;

            deleteRequest.AddParameter("application/json", urlsAsJson, ParameterType.RequestBody);
            Execute(deleteRequest);
        }
    }
}
