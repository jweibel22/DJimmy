using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DJimmy.Domain;
using DJimmy.Domain.LastFM;
using DJimmy.Infrastructure.Integrations;
using MoreLinq;
using Raven.Abstractions.Data;
using Raven.Abstractions.Extensions;
using Raven.Client;
using Raven.Client.Document;

namespace DJimmy.Infrastructure.Importers
{
    public class LastFmImporter
    {
        private readonly IDocumentStore documentStore;
        private readonly LastfmPlaybacksFetcher playbacksFetcher;
        private readonly CancellationToken cancellationToken;

        public delegate void BatchDownloadedHandler(IEnumerable<Playback> batch);

        public event BatchDownloadedHandler BatchDownloaded;

        public ProgressStatus Status { get; private set; }

        public LastFmImporter(IDocumentStore documentStore, LastfmPlaybacksFetcher playbacksFetcher, ProgressStatus progressStatus, CancellationToken cancellationToken)
        {
            this.playbacksFetcher = playbacksFetcher;
            this.cancellationToken = cancellationToken;
            this.documentStore = documentStore;
            this.Status = progressStatus;
        }

        public void Import()
        {
            using (var session = documentStore.OpenSession())
            {
                session.Advanced.MaxNumberOfRequestsPerSession = 1000;

                var latestKnownPlayback = session.Query<Playback>().OrderByDescending(pb => pb.Timestamp).FirstOrDefault();

                var from = latestKnownPlayback != null ? latestKnownPlayback.Timestamp : DateTime.MinValue;

                double totalPlaybacks = playbacksFetcher.TotalCount(from);
                double processed = 0;

                var playbacksInBatches = playbacksFetcher.Fetch(from).Batch(500);

                foreach (var playbackBatch in playbacksInBatches)
                {
                    foreach (var playback in playbackBatch)
                    {
                        session.Store(playback);
                        processed++;
                    }

                    session.SaveChanges();

                    if (BatchDownloaded != null)
                    {
                        BatchDownloaded(playbackBatch);
                    }

                    
                    Status.OnProgress((int)(processed / totalPlaybacks * 100.0));

                    if (cancellationToken.IsCancellationRequested)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }
            }
        }
    }
}
