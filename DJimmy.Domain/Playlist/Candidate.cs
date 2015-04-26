using DJimmy.Domain.Library;

namespace DJimmy.Domain.Playlist
{
    public class Candidate
    {
        public Song Song { get; set; }

        public int PlaycountIndex { get; set; }

        public int AddedSortIndex { get; set; }

        public int PlayedSortIndex { get; set; }
    }
}
