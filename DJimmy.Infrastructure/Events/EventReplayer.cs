using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain;
using Raven.Client;

namespace DJimmy.Infrastructure.Events
{
    public class EventReplayer<TEvent, TItem> where TEvent : Event
    {                
        private readonly IDocumentSession session;
        private readonly IIdentifyEvents<TEvent> eventsIdentifier;
        private static readonly int Limit = 50000;

        public EventReplayer(IDocumentSession session, IIdentifyEvents<TEvent> eventsIdentifier)
        {
            this.session = session;
            this.eventsIdentifier = eventsIdentifier;
        }

        private void Replay(IDictionary<string, TItem> songs, IEnumerable<TEvent> events)
        {
            foreach (var e in events)
            {
                switch (e.EventType)
                {
                    case EventType.Added:
                        songs.Add(eventsIdentifier.IdentifierOfEvent(e), AutoMapper.Mapper.Map<TItem>(e));
                        break;
                    case EventType.Removed:
                        songs.Remove(eventsIdentifier.IdentifierOfEvent(e));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public IEnumerable<TEvent> EventsFrom(DateTime from)
        {
            var events = session.Query<TEvent>()
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

        public IEnumerable<TItem> ReplayUntil(DateTime timestamp)
        {
            IDictionary<string, TItem> songs = new Dictionary<string, TItem>();
            var events = session.Query<TEvent>().Where(e => e.Timestamp < timestamp).Take(Limit).ToList().OrderBy(e => e.Timestamp);

            if (events.Count() == Limit)
            {
                throw new Exception("limit reached");
            }

            Replay(songs, events);

            return songs.Values;
        }

        public IEnumerable<TItem> ReplayAll()
        {
            IDictionary<string, TItem> songs = new Dictionary<string, TItem>();
            var events = session.Query<TEvent>().Take(Limit).ToList().OrderBy(e => e.Timestamp);

            if (events.Count() == Limit)
            {
                throw new Exception("limit reached");
            }

            Replay(songs, events);

            return songs.Values;
        }

    }
}
