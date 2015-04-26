using System.Collections.Generic;
using System.Xml;
using DJimmy.Domain.LocalFiles;

namespace DJimmy.Infrastructure.Integrations
{
    public class ITunesLibrary
    {
        private static FileType ParseFileType(string s)
        {
            if (s == "mp3")
                return FileType.Mp3;
            else if (s == "flac")
                return FileType.Flac;
            else 
                return FileType.Unknown;
        }

        public static IEnumerable<LocalFile> Parse(string filename)
        {
            var doc = new XmlDocument();
            doc.Load(filename);


            foreach (XmlNode songNode in doc.SelectNodes("//dict/dict/dict"))
            {
                string title = "";
                string artist = "";
                string album = "";
                string localFile = null;

                bool isFile = false;

                foreach (XmlNode c in songNode.ChildNodes)
                {
                    if (c.Name == "key")
                    {
                        if (c.InnerText == "Name")
                        {
                            title = c.NextSibling.InnerText.Trim();
                        }
                        if (c.InnerText == "Artist")
                        {
                            artist = c.NextSibling.InnerText.Trim();
                        }
                        if (c.InnerText == "Album")
                        {
                            album = c.NextSibling.InnerText.Trim();
                        }

                        if (c.InnerText == "Location")
                        {
                            localFile = c.NextSibling.InnerText;
                        }

                        if (c.InnerText == "Track Type")
                        {
                            if (c.NextSibling.InnerText == "File")
                            {
                                isFile = true;
                            }
                        }

                        if (c.InnerText == "Podcast")
                        {
                            if (c.NextSibling.Name == "true")
                            {
                                isFile = false;
                            }                            
                        }
                    }
                }

                if (isFile)
                {
                    var fileType = ParseFileType(localFile.Substring(localFile.LastIndexOf('.') + 1).ToLower());

                    yield return new LocalFile
                    {
                        Album = album,
                        Artist = artist,
                        Path = localFile
                            .Replace("file://localhost//DiskStation", "\\\\DiskStation")
                            .Replace("%20", " ")
                            .Replace("/", "\\"),
                        Title = title,
                        FileType = fileType
                    };
                }
            }


        }



    }
}
