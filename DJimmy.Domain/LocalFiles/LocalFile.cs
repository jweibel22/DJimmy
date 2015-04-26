using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJimmy.Domain.LocalFiles
{
    public class LocalFile
    {
        public string Path { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public FileType FileType { get; set; }
    }

    public enum FileType
    {
        Unknown, Mp3, Flac
    }
}
