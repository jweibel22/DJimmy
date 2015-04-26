using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain.LastFM;
using DJimmy.Domain.Library;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace DJimmy.Infrastructure.Indexes
{
    public class PlaybacksIndex : AbstractIndexCreationTask<Playback>
    {
        public class ReduceResult
        {
            public string Title { get; set; }

            public string Artist { get; set; }

            public DateTime Timestamp { get; set; }
        }

        public PlaybacksIndex()
        {
            Map = playbacks => from playback in playbacks
                               select new ReduceResult
                               {
                                   Artist = playback.Artist,
                                   Title = playback.Title,
                                   Timestamp = playback.Timestamp
                               };
        }
    }
}
