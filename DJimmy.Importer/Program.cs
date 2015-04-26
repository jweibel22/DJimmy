using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DJimmy.Domain;
using DJimmy.Domain.Library;
using DJimmy.Domain.LocalFiles;
using DJimmy.Domain.Spotify;
using DJimmy.Infrastructure;
using DJimmy.Infrastructure.Importers;
using DJimmy.Infrastructure.Indexes;
using Raven.Abstractions.Extensions;
using Raven.Client;

namespace DJimmy.Importer
{
    class Program
    {
        private const string AuthorizationToken =
            "BQDTR31TIaL1K4nBAyfTK8AEzGvXZ0KnxqZup18q9cdoSqt6Yhv9bSFYvVWT54u27rOk52qGXr45FCiYconFiIn9XYKzPRinjv0dZkVG3KVQb2_Ol0t1XoYeKPE1NZfbeAyOVgrYPJcZpPJxgN50uGah307_mBYv01vIYhIUk4axe1_4iBAYZceHyOca4zBTAE-Hef0y";

        static void Main(string[] args)
        {
            //var api = new SpotifyLibraryApi(AuthorizationToken);
            //AddSongs(api.GetLibrary());

            AutoMapper.Mapper
                .CreateMap<SpotifySongEvent, SpotifySong>();

            AutoMapper.Mapper
                .CreateMap<SpotifySong, SpotifySongEvent>()
                .ForMember(x => x.Id, o => o.Ignore());

            AutoMapper.Mapper
                .CreateMap<LocalFileEvent, Domain.LocalFiles.LocalFile>();

            AutoMapper.Mapper
                .CreateMap<Domain.LocalFiles.LocalFile, LocalFileEvent>()
                .ForMember(x => x.Id, o => o.Ignore());
                        
            var documentStore = DocumentStoreFactory.DocumentStore;

            //using (var session = documentStore.OpenSession())
            //{
            //    var importer = new SpotifyImporter(session);

            //    importer.Import();

            //    session.SaveChanges();
            //}

            //using (var session = documentStore.OpenSession())
            //{
            //    var importer = new LocalFileImporter(session, @"c:\transit\Library.xml");

            //    importer.Import();

            //    session.SaveChanges();
            //}            
            

            //AddSongs(ITunesLibrary.Parse(@"c:\transit\Library.xml"));

            //var songs = new[]
            //{
            //    //Song.NewSpotifySong("Wonderwall", "Oasis", "asfd", "f://1.mp3"),
            //    //Song.NewSpotifySong("Wonderwall", "Oasis", "morning glory", "f://2.mp3"),
            //    //Song.NewSpotifySong("Hello", "Oasis", "asfd", "f://3.mp3")
            //    //Song.NewLocal("Wonderwall", "Oasis", "asfd", "f://wonderwall.mp3"),
            //};

            //AddSongs(songs);

            Console.WriteLine("All done");

            Console.ReadLine();
        }

        static void AddSongs(IEnumerable<Song> songs)
        {

            IDictionary<string, Song> allSongs;
            IList<string> spotifyUrls;

            var documentStore = DocumentStoreFactory.DocumentStore;

            using (var session = documentStore.OpenSession())
            {

                allSongs = session.Query<Song>().Take(10000).ToDictionary(s => s.Id, s => s);
                spotifyUrls = allSongs.Values.SelectMany(s => s.SpotifyUrls).ToList();

                session.Advanced.MaxNumberOfRequestsPerSession = 10000;

                foreach (var song in songs)
                {                    
                    if (song.SpotifyUrls.Count > 1)
                    {
                        throw new Exception("At most one spotify url was expected");
                    }

                    //if (song.SpotifyUrls.Count == 1 && session.Query<Song, SpotifyUrlIndex>()
                    //    .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                    //    .ProjectFromIndexFieldsInto<SpotifyUrlIndex.ReduceResult>()
                    //    .Any(x => x.Url == song.SpotifyUrls.First()))
                        if (song.SpotifyUrls.Count == 1 && spotifyUrls.Any(x => x == song.SpotifyUrls.First()))
                    {
                        continue;
                    }
                    else
                    {
                        //var songFromDb = session.Query<TitleArtistIndex.ReduceResult, TitleArtistIndex>()
                        //                        .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                        //                        .Where(s => s.Artist == song.Artist && s.Title == song.Title)                                                
                        //                        .As<Song>()                                                
                        //                        .ToList()
                        //                        .FirstOrDefault();

                        var songFromDb = allSongs.Values.FirstOrDefault(s => String.Compare(s.Artist, song.Artist, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0
                            && String.Compare(s.Title, song.Title, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0);

                        if (songFromDb == null)
                        {
                            Console.WriteLine("Adding song {1} - {0}", song.Title, song.Artist);
                            session.Store(song);
                            allSongs.Add(song.Id, song);
                            spotifyUrls.AddRange(song.SpotifyUrls);
                        }
                        else
                        {
                            foreach (var localFile in song.LocalFiles)
                            {
                                if (!songFromDb.LocalFiles.Contains(localFile))
                                {
                                    Console.WriteLine("Adding file {0}", localFile);
                                    songFromDb.LocalFiles.Add(localFile);
                                }                                
                            }

                            foreach (var url in song.SpotifyUrls)
                            {
                                if (!songFromDb.SpotifyUrls.Contains(url))
                                {
                                    Console.WriteLine("Adding spotify link {0}", url);
                                    songFromDb.SpotifyUrls.Add(url);        
                                    spotifyUrls.Add(url);
                                }
                            }
                        }
                    }                                        
                }
                session.SaveChanges();
            }            
        }
    }
}
