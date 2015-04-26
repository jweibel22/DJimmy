using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain;
using DJimmy.Domain.Library;
using Raven.Client.Indexes;

namespace DJimmy.Infrastructure.Indexes
{
    public class TitleArtistIndex: AbstractIndexCreationTask<Song>
    {
        public class ReduceResult
        {
            public string Title { get; set; }

            public string Artist { get; set; }
        }

        public TitleArtistIndex()
        {
            Map = songs => from song in songs
                select new ReduceResult {Title = song.Title, Artist = song.Artist};
        }
    }
}
