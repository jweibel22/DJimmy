using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DJimmy.Domain.LastFM;
using DJimmy.Domain.LocalFiles;
using DJimmy.Domain.Spotify;
using Newtonsoft.Json;

namespace DJimmy.Domain.Library
{
    public class Library
    {

        protected IDictionary<string, Song> SongsMap { get; set; }

        protected List<string> LocalFiles { get; set; }

        protected List<string> SpotifyUrls { get; set; }

        public Library()
        {
            SpotifyUrls = new List<string>();
            LocalFiles = new List<string>();
            SongsMap = new Dictionary<string, Song>();
            CreatedAt = DateTime.Now;
        }

        public string Id { get; set; }

        public event EventHandler Updated;

        protected void OnUpdated()
        {
            if (this.Updated != null)
            {
                Updated(this, EventArgs.Empty);
            }
        }

        [Raven.Imports.Newtonsoft.Json.JsonIgnore]
        public IEnumerable<Song> Songs
        {
            get { return SongsMap.Values; }
        }

        private string AsKey(string title, string artist)
        {
            return String.Format("{0}-{1}", artist == null ? "" : artist.ToLower(), title == null ? "" : title.ToLower());
        }

        private void AddSong(Song song)
        {
            SongsMap.Add(AsKey(song.Title, song.Artist), song);
            LocalFiles.AddRange(song.LocalFiles.Select(f => f.Path));
            SpotifyUrls.AddRange(song.SpotifyUrls);
        }

        private void RemoveSong(Song song)
        {
            SongsMap.Remove(AsKey(song.Title, song.Artist));
            foreach (var localFile in song.LocalFiles)
            {
                LocalFiles.Remove(localFile.Path);
            }

            foreach (var spotifyUrl in song.SpotifyUrls)
            {
                SpotifyUrls.Remove(spotifyUrl);
            }
        }

        private bool IsEqual(string s1, string s2)
        {
            var x = s1 == null ? "" : s1.ToLower();
            var y = s2 == null ? "" : s2.ToLower();

            return y.Contains(x) || x.Contains(y);            
        }

        public Song FindSong(string title, string artist)
        {
            //var key = AsKey(title, artist);
            //return SongsMap.ContainsKey(key) ? SongsMap[key] : null;            

            //return SongsMap.Values.FirstOrDefault(s =>
            //    String.Compare(s.Artist, artist, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0
            //    && String.Compare(s.Title, title, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0);            

            return SongsMap.Values.FirstOrDefault(s => IsEqual(s.Artist, artist) && IsEqual(s.Title, title));            
        }

        public DateTime CreatedAt { get; set; }

        public DateTime LastSpotifySync { get; private set; }

        public DateTime LatestLastFmSync { get; private set; }

        public DateTime LatestLocalFileSync { get; set; }

        public void AddPlaybacks(AliasMap aliasMap, IEnumerable<Playback> playbacks, ProgressStatus progressStatus = null)
        {
            double eventsToProcess = playbacks.Count();
            int counter = 0;

            foreach (var playback in playbacks)
            {
                if (progressStatus != null)
                    progressStatus.OnProgress((int)(++counter / eventsToProcess * 100.0));

                var song = FindSong(aliasMap.MapTitle(playback.Title), aliasMap.MapArtist(playback.Artist));

                if (song != null)
                {
                    song.AddPlayback(playback.Timestamp);
                }
                else
                {
                    var newSong = Song.New(aliasMap.MapTitle(playback.Title), aliasMap.MapArtist(playback.Artist), playback.Album);
                    newSong.AddPlayback(playback.Timestamp);
                    AddSong(newSong); 
                }
            }

            if (playbacks.Any())
            {
                LatestLastFmSync = playbacks.Max(p => p.Timestamp).ToLocalTime();
                OnUpdated();
            }
        }

        public void SyncWithLocalStore(AliasMap aliasMap, IEnumerable<LocalFileEvent> localFiles, ProgressStatus progressStatus = null)
        {
            double eventsToProcess = localFiles.Count();
            int counter = 0;

            foreach (var localFile in localFiles)
            {
                if (progressStatus != null)
                    progressStatus.OnProgress((int)(++counter / eventsToProcess * 100.0));

                if (this.LocalFiles.Contains(localFile.Path))  //if (songs.SelectMany(s => s.LocalFiles).Any(url => url == localFile.Path))
                {
                    continue;
                }

                var existingSong = FindSong(aliasMap.MapTitle(localFile.Title), aliasMap.MapArtist(localFile.Artist));
                
                if (existingSong != null)
                {
                    if (!existingSong.LocalFiles.Any(x => x.Path == localFile.Path))
                    {
                        Console.WriteLine("Adding file {0}", localFile.Path);
                        existingSong.LocalFiles.Add(File.New(localFile.Path, localFile.FileType));
                    }
                }
                else
                {
                    AddSong(Song.NewLocal(aliasMap.MapTitle(localFile.Title), aliasMap.MapArtist(localFile.Artist), localFile.Album, localFile.Path, localFile.FileType));
                }
            }

            if (localFiles.Any())
            {
                LatestLocalFileSync = localFiles.Max(e => e.Timestamp);
                OnUpdated();
            }
        }

        public void SyncWithSpotify(AliasMap aliasMap, IEnumerable<SpotifySongEvent> spotifyEvents, ProgressStatus progressStatus = null)
        {
            double eventsToProcess = spotifyEvents.Count();
            int counter = 0;

            foreach (var spotifyEvent in spotifyEvents)
            {
                if (progressStatus != null)
                    progressStatus.OnProgress((int)(++counter / eventsToProcess * 100.0));

                switch (spotifyEvent.EventType)
                {
                    case EventType.Added:
                        if (this.SpotifyUrls.Contains(spotifyEvent.Url))  //if (songs.SelectMany(s => s.SpotifyUrls).Any(url => url == spotifyEvent.Url))
                        {
                            continue;
                        }

                        var existingSong = FindSong(aliasMap.MapTitle(spotifyEvent.Title), aliasMap.MapArtist(spotifyEvent.Artist));

                        if (existingSong != null)
                        {
                            if (!existingSong.SpotifyUrls.Contains(spotifyEvent.Url))
                            {
                                Console.WriteLine("Adding spotify link {0}", spotifyEvent.Url);
                                existingSong.SpotifyUrls.Add(spotifyEvent.Url);
                            }
                        }
                        else
                        {
                            AddSong(Song.NewSpotifySong(aliasMap.MapTitle(spotifyEvent.Title), aliasMap.MapArtist(spotifyEvent.Artist), spotifyEvent.Album,
                                spotifyEvent.Url));
                        }

                        break;
                    case EventType.Removed:

                        foreach (var matched in SongsMap.Values.Where(s => s.SpotifyUrls.Contains(spotifyEvent.Url)).ToList())
                        {
                            if (matched.SpotifyUrls.Count > 1 || matched.LocalFiles.Count > 0)
                            {
                                matched.SpotifyUrls.Remove(spotifyEvent.Url);
                            }
                            else
                            {
                                RemoveSong(matched);
                            }
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (spotifyEvents.Any())
            {
                LastSpotifySync = spotifyEvents.Max(e => e.Timestamp);
                OnUpdated();
            }
        }

        public void RemoveAlias(Alias alias)
        {
            throw new NotImplementedException();
        }

        public void AddNewAlias(Alias alias)
        {
            switch (alias.Type)
            {
                case AliasType.Title:
                {
                    var matched = SongsMap.Values.Where(s => String.Compare(s.Title, alias.Was, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
                                .ToList();

                    foreach (var song in matched)
                    {
                        var similarSong = FindSong(alias.Is, song.Artist);

                        if (similarSong != null)
                        {
                            similarSong.Merge(song);
                            RemoveSong(song);
                        }
                        else
                        {
                            song.ChangeTitle(alias.Is);
                        }
                    }
                    break;
                }
                case AliasType.Artist:
                {
                    var matched =
                        SongsMap.Values.Where(
                            s =>
                                String.Compare(s.Artist, alias.Was, CultureInfo.InvariantCulture,
                                    CompareOptions.IgnoreCase) == 0).ToList();

                    foreach (var song in matched)
                    {
                        var similarSong = FindSong(song.Title, alias.Is);

                        if (similarSong != null)
                        {
                            similarSong.Merge(song);
                            RemoveSong(song);
                        }
                        else
                        {
                            song.ChangeArtist(alias.Is);
                        }
                    }
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
