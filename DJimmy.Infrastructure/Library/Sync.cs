using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DJimmy.Domain;
using DJimmy.Domain.Library;
using DJimmy.Domain.LocalFiles;
using DJimmy.Domain.Spotify;
using DJimmy.Infrastructure.Events;
using DJimmy.Infrastructure.Importers;
using DJimmy.Infrastructure.Integrations;
using Raven.Client;

namespace DJimmy.Infrastructure.Library
{
    public class Sync : IDisposable
    {
        private readonly IDocumentStore documentStore;
        private readonly Domain.Library.Library library;
        private readonly ISpotifyAuthorizationService spotifyAuthorizationService;
        private readonly string lastFmUsername;
        private readonly CancellationToken cancellationToken;

        public Sync(IDocumentStore documentStore, Domain.Library.Library library, ISpotifyAuthorizationService spotifyAuthorizationService, string lastFmUsername, CancellationToken cancellationToken)
        {
            this.library = library;
            this.spotifyAuthorizationService = spotifyAuthorizationService;
            this.documentStore = documentStore;
            this.lastFmUsername = lastFmUsername;
            this.cancellationToken = cancellationToken;
            this.Status = new ProgressStatus();
        }

        public ProgressStatus Status { get; private set; }

        public void Run()
        {
            try
            {
                AliasMap aliasMap;

                Status.OnProgress(0, "Loading aliases");

                using (var session = documentStore.OpenSession())
                {
                    var aliases = session.Query<Alias>().ToList();
                    aliasMap = new AliasMap(aliases);
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                Status.OnProgress(5, "Importing songs from spotify");

                var spotifyImporter = new SpotifyImporter(documentStore,
                    spotifyAuthorizationService.GetAuthorizationToken(), Status.SpawnChild(90), cancellationToken);
                spotifyImporter.Import();

                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                Status.OnProgress(95, "Merging new spotify songs into library");

                lock (library)
                {
                    using (var session = documentStore.OpenSession())
                    {
                        var replayer = new EventReplayer<SpotifySongEvent, SpotifySong>(session,
                            new SpotifyEventsIdentifier());
                        var events = replayer.EventsFrom(library.LastSpotifySync);

                        session.Store(library);
                        library.SyncWithSpotify(aliasMap, events);
                        session.SaveChanges();
                    }
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                Status.OnProgress(0, "Importing scrobbles from LastFM");

                var lastFmImporter = new LastFmImporter(documentStore, new LastfmPlaybacksFetcher(lastFmUsername), Status.SpawnChild(90), cancellationToken);
                //lastFmImporter.BatchDownloaded += batch => library.AddPlaybacks(aliasMap, batch);
                //lastFmImporter.Status.Progressed += (sender, args) => this.Status.OnProgress(lastFmImporter.Status.Progress, "Importing scrobbles from LastFM");
                lastFmImporter.Import();

                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                Status.OnProgress(90, "Merging playbacks from LastFM into library");

                lock (library)
                {
                    using (var session = documentStore.OpenSession())
                    {
                        var playbackReplayer = new LastFmEventReplayer(session);
                        var playbacks = playbackReplayer.EventsFrom(library.LatestLastFmSync.ToUniversalTime());

                        session.Store(library);
                        library.AddPlaybacks(aliasMap, playbacks);
                        session.SaveChanges();
                    }
                }

                Status.OnProgress(100, "Done");
            }
            catch (OperationCanceledException)
            {
                
            }
        }

        public void Dispose()
        {
            Status.Dispose();
        }
    }
}
