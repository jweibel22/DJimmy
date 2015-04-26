using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJimmy.Domain.Spotify
{
    public class SpotifySongEvent : Event
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }        
    }
}
