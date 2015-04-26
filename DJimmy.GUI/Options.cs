using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DJimmy.Infrastructure.Integrations;
using Raven.Client;

namespace DJimmy.GUI
{
    public partial class Options : Form
    {
        private readonly LibraryUserSettings libraryUserSettings;
        private readonly Timer statusChecker = new Timer();
        private readonly ISpotifyAuthorizationService spotifyAuthorizationService;

        public Options(LibraryUserSettings libraryUserSettings, ISpotifyAuthorizationService spotifyAuthorizationService)
        {
            InitializeComponent();

            this.libraryUserSettings = libraryUserSettings;
            this.spotifyAuthorizationService = spotifyAuthorizationService;
            tbLastFmUsername.Text = libraryUserSettings.LibraryProperties.LastFmUsername;
            udAutoSyncInterval.Value = libraryUserSettings.LibraryProperties.AutoSyncInterval;

            statusChecker.Interval = 2000;
            statusChecker.Tick += RefreshSpotifyLoginStatus;
            lbSpotifyLogin.Text = spotifyAuthorizationService.IsLoggedIn() ? "Spotify: Logged in" : "Spotify: Logged out";
            statusChecker.Start();            
        }

        private void RefreshSpotifyLoginStatus(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                lbSpotifyLogin.Text = spotifyAuthorizationService.IsLoggedIn()
                    ? "Spotify: Logged in"
                    : "Spotify: Logged out";
            }));
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            libraryUserSettings.LibraryProperties.LastFmUsername = tbLastFmUsername.Text.Trim();
            libraryUserSettings.LibraryProperties.AutoSyncInterval = (int)udAutoSyncInterval.Value;
            libraryUserSettings.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sInfo = new ProcessStartInfo("https://accounts.spotify.com/en/authorize?client_id=bf6a7ee866724c36871c7436c7940d17&redirect_uri=http:%2F%2Flocalhost:8889%2FAuthorized&scope=playlist-modify-public%20playlist-modify-private%20user-library-read%20user-library-modify&response_type=code&state=123");
            Process.Start(sInfo);
        }
        
        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (String.IsNullOrEmpty(libraryUserSettings.LibraryProperties.LastFmUsername) || !spotifyAuthorizationService.IsLoggedIn())
            {
                e.Cancel = MessageBox.Show(
                    "You must be logged into Spotify and have supplied a valid last.fm username, otherwise I can't build your music collection. You can open this window at a later time from the Views -> Options menu. Really close?",
                    "Missing information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No;
            }
        }

        private void Options_FormClosed(object sender, FormClosedEventArgs e)
        {
            statusChecker.Tick -= RefreshSpotifyLoginStatus;
        }
    }
}
