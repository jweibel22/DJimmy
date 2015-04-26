using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain.LocalFiles;
using Microsoft.SqlServer.Server;

namespace DJimmy.Domain.Library
{
    public class File
    {
        public string Path { get; set; }

        public FileType FileType { get; set; }

        public static File New(string path, FileType fileType)
        {
            return new File
            {
                FileType = fileType,
                Path = path
            };
        }
    }
}
