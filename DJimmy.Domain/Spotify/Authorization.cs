using System;

namespace DJimmy.Domain.Spotify
{
    public class Authorization
    {
        public string AuthorizationToken { get; set; }

        public string RefreshToken { get; set; }

        public int Expires { get; set; }

        public DateTime Timestamp { get; set; }

        public void CopyFrom(Authorization other)
        {
            AuthorizationToken = other.AuthorizationToken;
            RefreshToken = other.RefreshToken;
            Expires = other.Expires;
            Timestamp = other.Timestamp;
        }
    }
}
