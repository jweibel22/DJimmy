using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJimmy.Domain.Library
{
    public class AliasMap
    {
        private readonly IDictionary<string, string> titleMap;
        private readonly IDictionary<string, string> artistMap;
        
        public AliasMap(IEnumerable<Alias> aliases)
        {
            artistMap = aliases.Where(a => a.Type == AliasType.Artist).ToDictionary(a => a.Was.ToLower(), a => a.Is);
            titleMap = aliases.Where(a => a.Type == AliasType.Title).ToDictionary(a => a.Was.ToLower(), a => a.Is);
        }

        public string MapTitle(string title)
        {
            if (title == null)
                return null;

            return titleMap.ContainsKey(title.ToLower()) ? titleMap[title.ToLower()] : title;
        }

        public string MapArtist(string artist)
        {
            if (artist == null)
                return null;

            return artistMap.ContainsKey(artist.ToLower()) ? artistMap[artist.ToLower()] : artist;
        }
    }
}
