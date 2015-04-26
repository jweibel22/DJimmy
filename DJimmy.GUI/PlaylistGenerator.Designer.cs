namespace DJimmy.GUI
{
    partial class PlaylistGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvSongs = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbQualityWhatever = new System.Windows.Forms.RadioButton();
            this.rbQualityMp3 = new System.Windows.Forms.RadioButton();
            this.rbQualityFlac = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLocal = new System.Windows.Forms.RadioButton();
            this.rbSpotify = new System.Windows.Forms.RadioButton();
            this.btnExportToSpotify = new System.Windows.Forms.Button();
            this.btnRotation = new System.Windows.Forms.Button();
            this.btnFavourites = new System.Windows.Forms.Button();
            this.btnRandom = new System.Windows.Forms.Button();
            this.cbRelativePaths = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSongs)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSongs
            // 
            this.dgvSongs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSongs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSongs.Location = new System.Drawing.Point(0, 106);
            this.dgvSongs.Name = "dgvSongs";
            this.dgvSongs.Size = new System.Drawing.Size(933, 426);
            this.dgvSongs.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbRelativePaths);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnExportToSpotify);
            this.panel1.Controls.Add(this.btnRotation);
            this.panel1.Controls.Add(this.btnFavourites);
            this.panel1.Controls.Add(this.btnRandom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 100);
            this.panel1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbQualityWhatever);
            this.groupBox2.Controls.Add(this.rbQualityMp3);
            this.groupBox2.Controls.Add(this.rbQualityFlac);
            this.groupBox2.Location = new System.Drawing.Point(501, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 79);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quality";
            // 
            // rbQualityWhatever
            // 
            this.rbQualityWhatever.AutoSize = true;
            this.rbQualityWhatever.Location = new System.Drawing.Point(7, 57);
            this.rbQualityWhatever.Name = "rbQualityWhatever";
            this.rbQualityWhatever.Size = new System.Drawing.Size(72, 17);
            this.rbQualityWhatever.TabIndex = 2;
            this.rbQualityWhatever.Text = "Whatever";
            this.rbQualityWhatever.UseVisualStyleBackColor = true;
            // 
            // rbQualityMp3
            // 
            this.rbQualityMp3.AutoSize = true;
            this.rbQualityMp3.Location = new System.Drawing.Point(7, 38);
            this.rbQualityMp3.Name = "rbQualityMp3";
            this.rbQualityMp3.Size = new System.Drawing.Size(46, 17);
            this.rbQualityMp3.TabIndex = 1;
            this.rbQualityMp3.Text = "Mp3";
            this.rbQualityMp3.UseVisualStyleBackColor = true;
            // 
            // rbQualityFlac
            // 
            this.rbQualityFlac.AutoSize = true;
            this.rbQualityFlac.Checked = true;
            this.rbQualityFlac.Location = new System.Drawing.Point(7, 20);
            this.rbQualityFlac.Name = "rbQualityFlac";
            this.rbQualityFlac.Size = new System.Drawing.Size(65, 17);
            this.rbQualityFlac.TabIndex = 0;
            this.rbQualityFlac.TabStop = true;
            this.rbQualityFlac.Text = "Lossless";
            this.rbQualityFlac.UseVisualStyleBackColor = true;
            this.rbQualityFlac.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbLocal);
            this.groupBox1.Controls.Add(this.rbSpotify);
            this.groupBox1.Location = new System.Drawing.Point(315, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 74);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Destination";
            // 
            // rbLocal
            // 
            this.rbLocal.AutoSize = true;
            this.rbLocal.Location = new System.Drawing.Point(7, 44);
            this.rbLocal.Name = "rbLocal";
            this.rbLocal.Size = new System.Drawing.Size(51, 17);
            this.rbLocal.TabIndex = 1;
            this.rbLocal.Text = "Local";
            this.rbLocal.UseVisualStyleBackColor = true;
            this.rbLocal.CheckedChanged += new System.EventHandler(this.rbLocal_CheckedChanged);
            // 
            // rbSpotify
            // 
            this.rbSpotify.AutoSize = true;
            this.rbSpotify.Checked = true;
            this.rbSpotify.Location = new System.Drawing.Point(7, 20);
            this.rbSpotify.Name = "rbSpotify";
            this.rbSpotify.Size = new System.Drawing.Size(57, 17);
            this.rbSpotify.TabIndex = 0;
            this.rbSpotify.TabStop = true;
            this.rbSpotify.Text = "Spotify";
            this.rbSpotify.UseVisualStyleBackColor = true;
            this.rbSpotify.CheckedChanged += new System.EventHandler(this.rbSpotify_CheckedChanged);
            // 
            // btnExportToSpotify
            // 
            this.btnExportToSpotify.Location = new System.Drawing.Point(797, 56);
            this.btnExportToSpotify.Name = "btnExportToSpotify";
            this.btnExportToSpotify.Size = new System.Drawing.Size(104, 23);
            this.btnExportToSpotify.TabIndex = 3;
            this.btnExportToSpotify.Text = "Export";
            this.btnExportToSpotify.UseVisualStyleBackColor = true;
            this.btnExportToSpotify.Click += new System.EventHandler(this.btnExportToSpotify_Click);
            // 
            // btnRotation
            // 
            this.btnRotation.Location = new System.Drawing.Point(40, 56);
            this.btnRotation.Name = "btnRotation";
            this.btnRotation.Size = new System.Drawing.Size(75, 23);
            this.btnRotation.TabIndex = 2;
            this.btnRotation.Text = "Rotation";
            this.btnRotation.UseVisualStyleBackColor = true;
            this.btnRotation.Click += new System.EventHandler(this.btnRotation_Click);
            // 
            // btnFavourites
            // 
            this.btnFavourites.Location = new System.Drawing.Point(121, 27);
            this.btnFavourites.Name = "btnFavourites";
            this.btnFavourites.Size = new System.Drawing.Size(75, 23);
            this.btnFavourites.TabIndex = 1;
            this.btnFavourites.Text = "Favourites";
            this.btnFavourites.UseVisualStyleBackColor = true;
            this.btnFavourites.Click += new System.EventHandler(this.btnFavourites_Click);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(40, 27);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(75, 23);
            this.btnRandom.TabIndex = 0;
            this.btnRandom.Text = "Random";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // cbRelativePaths
            // 
            this.cbRelativePaths.AutoSize = true;
            this.cbRelativePaths.Location = new System.Drawing.Point(737, 12);
            this.cbRelativePaths.Name = "cbRelativePaths";
            this.cbRelativePaths.Size = new System.Drawing.Size(164, 17);
            this.cbRelativePaths.TabIndex = 6;
            this.cbRelativePaths.Text = "Use relative paths in M3U file";
            this.cbRelativePaths.UseVisualStyleBackColor = true;
            // 
            // PlaylistGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 533);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvSongs);
            this.Name = "PlaylistGenerator";
            this.Text = "PlaylistGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSongs)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSongs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRotation;
        private System.Windows.Forms.Button btnFavourites;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Button btnExportToSpotify;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLocal;
        private System.Windows.Forms.RadioButton rbSpotify;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbQualityWhatever;
        private System.Windows.Forms.RadioButton rbQualityMp3;
        private System.Windows.Forms.RadioButton rbQualityFlac;
        private System.Windows.Forms.CheckBox cbRelativePaths;
    }
}