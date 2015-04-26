using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DJimmy.Domain;
using DJimmy.Domain.Library;
using DJimmy.Infrastructure;
using DJimmy.Infrastructure.Events;
using DJimmy.Infrastructure.Importers;
using DJimmy.Infrastructure.Integrations;
using DJimmy.Infrastructure.Library;
using Timer = System.Timers.Timer;

namespace DJimmy.GUI
{
    public class AutoSync
    {
        private readonly Thread thread;
        private readonly Library library;
        private bool running;
        private readonly ISpotifyAuthorizationService spotifyAuthorizationService;
        private readonly LibraryUserSettings userSettings;
        private CancellationTokenSource cancellationTokenSource;
        private DateTime latestRun = DateTime.MinValue;

        public event EventHandler Updated;
        public event ProgressEventHandler Progressed;

        public delegate void ProgressEventHandler(int progress, string text);

        public AutoSync(Library library, ISpotifyAuthorizationService spotifyAuthorizationService, LibraryUserSettings userSettings)
        {
            this.library = library;
            this.spotifyAuthorizationService = spotifyAuthorizationService;
            this.userSettings = userSettings;
            thread = new Thread(Run);
        }

        private void Run()
        {
            while (running)
            {
                Thread.Sleep(1000);

                if (userSettings.LibraryProperties.AutoSyncInterval == 0 || DateTime.Now < latestRun.AddMinutes(userSettings.LibraryProperties.AutoSyncInterval))
                {
                    continue;
                }

                try
                {
                    cancellationTokenSource = new CancellationTokenSource();

                    var task = Task.Factory.StartNew(() =>
                    {
                        using (
                            var sync = new Sync(DocumentStoreFactory.DocumentStore, library, spotifyAuthorizationService,
                                userSettings.LibraryProperties.LastFmUsername, cancellationTokenSource.Token))
                        {
                            try
                            {
                                latestRun = DateTime.Now;

                                sync.Status.Progressed +=
                                    (sender, args) =>
                                    {
                                        if (Progressed != null)
                                            Progressed(sync.Status.Progress, sync.Status.Text);
                                    };
                                        
                                sync.Run();
                            }
                            finally
                            {
                                if (Updated != null)
                                {
                                    Updated(this, EventArgs.Empty);
                                }
                            }
                        }

                    }, cancellationTokenSource.Token);

                    task.Wait();
                    cancellationTokenSource = null;
                }
                catch (AggregateException ex)
                {
                    if (ex.InnerExceptions.Count != 1 || !(ex.InnerExceptions.Single() is UnauthorizedAccessException))
                    {
                        MessageBox.Show(ex.Message, "AutoSync failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "AutoSync failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void Start()
        {
            running = true;
            thread.Start();
        }

        public void Stop()
        {
            if (running)
            {
                running = false;

                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                }

                thread.Join();
            }
        }
    }
}
