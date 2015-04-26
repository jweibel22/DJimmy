using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Be.Timvw.Framework.ComponentModel;
using DJimmy.Domain.Library;
using DJimmy.Domain.LocalFiles;
using DJimmy.Domain.Playlist;
using DJimmy.Infrastructure;
using DJimmy.Infrastructure.Integrations;
using DJimmy.Infrastructure.Library;
using Random = DJimmy.Infrastructure.Library.Random;

namespace DJimmy.GUI
{
    public partial class PlaylistGenerator : Form
    {
        private readonly Library library;
        private IEnumerable<Song> songs;
        private IEnumerable<Song> playlist;
        private readonly ISpotifyAuthorizationService spotifyAuthorizationService;

        public PlaylistGenerator(Library library, ISpotifyAuthorizationService spotifyAuthorizationService)
        {
            this.library = library;
            this.spotifyAuthorizationService = spotifyAuthorizationService;
            InitializeComponent();

            RefreshFilter();
        }

        private IEnumerable<Song> Generate(ITrackValue trackValue)
        {
            var orderedByPlaybacks = songs.GroupBy(s => s.Playcount).Select(kv => kv.Key).OrderBy(p => p).ToList();
            var orderedByPlayed = songs.OrderByDescending(s => s.LastPlayed).ToList();
            var orderedByAdded = songs.OrderByDescending(s => s.FirstPlayed).ToList();

            var candidates = songs.Select(s => new Candidate
            {
                Song = s,
                AddedSortIndex = orderedByAdded.IndexOf(s),
                PlaycountIndex = orderedByPlaybacks.IndexOf(s.Playcount),
                PlayedSortIndex = orderedByPlayed.IndexOf(s)
            });

            var generator = new Infrastructure.Library.PlaylistGenerator(candidates);

            return generator.GeneratePlaylist(trackValue, 30).Select(c => c.Song);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            playlist = Generate(new Random());
            dgvSongs.DataSource = new SortableBindingList<Song>(playlist);
        }

        private void btnFavourites_Click(object sender, EventArgs e)
        {
            playlist = Generate(new Favourites());
            dgvSongs.DataSource = new SortableBindingList<Song>(playlist);
        }

        private void btnRotation_Click(object sender, EventArgs e)
        {
            playlist = Generate(new Rotation(songs.Count(), songs.GroupBy(s => s.Playcount).Count()));
            dgvSongs.DataSource = new SortableBindingList<Song>(playlist);
        }

        private static string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath + Path.DirectorySeparatorChar);
            return Uri.UnescapeDataString(referenceUri.MakeRelativeUri(fileUri).ToString()).Replace('/', Path.DirectorySeparatorChar);
        }

        private void btnExportToSpotify_Click(object sender, EventArgs e)
        {
            if (playlist != null)
            {
                if (rbSpotify.Checked)
                {
                    var playlistApi = new SpotifyPlaylistApi(spotifyAuthorizationService.GetAuthorizationToken());
                    playlistApi.Clear();
                    playlistApi.AddTracks(playlist.ToList());                                    
                }
                else if (rbLocal.Checked)
                {
                    var dlg = new SaveFileDialog();

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        using (var writer = new StreamWriter(dlg.FileName))
                        {
                            writer.WriteLine("#EXTM3U");

                            foreach (var song in playlist)
                            {
                                writer.WriteLine(String.Format("#EXTINF:0,{0} - {1}", song.Artist, song.Title));

                                string path = null;

                                if (rbQualityFlac.Checked)
                                {
                                    path = song.LocalFiles.First(f => f.FileType == FileType.Flac).Path;
                                }
                                else if (rbQualityMp3.Checked)
                                {
                                    path = song.LocalFiles.First(f => f.FileType == FileType.Mp3).Path;
                                }
                                else
                                {
                                    path = song.LocalFiles.First().Path;
                                }

                                if (path != null)
                                {
                                    if (cbRelativePaths.Checked)
                                    {
                                        path = MakeRelative(path, new FileInfo(dlg.FileName).DirectoryName);
                                    }
                                    writer.WriteLine(path);
                                }
                            }
                        }                        
                    }

                }
            }
        }

        private void RefreshFilter()
        {
            if (rbSpotify.Checked)
            {
                songs = library.Songs.Where(s => s.Playcount > 0 && s.SpotifyUrls.Any());
            }
            else if (rbLocal.Checked)
            {
                songs = library.Songs.Where(s => s.Playcount > 0 && s.LocalFiles.Any());

                if (rbQualityFlac.Checked)
                {
                    songs = songs.Where(s => s.LocalFiles.Any(f => f.FileType == FileType.Flac));    
                }
                else if (rbQualityMp3.Checked)
                {
                    songs = songs.Where(s => s.LocalFiles.Any(f => f.FileType == FileType.Mp3));    
                }
            }
        }

        private void rbSpotify_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFilter();
        }

        private void rbLocal_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFilter();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
