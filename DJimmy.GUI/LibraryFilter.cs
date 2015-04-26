using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain.Library;

namespace DJimmy.GUI
{
    class LibraryFilter
    {
        public LibraryFilter()
        {
            SpotifyFilter = IncludeInFilter.Whatever;
            LocalFilter = IncludeInFilter.Whatever;
            LastFmFilter = IncludeInFilter.Whatever;
        }

        public IncludeInFilter SpotifyFilter { get; set; }

        public IncludeInFilter LocalFilter { get; set; }

        public IncludeInFilter LastFmFilter { get; set; }

        public string Filter { get; set; }

        public IEnumerable<Song> ApplyFilter(IEnumerable<Song> newSongs)
        {
            if (LocalFilter == IncludeInFilter.Yes)
            {
                newSongs = newSongs.Where(s => s.LocalFiles != null && s.LocalFiles.Count > 0);
            }
            else if (LocalFilter == IncludeInFilter.No)
            {
                newSongs = newSongs.Where(s => s.LocalFiles == null || s.LocalFiles.Count == 0);
            }

            if (SpotifyFilter == IncludeInFilter.Yes)
            {
                newSongs = newSongs.Where(s => s.SpotifyUrls.Any());
            }
            else if (SpotifyFilter == IncludeInFilter.No)
            {
                newSongs = newSongs.Where(s => !s.SpotifyUrls.Any());
            }

            if (LastFmFilter == IncludeInFilter.Yes)
            {
                newSongs = newSongs.Where(s => s.Playbacks.Any());
            }
            else if (LastFmFilter == IncludeInFilter.No)
            {
                newSongs = newSongs.Where(s => !s.Playbacks.Any());
            }

            if (!string.IsNullOrWhiteSpace(Filter))
            {
                newSongs = newSongs.Where(s =>
                    (s.Title != null && s.Title.ToLower().Contains(Filter))
                    || (s.Artist != null && s.Artist.ToLower().Contains(Filter))
                    || (s.Album != null && s.Album.ToLower().Contains(Filter)));
            }

            return newSongs.ToList();
        }
    }
}
