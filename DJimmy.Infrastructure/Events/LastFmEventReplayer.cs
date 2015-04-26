using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain.LastFM;
using DJimmy.Domain.Spotify;
using Raven.Client;

namespace DJimmy.Infrastructure.Events
{
    public class LastFmEventReplayer
    {
        private readonly IDocumentSession session;
        private static readonly int Limit = 80000;

        public LastFmEventReplayer(IDocumentSession session)
        {
            this.session = session;
        }

        public IEnumerable<Playback> EventsFrom(DateTime from)
        {
            var events = session.Query<Playback>()
                .Where(e => e.Timestamp > from)                
                .Take(Limit)
                .ToList()
                .OrderBy(e => e.Timestamp);

            if (events.Count() == Limit)
            {
                throw new Exception("limit reached");
            }

            return events;
        }
    }
}
