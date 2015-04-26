using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DJimmy.Domain.Playlist;
using log4net;

namespace DJimmy.Infrastructure.Library
{
    public class PlaylistGenerator
    {
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IEnumerable<Candidate> tracks;

        public PlaylistGenerator(IEnumerable<Candidate> tracks)
        {
            this.tracks = tracks;
        }

        public IList<Candidate> GeneratePlaylist(ITrackValue trackValue, int size)
        {
            var result = new List<Candidate>(tracks);

            var r = result.OrderByDescending(t => trackValue.Value(t)).Take(size).ToList();

            foreach (var t in r)
            {
                log.Debug(trackValue.Print(t));
            }

            return r;
        }
    }

    public interface ITrackValue
    {
        double Value(Candidate track);
        string Print(Candidate track);
    }

    public class Random : ITrackValue
    {
        System.Random rnd = new System.Random();

        public double Value(Candidate track)
        {
            return rnd.NextDouble();
        }

        public string Print(Candidate track)
        {
            return "";
        }
    }

    public class Rotation : ITrackValue
    {
        const int LikedSongsRotationInterval = 100;
        
        private int songCount;
        private int playcountGroupsCount;

        public Rotation(int songCount, int playcountGroupsCount)
        {
            this.songCount = songCount;
            this.playcountGroupsCount = playcountGroupsCount;
        }

        public double Value(Candidate track)
        {
            //DateTime now = DateTime.Now;
            //int playedDaysAgo = (now - track.Played).Days;
            //int addedDaysAgo = (now - track.Added).Days;
            //int playcountThisYear = playbackCounter.Count(track);

            //if (starredTracks.Any(s => track.SpotifyUrls.Contains(s)))
            //{
            //    return track.PlayedSortIdx * (SongCount / LikedSongsRotationInterval) - track.PlaycountIdx;
            //}
            //else
            //{
            //    return track.PlayedSortIndex - track.PlaycountIndex;
            //}

            return track.PlayedSortIndex - ((double)((double)playcountGroupsCount - XX(track)) / (double)playcountGroupsCount) * (double)songCount;
        }

        private double XX(Candidate t)
        {
            return Math.Min(t.PlaycountIndex + (double)playcountGroupsCount / 10, (double)playcountGroupsCount);            
        }

        public string Print(Candidate t)
        {
            return String.Format("track: {0}, v = {1}-(({4}-{2})/{4})*{3}={5}", t.Song.Artist + " - " + t.Song.Title, t.PlayedSortIndex, XX(t), songCount, playcountGroupsCount, t.PlayedSortIndex - ((double)((double)playcountGroupsCount - (double)XX(t)) / (double)playcountGroupsCount) * (double)songCount);
        }
    }

    public class Favourites : ITrackValue
    {
        public double Value(Candidate track)
        {
            return track.PlaycountIndex;
        }

        public string Print(Candidate track)
        {
            return "";
        }
    }

    //public class Popular : ITrackValue
    //{
    //    readonly PlaybackCounter playbackCounter = new PlaybackCounter(DateTime.Now.AddMonths(-3), DateTime.Now);

    //    public double Value(Candidate track)
    //    {
    //        return playbackCounter.Count(track);
    //    }

    //    public string Print(Candidate track)
    //    {
    //        return "";
    //    }
    //}

    //public class AddedInYear : ITrackValue
    //{
    //    private readonly int year;

    //    public AddedInYear(int year)
    //    {
    //        this.year = year;
    //    }

    //    public double Value(Candidate track)
    //    {
    //        return track.Added.Year == year ? 1 : 0;
    //    }

    //    public string Print(Candidate track)
    //    {
    //        return "";
    //    }
    //}

    public class Aggregate : ITrackValue
    {
        private readonly IEnumerable<ITrackValue> parts;

        public Aggregate(IEnumerable<ITrackValue> parts)
        {
            this.parts = parts;
        }

        public double Value(Candidate track)
        {
            var result = parts.Select(x => x.Value(track)).Aggregate((x, y) => x * y);
            return result;
        }

        public string Print(Candidate track)
        {
            return "";
        }
    }
}
