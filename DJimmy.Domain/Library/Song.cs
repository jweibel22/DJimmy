using System;
using System.Collections.Generic;
using System.Linq;
using DJimmy.Domain.LastFM;
using DJimmy.Domain.LocalFiles;
using Raven.Imports.Newtonsoft.Json;

namespace DJimmy.Domain.Library
{
    public class Song
    {
        public static Song NewLocal(string title, string artist, string album, string path, FileType filetype)
        {
            var result = New(title, artist, album);
            result.LocalFiles.Add(File.New(path, filetype));
            
            return result;
        }

        public static Song NewSpotifySong(string title, string artist, string album, string url)
        {
            var result = New(title, artist, album);
            result.SpotifyUrls.Add(url);
            return result;
        }

        public static Song New(string title, string artist, string album)
        {
            var result = new Song();
            result.Album = album;
            result.Artist = artist;
            result.Title = title;
            result.SpotifyUrls = new List<string>();
            result.LocalFiles = new List<File>();
            result.Playbacks = new List<DateTime>();

            return result;
        }

        protected Song()
        {
            SpotifyUrls = new List<string>();
            LocalFiles = new List<File>();
            Playbacks = new List<DateTime>();            
        }

        public string Id { get; set; }

        public string Title { get; private set; }

        public string Artist { get; private set; }

        public string Album { get; private set; }

        public IList<string> SpotifyUrls { get; private set; }

        public IList<File> LocalFiles { get; private set; }        
            
        [JsonIgnore]
        public bool HasLocalFiles
        {
            get { return LocalFiles.Any(); }
        }

        [JsonIgnore]
        public bool InSpotify
        {
            get { return SpotifyUrls.Any(); }
        }

        public void Merge(Song song)
        {
            SpotifyUrls = SpotifyUrls.Union(song.SpotifyUrls).ToList();
            LocalFiles = LocalFiles.Union(song.LocalFiles).ToList();
            Playbacks = Playbacks.Union(song.Playbacks).ToList();
        }

        public void ChangeArtist(string artist)
        {
            Artist = artist;
        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }

        public DateTime FirstPlayed
        {
            get { return Playbacks.Any() ? Playbacks.Min() : DateTime.MinValue; }
        }

        public DateTime LastPlayed
        {
            get { return Playbacks.Any() ? Playbacks.Max() : DateTime.MinValue; }
        }

        public int Playcount
        {
            get { return Playbacks.Count; }
        }

        public IList<DateTime> Playbacks { get; private set; }

        public void AddPlayback(DateTime timestamp)
        {
            Playbacks.Add(timestamp);
        }
    }
}
