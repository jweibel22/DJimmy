using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using DJimmy.Domain;
using DJimmy.Domain.Spotify;
using DJimmy.Infrastructure.Events;
using DJimmy.Infrastructure.Integrations;
using MoreLinq;
using Raven.Client;

namespace DJimmy.Infrastructure.Importers
{
    public class SpotifyImporter
    {
        private readonly IDocumentStore documentStore;
        private static readonly int Limit = 10000;
        private readonly string authorizationToken;
        private readonly ProgressStatus progressStatus;
        private readonly CancellationToken cancellationToken;

        public SpotifyImporter(IDocumentStore documentStore, string authorizationToken, ProgressStatus progressStatus, CancellationToken cancellationToken)
        {
            this.authorizationToken = authorizationToken;
            this.progressStatus = progressStatus;
            this.cancellationToken = cancellationToken;
            this.documentStore = documentStore;
        }

        public void Import()
        {
            using (var session = documentStore.OpenSession())
            {
                var spotifySongEventReplayer = new EventReplayer<SpotifySongEvent, SpotifySong>(session, new SpotifyEventsIdentifier());
                var songs = spotifySongEventReplayer.ReplayAll();

                var api = new SpotifyLibraryApi(authorizationToken);
                var fromSpotify = api.GetLibrary(progressStatus, cancellationToken).ToList();
                var now = DateTime.Now;

                fromSpotify.ExceptBy(songs, s => s.Url)
                    .Select(s =>
                    {
                        var e = Mapper.Map<SpotifySongEvent>(s);
                        e.Timestamp = now;
                        e.EventType = EventType.Added;
                        return e;
                    }).ForEach(e => session.Store(e));

                songs.ExceptBy(fromSpotify, s => s.Url)
                    .Select(s =>
                    {
                        var e = new SpotifySongEvent();
                        e.Url = s.Url;
                        e.Timestamp = now;
                        e.EventType = EventType.Removed;
                        return e;
                    }).ForEach(e => session.Store(e));


                session.SaveChanges();
            }
        }
    }
}
