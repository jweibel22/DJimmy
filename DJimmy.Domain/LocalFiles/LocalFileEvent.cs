using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJimmy.Domain.LocalFiles
{
    public class LocalFileEvent : Event
    {
        public string Path { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public FileType FileType { get; set; }
    }
}
