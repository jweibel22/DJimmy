using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain.Spotify;
using Newtonsoft.Json;
using Raven.Client;
using RestSharp;

namespace DJimmy.Infrastructure.Integrations
{
    public interface ISpotifyAuthorizationService
    {
        string GetAuthorizationToken();
        void Authorize(string code);
        bool IsLoggedIn();
    }

    public class SpotifyAuthorizationService : ISpotifyAuthorizationService
    {
        private readonly IDocumentStore documentStore;

        public SpotifyAuthorizationService(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        private static Authorization RequestAccessToken(string code)
        {
            var client = new RestClient("https://accounts.spotify.com/");
            var clientId = "bf6a7ee866724c36871c7436c7940d17";
            var clientSecret = "0ab6ace2abf848858d041c1822befefe";

            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(clientId + ":" + clientSecret);
            string encodedClientCredentials = Convert.ToBase64String(toEncodeAsBytes);
            var request = new RestRequest("api/token", Method.POST);

            request.AddHeader("Authorization", "Basic " + encodedClientCredentials);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", "http:%2F%2Flocalhost:8889%2FAuthorized");

            var response = client.Execute(request).Content;

            var responseAsJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            return new Authorization
            {
                AuthorizationToken = (string)responseAsJson["access_token"],
                Expires = (int)(long)responseAsJson["expires_in"],
                RefreshToken = (string)responseAsJson["refresh_token"],
                Timestamp = DateTime.UtcNow
            };
        }

        public void Authorize(string code)
        {
            var authorizationInfo = RequestAccessToken(code);
            StoreAuthorizationInfo(authorizationInfo);
        }

        public bool IsLoggedIn()
        {
            using (var session = documentStore.OpenSession())
            {
                var authorizationFromDb = session.Query<Authorization>().SingleOrDefault();

                if (authorizationFromDb == null)
                {
                    return false;
                }

                return DateTime.UtcNow < authorizationFromDb.Timestamp.AddSeconds(authorizationFromDb.Expires);
            }
        }

        private void StoreAuthorizationInfo(Authorization authorization)
        {
            using (var session = documentStore.OpenSession())
            {
                var authorizationFromDb = session.Query<Authorization>().SingleOrDefault();

                if (authorizationFromDb != null)
                {
                    authorizationFromDb.CopyFrom(authorization);
                }
                else
                {
                    session.Store(authorization);
                }

                session.SaveChanges();
            }
        }

        public string GetAuthorizationToken()
        {
            using (var session = documentStore.OpenSession())
            {
                var authorizationInfo = session.Query<Authorization>().SingleOrDefault();

                if (authorizationInfo == null)
                {
                    throw new UnauthorizedAccessException("Not logged in");
                }

                return authorizationInfo.AuthorizationToken;
            }
        }
    }
}
