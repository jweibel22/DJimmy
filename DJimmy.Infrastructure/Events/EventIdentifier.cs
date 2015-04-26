using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain;
using DJimmy.Domain.LocalFiles;
using DJimmy.Domain.Spotify;

namespace DJimmy.Infrastructure.Events
{
    public interface IIdentifyEvents<in T> where T : Event
    {
        string IdentifierOfEvent(T e);
    }

    public class SpotifyEventsIdentifier : IIdentifyEvents<SpotifySongEvent>
    {
        public string IdentifierOfEvent(SpotifySongEvent e)
        {
            return e.Url;
        }
    }

    public class LocalFileEventsIdentifier : IIdentifyEvents<LocalFileEvent>
    {
        public string IdentifierOfEvent(LocalFileEvent e)
        {
            return e.Path;
        }
    }
}
