using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using Be.Timvw.Framework.ComponentModel;
using DJimmy.Domain;
using DJimmy.Domain.Library;
using DJimmy.Domain.LocalFiles;
using DJimmy.Domain.Spotify;
using DJimmy.Infrastructure;
using DJimmy.Infrastructure.Events;
using DJimmy.Infrastructure.Importers;
using DJimmy.Infrastructure.Integrations;
using DJimmy.Infrastructure.Library;
using Raven.Client.Document;
using Timer = System.Windows.Forms.Timer;

namespace DJimmy.GUI
{
    public partial class Form1 : Form
    {
        private Library library;
        private IList<Song> displayedSongs;

        private readonly SpotifyAuthorizationHttpServer spotifyAuthorizationHttpServer;
        private readonly ISpotifyAuthorizationService spotifyAuthorizationService;

        private ResponsiveTextBox responsiveTextBox;
        
        private readonly LibraryFilter libraryFilter = new LibraryFilter();

        private readonly Timer statusChecker = new Timer();

        private AutoSync autoSync;

        public Form1()
        {
            InitializeComponent();

            responsiveTextBox = new ResponsiveTextBox(tbFilter);
            responsiveTextBox.TypingDone += responsiveTextBox_TypingDone;
            spotifyAuthorizationService = new SpotifyAuthorizationService(DocumentStoreFactory.DocumentStore);
            spotifyAuthorizationHttpServer = new SpotifyAuthorizationHttpServer(spotifyAuthorizationService);

            LibraryUserSettings.Initialize(DocumentStoreFactory.DocumentStore);

            statusChecker.Interval = 30000;            
            statusChecker.Tick += (sender, args) => this.Invoke(new Action(RefreshSpotifyLoginStatus));
            RefreshSpotifyLoginStatus();
            statusChecker.Start();            
        }

        #region EventHandlers

        
        private void RefreshSpotifyLoginStatus()
        {
            lbSpotifyLogin.Text = spotifyAuthorizationService.IsLoggedIn() ? "Spotify: Logged in" : "Spotify: Logged out";
        }

        private void RefreshLibrarySyncStatus(int progress, string text)
        {
            Invoke(new Action(() =>
            {
                pbLibrarySync.Visible = true;
                pbLibrarySync.ProgressBar.Value = progress;
                lblLibrarySyncStatus.Text = text;
                statusStrip1.Refresh();
            }));
        }

        void responsiveTextBox_TypingDone(object sender, EventArgs e)
        {
            libraryFilter.Filter = tbFilter.Text.ToLower();
            RefreshGrid();
        }

        private void AutoSyncOnUpdated(object sender, EventArgs eventArgs)
        {
            Invoke(new Action(LibrarySyncDone));
        }

        private void LibrarySyncDone()
        {
            lblLibrarySyncStatus.Text = String.Format("Library synched at {0}", DateTime.Now);
            pbLibrarySync.Visible = false;
            statusStrip1.Refresh();
        }

        void library_Updated(object sender, EventArgs e)
        {
            this.Invoke(new Action(RefreshGrid));
        }

        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadLibrary();

            spotifyAuthorizationHttpServer.Start();
        }

        private void RefreshGrid()
        {
            displayedSongs = libraryFilter.ApplyFilter(library.Songs).ToList();

            int selectedRow = -1;
            if (dgvSongs.SelectedRows.Count == 1)
            {
                selectedRow = dgvSongs.CurrentRow.Index;
            }

            var sortProperty = dgvSongs.DataSource == null ? null : ((SortableBindingList<Song>)dgvSongs.DataSource).SortProperty;
            var sortDirection = dgvSongs.DataSource == null ? ListSortDirection.Ascending : ((SortableBindingList<Song>)dgvSongs.DataSource).SortDirection;

            dgvSongs.DataSource = new SortableBindingList<Song>(displayedSongs);

            if (selectedRow != -1 && dgvSongs.Rows.Count > selectedRow)
            {
                dgvSongs.CurrentCell = dgvSongs.Rows[selectedRow].Cells[1];
            }

            if (sortProperty != null)
            {
                ((SortableBindingList<Song>)dgvSongs.DataSource).Sort(sortProperty, sortDirection);    
            }            
        }

        private void dgvSongs_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn column in dgvSongs.Columns)
            {
                if (column.Name == "Title")
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    column.Width = 300;
                }
                else if (column.Name == "Artist")
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    column.Width = 300;
                }
                else if (column.Name == "Album")
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    column.Width = 300;
                }
                else if (column.Name == "Playcount")
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    column.Width = 70;
                }
                else if (column.Name == "HasLocalFiles")
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    column.Width = 70;
                }
                else if (column.Name == "InSpotify")
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    column.Width = 70;
                }
                else if (column.Name == "FirstPlayed")
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    column.Width = 100;
                }
                else if (column.Name == "LastPlayed")
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    column.Width = 100;
                }
                else
                {
                    column.Visible = false;
                }
            }
            foreach (DataGridViewRow row in dgvSongs.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }

            lblRowCount.Text = "Rows: " + dgvSongs.Rows.Count;
            statusStrip1.Refresh();
        }


        private void cbShowSpotifySongs_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLocalNo.Checked)
                libraryFilter.LocalFilter = IncludeInFilter.No;
            else if (rbLocalYes.Checked)
                libraryFilter.LocalFilter = IncludeInFilter.Yes;
            else libraryFilter.LocalFilter = IncludeInFilter.Whatever;

            if (rbSpotifyNo.Checked)
                libraryFilter.SpotifyFilter = IncludeInFilter.No;
            else if (rbSpotifyYes.Checked)
                libraryFilter.SpotifyFilter = IncludeInFilter.Yes;
            else libraryFilter.SpotifyFilter = IncludeInFilter.Whatever;

            if (rbLastFmNo.Checked)
                libraryFilter.LastFmFilter = IncludeInFilter.No;
            else if (rbLastFmYes.Checked)
                libraryFilter.LastFmFilter = IncludeInFilter.Yes;
            else libraryFilter.LastFmFilter = IncludeInFilter.Whatever;

            RefreshGrid();
        }

        private void duplicatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options(LibraryUserSettings.Instance, spotifyAuthorizationService).ShowDialog();
        }


        private void LoadLibrary()
        {
            bool newLibrary = false;

            using (var session = DocumentStoreFactory.DocumentStore.OpenSession())
            {
                library = session.Query<Library>().OrderByDescending(x => x.CreatedAt).FirstOrDefault();

                if (library == null)
                {
                    library = new Library();
                    library.CreatedAt = DateTime.Now;
                    session.Store(library);
                    newLibrary = true;
                }                

                session.SaveChanges();
            }

            if (newLibrary)
            {
                new Options(LibraryUserSettings.Instance, spotifyAuthorizationService).ShowDialog();
            }

            library.Updated += library_Updated;
            RefreshGrid();            
            
            autoSync = new AutoSync(library, spotifyAuthorizationService, LibraryUserSettings.Instance);
            autoSync.Progressed += RefreshLibrarySyncStatus;
            autoSync.Updated += AutoSyncOnUpdated;
                
            autoSync.Start();
        }

        private void CloseLibrary()
        {
            if (autoSync != null)
            {
                autoSync.Stop();
                autoSync.Updated -= AutoSyncOnUpdated;
                autoSync.Progressed -= RefreshLibrarySyncStatus;
            }

            autoSync = null;
            displayedSongs = null;
            library = null;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {                
                //ImportFiles(() => LocalFilesImporter.Import(new DirectoryInfo(dlg.SelectedPath)));
                ImportFiles(() => LocalFilesImporter.Import(new DirectoryInfo("\\\\DiskStation\\music\\flac")));
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "ITunes Xml files (*.xml)|*.xml";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImportFiles(() => ITunesLibrary.Parse(dlg.FileName));
            }
        }

        private void ImportFiles(Func<IEnumerable<LocalFile>> importerFunc)        
        {

                var progress = new Progress();
                progress.Show();

                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (x, y) =>
                {
                    var progressStatus = new ProgressStatus();
                    progressStatus.Progressed += (o, args) => Invoke(new Action(() => progress.SetProgress(progressStatus.Progress, progressStatus.Text)));                    

                    IEnumerable<LocalFile> existingFiles;

                    progressStatus.OnProgress(0, "Importing local files");

                    using (var session = DocumentStoreFactory.DocumentStore.OpenSession())
                    {
                        existingFiles =
                            new EventReplayer<LocalFileEvent, LocalFile>(session, new LocalFileEventsIdentifier())
                                .ReplayAll();
                    }

                    var importer = new LocalFileImporter(existingFiles, DocumentStoreFactory.DocumentStore);

                    importer.Import(importerFunc());

                    progressStatus.OnProgress(0, "Waiting to gain access to library");

                    lock (library)
                    {
                        using (var session = DocumentStoreFactory.DocumentStore.OpenSession())
                        {
                            var aliases = session.Query<Alias>().ToList();
                            var aliasMap = new AliasMap(aliases);

                            progressStatus.OnProgress(0, "Merging local files into library");

                            var localFileReplayer = new EventReplayer<LocalFileEvent, LocalFile>(session,
                                new LocalFileEventsIdentifier());
                            var localFileEvents = localFileReplayer.EventsFrom(library.LatestLocalFileSync);
                            library.SyncWithLocalStore(aliasMap, localFileEvents, progressStatus);
                            session.Store(library);
                            session.SaveChanges();

                            Invoke(new Action(() => RefreshGrid()));
                        }
                    }
                };

                backgroundWorker.RunWorkerCompleted += (o, args) => Invoke(new Action(() => progress.Close()));
                backgroundWorker.RunWorkerAsync();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var progress = new Progress();
            progress.Show();

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (x, y) =>
            {
                var newLibrary = new Library();

                using (var session = DocumentStoreFactory.DocumentStore.OpenSession())
                {
                    var progressStatus = new ProgressStatus();
                    progressStatus.Progressed += (o, args) => Invoke(new Action(() => progress.SetProgress(progressStatus.Progress, progressStatus.Text)));

                    progressStatus.OnProgress(0, "Loading aliases");

                    var aliases = session.Query<Alias>().ToList();
                    var aliasMap = new AliasMap(aliases);

                    session.Store(newLibrary);

                    progressStatus.OnProgress(5, "Loading spotify songs");

                    var replayer = new EventReplayer<SpotifySongEvent, SpotifySong>(session, new SpotifyEventsIdentifier());
                    var events = replayer.EventsFrom(newLibrary.LastSpotifySync);
                    newLibrary.SyncWithSpotify(aliasMap, events, progressStatus.SpawnChild(20));

                    progressStatus.OnProgress(25, "Loading local files");

                    var localFileReplayer = new EventReplayer<LocalFileEvent, LocalFile>(session, new LocalFileEventsIdentifier());
                    var localFileEvents = localFileReplayer.EventsFrom(newLibrary.LatestLocalFileSync);
                    newLibrary.SyncWithLocalStore(aliasMap, localFileEvents, progressStatus.SpawnChild(50));

                    progressStatus.OnProgress(75, "Loading playbacks");

                    var playbackReplayer = new LastFmEventReplayer(session);
                    var playbacks = playbackReplayer.EventsFrom(newLibrary.LatestLastFmSync.ToUniversalTime());
                    newLibrary.AddPlaybacks(aliasMap, playbacks, progressStatus.SpawnChild(25));

                    session.SaveChanges();
                }

                Invoke(new Action(() =>
                {
                    CloseLibrary();
                    LoadLibrary();
                }));
            };

            backgroundWorker.RunWorkerCompleted += (o, args) => Invoke(new Action(() => progress.Close()));
            backgroundWorker.RunWorkerAsync();
        }

        private void playlistGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PlaylistGenerator(library, spotifyAuthorizationService).Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void syncToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //using (var session = DocumentStoreFactory.DocumentStore.OpenSession())
            //{
            //    var sw = new Stopwatch();
            //    sw.Start();
            //    using (session.Advanced.DocumentStore.AggressivelyCacheFor(TimeSpan.FromHours(5)))
            //    {
            //        var l = session.Query<Library>().OrderByDescending(x => x.CreatedAt).Take(3).ToList();
            //    }
            //    sw.Stop();

            //    MessageBox.Show("Done. It took " + sw.ElapsedMilliseconds + " ms");
            //}

            


            /*
            var progress = new Progress();
            progress.Show();

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (x, y) =>
            {
                try
                {
                    using (var sync = new Sync(DocumentStoreFactory.DocumentStore, library, spotifyAuthorizationService,
                            LibraryUserSettings.Instance.LibraryProperties.LastFmUsername, new CancellationToken()))
                    {
                        sync.Status.Progressed += (o, args) => Invoke(new Action(() => progress.SetProgress(sync.Status.Progress, sync.Status.Text)));
                        sync.Run();
                        Invoke(new Action(LibrarySyncDone));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Sync failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            backgroundWorker.RunWorkerCompleted += (o, args) => Invoke(new Action(() => progress.Close()));
            backgroundWorker.RunWorkerAsync();
             */
        }

        private void loginToSpotifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sInfo = new ProcessStartInfo("https://accounts.spotify.com/en/authorize?client_id=bf6a7ee866724c36871c7436c7940d17&redirect_uri=http:%2F%2Flocalhost:8889%2FAuthorized&scope=playlist-modify-public%20playlist-modify-private%20user-library-read%20user-library-modify&response_type=code&state=123");
            Process.Start(sInfo);
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {

        }

        private void aliasManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AliasManager(library, DocumentStoreFactory.DocumentStore).ShowDialog();
        }


        private void lookUpInSpotifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvSongs.CurrentRow != null)
            {
                var title = (string) dgvSongs.CurrentRow.Cells["Title"].Value;
                var artist = (string) dgvSongs.CurrentRow.Cells["Artist"].Value;
                var spotifySongs = new SpotifySearchApi().Search(artist, title).ToList();

                var dialog = new AddToSpotify(spotifySongs);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var api = new SpotifyLibraryApi(spotifyAuthorizationService.GetAuthorizationToken());
                    
                    var id = dialog.SelectedSong.Url.Substring("spotify:track:".Length);
                    api.AddToLibrary(id);

                    lock (library)
                    {
                        using (var session = DocumentStoreFactory.DocumentStore.OpenSession())
                        {
                            var aliasMap = new AliasMap(session.Query<Alias>().ToList());
                            library.SyncWithSpotify(aliasMap, new[] {Mapper.Map<SpotifySong, SpotifySongEvent>(dialog.SelectedSong)});
                            session.Store(library);
                            session.SaveChanges();
                        }
                    }
                }
            }
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void Play()
        {
            if (dgvSongs.CurrentRow != null)
            {
                var title = (string)dgvSongs.CurrentRow.Cells["Title"].Value;
                var artist = (string)dgvSongs.CurrentRow.Cells["Artist"].Value;

                var song = library.FindSong(title, artist);

                if (song.LocalFiles.Any())
                {
                    var sInfo = new ProcessStartInfo(song.LocalFiles.First().Path                            
                            .Replace("%20", " ")
                            .Replace("/", "\\"));
                    Process.Start(sInfo);
                }
            }            
        }

        private void Rename(string oldName, string newName, AliasType aliasType)
        {
            lock (library)
            {
                using (var session = DocumentStoreFactory.DocumentStore.OpenSession())
                {
                    var aliases = session.Query<Alias>().ToList();
                    var alias =
                        aliases.SingleOrDefault(
                            a =>
                                a.Type == aliasType &&
                                String.Compare(a.Was, oldName, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) ==
                                0);

                    if (alias != null)
                    {
                        library.RemoveAlias(alias);
                        alias.Is = newName;
                        library.AddNewAlias(alias);
                    }
                    else
                    {
                        alias = new Alias
                        {
                            Is = newName,
                            Was = oldName.ToLower(),
                            Type = aliasType
                        };
                        session.Store(alias);
                        library.AddNewAlias(alias);
                    }

                    session.Store(library);

                    session.SaveChanges();
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvSongs.CurrentCell != null)
            {
                if (dgvSongs.CurrentCell.OwningColumn.Name == "Title" ||
                    dgvSongs.CurrentCell.OwningColumn.Name == "Artist")
                {
                    var oldName = (string) dgvSongs.CurrentCell.Value;
                    string newName = oldName;

                    if (InputDialog.ShowInputDialog(ref newName) == DialogResult.OK)
                    {
                        AliasType aliasType = dgvSongs.CurrentCell.OwningColumn.Name == "Title"
                            ? AliasType.Title
                            : AliasType.Artist;

                        Rename(oldName, newName, aliasType);
                        RefreshGrid();
                    }
                }
            }
        }

        private void dgvSongs_DoubleClick(object sender, EventArgs e)
        {
            Play();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var progress = new Progress();
            progress.SetText("Shutting down ...");            

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (x, y) =>
            {
                spotifyAuthorizationHttpServer.Stop();
                CloseLibrary();
                statusChecker.Stop();
            };

            backgroundWorker.RunWorkerCompleted += (o, args) => Invoke(new Action(() =>
            {
                progress.Close();
                Close();
            }));
    
            backgroundWorker.RunWorkerAsync();

            progress.ShowDialog();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void playbacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Playbacks(DocumentStoreFactory.DocumentStore).Show();
        }

    }
}
