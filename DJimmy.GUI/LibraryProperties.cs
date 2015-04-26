using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;

namespace DJimmy.GUI
{
    public class LibraryProperties
    {
        public string Id { get; set; }

        public string LastFmUsername { get; set; }

        public int AutoSyncInterval { get; set; }
    }
}
