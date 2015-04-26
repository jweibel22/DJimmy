using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJimmy.Domain.LastFM
{
    public class Playback
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
