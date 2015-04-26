using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain;
using DJimmy.Domain.Library;
using Raven.Client.Indexes;

namespace DJimmy.Infrastructure.Indexes
{
    public class SpotifyUrlIndex : AbstractIndexCreationTask<Song, SpotifyUrlIndex.ReduceResult>
    {
        public class ReduceResult
        {
            public string Url { get; set; }
        }

        public SpotifyUrlIndex()
        {
            Map = songs => from song in songs
                           from url in song.SpotifyUrls
                           select new ReduceResult { Url = url };            
        }
    }
}
