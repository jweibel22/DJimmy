using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Be.Timvw.Framework.ComponentModel;
using DJimmy.Domain.Spotify;
using DJimmy.Infrastructure.Integrations;

namespace DJimmy.GUI
{
    public partial class AddToSpotify : Form
    {
        private readonly IList<SpotifySong> songs;

        public AddToSpotify(IList<SpotifySong> songs)
        {
            this.songs = songs;
            InitializeComponent();
        }

        private void AddToSpotify_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new SortableBindingList<SpotifySong>(songs);
        }

        public SpotifySong SelectedSong { get; private set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                SelectedSong = songs[dataGridView1.CurrentRow.Index];
                    //(string)dataGridView1.CurrentRow.Cells["Url"].Value;                
                //Close();
            }
        }
    }
}
