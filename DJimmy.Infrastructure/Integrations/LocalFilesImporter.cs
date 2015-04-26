using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DJimmy.Domain.LocalFiles;
using log4net;
using TagLib;

namespace DJimmy.Infrastructure.Integrations
{
    public class LocalFilesImporter
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        class SimpleFileAbstraction : TagLib.File.IFileAbstraction
        {
            private readonly string name;
            private readonly Stream stream;

            public SimpleFileAbstraction(Stream stream, string name)
            {
                this.stream = stream;
                this.name = name;
            }


            public string Name
            {
                get { return name; }
            }

            public System.IO.Stream ReadStream
            {
                get { return stream; }
            }

            public System.IO.Stream WriteStream
            {
                get { return stream; }
            }

            public void CloseStream(System.IO.Stream stream)
            {
                stream.Position = 0;
            }
        }

  
        public static TagLib.Tag FileTagReader(Stream stream, string fileName)
        {
            var simpleFileAbstraction = new SimpleFileAbstraction(stream, fileName);

            var mp3File = TagLib.File.Create(simpleFileAbstraction);

            Tag tags = mp3File.Tag;

            return tags;
        }

        private static void WalkDirectoryTree(DirectoryInfo root, string fileFilter, IList<LocalFile> result)
        {
            var files = root.GetFiles(fileFilter);

            foreach (var fi in files)
            {
                try
                {
                    using (var stream = fi.OpenRead())
                    {
                        Tag tag = FileTagReader(stream, fi.FullName);

                        var localFile = new LocalFile
                        {
                            FileType = FileType.Flac,
                            Album = tag.Album,
                            Artist = tag.FirstPerformer,
                            Path = fi.FullName,
                            Title = tag.Title
                        };

                        result.Add(localFile);
                    }

                    //TagLib.File.Create()

                    //using (var flacFile = new FlacFile(fi.FullName))
                    //{

                    //    }
                    //    else
                    //    {
                    //        throw new ApplicationException("No meta data found");
                    //    }

                    //    log.Info("Flac file " + fi.FullName + " was imported");
                    //}
                }
                catch (Exception ex)
                {
                    log.Error("Failed to import flac file " + fi.FullName, ex);
                }
            }

            foreach (var dirInfo in root.GetDirectories())
            {
                WalkDirectoryTree(dirInfo, fileFilter, result);
            }
        }

        public static IEnumerable<LocalFile> Import(DirectoryInfo root)
        {
            var result = new List<LocalFile>();

            WalkDirectoryTree(root, "*.flac", result);

            return result;
        }

    }
}
