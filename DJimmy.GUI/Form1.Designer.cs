namespace DJimmy.GUI
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.dgvSongs = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookUpInSpotifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbLastFmWhatever = new System.Windows.Forms.RadioButton();
            this.rbLastFmNo = new System.Windows.Forms.RadioButton();
            this.rbLastFmYes = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbSpotifyWhatever = new System.Windows.Forms.RadioButton();
            this.rbSpotifyNo = new System.Windows.Forms.RadioButton();
            this.rbSpotifyYes = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLocalWhatever = new System.Windows.Forms.RadioButton();
            this.rbLocalNo = new System.Windows.Forms.RadioButton();
            this.rbLocalYes = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aliasManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playbacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToSpotifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLibrarySyncStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbLibrarySync = new System.Windows.Forms.ToolStripProgressBar();
            this.lbSpotifyLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRowCount = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSongs)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSongs
            // 
            this.dgvSongs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSongs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSongs.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvSongs.Location = new System.Drawing.Point(0, 80);
            this.dgvSongs.Name = "dgvSongs";
            this.dgvSongs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSongs.Size = new System.Drawing.Size(906, 459);
            this.dgvSongs.TabIndex = 0;
            this.dgvSongs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dgvSongs.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvSongs_DataBindingComplete);
            this.dgvSongs.DoubleClick += new System.EventHandler(this.dgvSongs_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.lookUpInSpotifyToolStripMenuItem,
            this.renameToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 70);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // lookUpInSpotifyToolStripMenuItem
            // 
            this.lookUpInSpotifyToolStripMenuItem.Name = "lookUpInSpotifyToolStripMenuItem";
            this.lookUpInSpotifyToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.lookUpInSpotifyToolStripMenuItem.Text = "Add to Spotify ...";
            this.lookUpInSpotifyToolStripMenuItem.Click += new System.EventHandler(this.lookUpInSpotifyToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbFilter);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 55);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbLastFmWhatever);
            this.groupBox3.Controls.Add(this.rbLastFmNo);
            this.groupBox3.Controls.Add(this.rbLastFmYes);
            this.groupBox3.Location = new System.Drawing.Point(695, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(188, 45);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ever played";
            // 
            // rbLastFmWhatever
            // 
            this.rbLastFmWhatever.AutoSize = true;
            this.rbLastFmWhatever.Checked = true;
            this.rbLastFmWhatever.Location = new System.Drawing.Point(113, 19);
            this.rbLastFmWhatever.Name = "rbLastFmWhatever";
            this.rbLastFmWhatever.Size = new System.Drawing.Size(72, 17);
            this.rbLastFmWhatever.TabIndex = 2;
            this.rbLastFmWhatever.TabStop = true;
            this.rbLastFmWhatever.Text = "Whatever";
            this.rbLastFmWhatever.UseVisualStyleBackColor = true;
            // 
            // rbLastFmNo
            // 
            this.rbLastFmNo.AutoSize = true;
            this.rbLastFmNo.Location = new System.Drawing.Point(68, 19);
            this.rbLastFmNo.Name = "rbLastFmNo";
            this.rbLastFmNo.Size = new System.Drawing.Size(39, 17);
            this.rbLastFmNo.TabIndex = 1;
            this.rbLastFmNo.TabStop = true;
            this.rbLastFmNo.Text = "No";
            this.rbLastFmNo.UseVisualStyleBackColor = true;
            // 
            // rbLastFmYes
            // 
            this.rbLastFmYes.AutoSize = true;
            this.rbLastFmYes.Location = new System.Drawing.Point(19, 20);
            this.rbLastFmYes.Name = "rbLastFmYes";
            this.rbLastFmYes.Size = new System.Drawing.Size(43, 17);
            this.rbLastFmYes.TabIndex = 0;
            this.rbLastFmYes.TabStop = true;
            this.rbLastFmYes.Text = "Yes";
            this.rbLastFmYes.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Filter:";
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(61, 14);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(219, 20);
            this.tbFilter.TabIndex = 5;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbSpotifyWhatever);
            this.groupBox2.Controls.Add(this.rbSpotifyNo);
            this.groupBox2.Controls.Add(this.rbSpotifyYes);
            this.groupBox2.Location = new System.Drawing.Point(305, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 45);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "In spotify library";
            // 
            // rbSpotifyWhatever
            // 
            this.rbSpotifyWhatever.AutoSize = true;
            this.rbSpotifyWhatever.Checked = true;
            this.rbSpotifyWhatever.Location = new System.Drawing.Point(113, 19);
            this.rbSpotifyWhatever.Name = "rbSpotifyWhatever";
            this.rbSpotifyWhatever.Size = new System.Drawing.Size(72, 17);
            this.rbSpotifyWhatever.TabIndex = 2;
            this.rbSpotifyWhatever.TabStop = true;
            this.rbSpotifyWhatever.Text = "Whatever";
            this.rbSpotifyWhatever.UseVisualStyleBackColor = true;
            this.rbSpotifyWhatever.CheckedChanged += new System.EventHandler(this.cbShowSpotifySongs_CheckedChanged);
            // 
            // rbSpotifyNo
            // 
            this.rbSpotifyNo.AutoSize = true;
            this.rbSpotifyNo.Location = new System.Drawing.Point(68, 19);
            this.rbSpotifyNo.Name = "rbSpotifyNo";
            this.rbSpotifyNo.Size = new System.Drawing.Size(39, 17);
            this.rbSpotifyNo.TabIndex = 1;
            this.rbSpotifyNo.TabStop = true;
            this.rbSpotifyNo.Text = "No";
            this.rbSpotifyNo.UseVisualStyleBackColor = true;
            this.rbSpotifyNo.CheckedChanged += new System.EventHandler(this.cbShowSpotifySongs_CheckedChanged);
            // 
            // rbSpotifyYes
            // 
            this.rbSpotifyYes.AutoSize = true;
            this.rbSpotifyYes.Location = new System.Drawing.Point(19, 20);
            this.rbSpotifyYes.Name = "rbSpotifyYes";
            this.rbSpotifyYes.Size = new System.Drawing.Size(43, 17);
            this.rbSpotifyYes.TabIndex = 0;
            this.rbSpotifyYes.TabStop = true;
            this.rbSpotifyYes.Text = "Yes";
            this.rbSpotifyYes.UseVisualStyleBackColor = true;
            this.rbSpotifyYes.CheckedChanged += new System.EventHandler(this.cbShowSpotifySongs_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbLocalWhatever);
            this.groupBox1.Controls.Add(this.rbLocalNo);
            this.groupBox1.Controls.Add(this.rbLocalYes);
            this.groupBox1.Location = new System.Drawing.Point(501, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 45);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Has local files";
            // 
            // rbLocalWhatever
            // 
            this.rbLocalWhatever.AutoSize = true;
            this.rbLocalWhatever.Checked = true;
            this.rbLocalWhatever.Location = new System.Drawing.Point(113, 19);
            this.rbLocalWhatever.Name = "rbLocalWhatever";
            this.rbLocalWhatever.Size = new System.Drawing.Size(72, 17);
            this.rbLocalWhatever.TabIndex = 2;
            this.rbLocalWhatever.TabStop = true;
            this.rbLocalWhatever.Text = "Whatever";
            this.rbLocalWhatever.UseVisualStyleBackColor = true;
            this.rbLocalWhatever.CheckedChanged += new System.EventHandler(this.cbShowSpotifySongs_CheckedChanged);
            // 
            // rbLocalNo
            // 
            this.rbLocalNo.AutoSize = true;
            this.rbLocalNo.Location = new System.Drawing.Point(68, 19);
            this.rbLocalNo.Name = "rbLocalNo";
            this.rbLocalNo.Size = new System.Drawing.Size(39, 17);
            this.rbLocalNo.TabIndex = 1;
            this.rbLocalNo.TabStop = true;
            this.rbLocalNo.Text = "No";
            this.rbLocalNo.UseVisualStyleBackColor = true;
            this.rbLocalNo.CheckedChanged += new System.EventHandler(this.cbShowSpotifySongs_CheckedChanged);
            // 
            // rbLocalYes
            // 
            this.rbLocalYes.AutoSize = true;
            this.rbLocalYes.Location = new System.Drawing.Point(19, 20);
            this.rbLocalYes.Name = "rbLocalYes";
            this.rbLocalYes.Size = new System.Drawing.Size(43, 17);
            this.rbLocalYes.TabIndex = 0;
            this.rbLocalYes.TabStop = true;
            this.rbLocalYes.Text = "Yes";
            this.rbLocalYes.UseVisualStyleBackColor = true;
            this.rbLocalYes.CheckedChanged += new System.EventHandler(this.cbShowSpotifySongs_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewsToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(906, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.loadToolStripMenuItem.Text = "Import from ITunes";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveToolStripMenuItem.Text = "Import folder";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.reloadToolStripMenuItem.Text = "Rebuild library";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewsToolStripMenuItem
            // 
            this.viewsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicatesToolStripMenuItem,
            this.playlistGeneratorToolStripMenuItem,
            this.aliasManagerToolStripMenuItem,
            this.playbacksToolStripMenuItem});
            this.viewsToolStripMenuItem.Name = "viewsToolStripMenuItem";
            this.viewsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.viewsToolStripMenuItem.Text = "Views";
            // 
            // duplicatesToolStripMenuItem
            // 
            this.duplicatesToolStripMenuItem.Name = "duplicatesToolStripMenuItem";
            this.duplicatesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.duplicatesToolStripMenuItem.Text = "Options";
            this.duplicatesToolStripMenuItem.Click += new System.EventHandler(this.duplicatesToolStripMenuItem_Click);
            // 
            // playlistGeneratorToolStripMenuItem
            // 
            this.playlistGeneratorToolStripMenuItem.Name = "playlistGeneratorToolStripMenuItem";
            this.playlistGeneratorToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.playlistGeneratorToolStripMenuItem.Text = "Playlist generator";
            this.playlistGeneratorToolStripMenuItem.Click += new System.EventHandler(this.playlistGeneratorToolStripMenuItem_Click);
            // 
            // aliasManagerToolStripMenuItem
            // 
            this.aliasManagerToolStripMenuItem.Name = "aliasManagerToolStripMenuItem";
            this.aliasManagerToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.aliasManagerToolStripMenuItem.Text = "Alias Manager";
            this.aliasManagerToolStripMenuItem.Click += new System.EventHandler(this.aliasManagerToolStripMenuItem_Click);
            // 
            // playbacksToolStripMenuItem
            // 
            this.playbacksToolStripMenuItem.Name = "playbacksToolStripMenuItem";
            this.playbacksToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.playbacksToolStripMenuItem.Text = "Playbacks";
            this.playbacksToolStripMenuItem.Click += new System.EventHandler(this.playbacksToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syncToolStripMenuItem,
            this.loginToSpotifyToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // syncToolStripMenuItem
            // 
            this.syncToolStripMenuItem.Name = "syncToolStripMenuItem";
            this.syncToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.syncToolStripMenuItem.Text = "Sync";
            this.syncToolStripMenuItem.Click += new System.EventHandler(this.syncToolStripMenuItem_Click);
            // 
            // loginToSpotifyToolStripMenuItem
            // 
            this.loginToSpotifyToolStripMenuItem.Name = "loginToSpotifyToolStripMenuItem";
            this.loginToSpotifyToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.loginToSpotifyToolStripMenuItem.Text = "Login to Spotify";
            this.loginToSpotifyToolStripMenuItem.Click += new System.EventHandler(this.loginToSpotifyToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLibrarySyncStatus,
            this.pbLibrarySync,
            this.lbSpotifyLogin,
            this.toolStripStatusLabel1,
            this.lblRowCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 542);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(906, 22);
            this.statusStrip1.TabIndex = 3;
            // 
            // lblLibrarySyncStatus
            // 
            this.lblLibrarySyncStatus.AutoSize = false;
            this.lblLibrarySyncStatus.Name = "lblLibrarySyncStatus";
            this.lblLibrarySyncStatus.Size = new System.Drawing.Size(250, 17);
            // 
            // pbLibrarySync
            // 
            this.pbLibrarySync.Name = "pbLibrarySync";
            this.pbLibrarySync.Size = new System.Drawing.Size(100, 16);
            this.pbLibrarySync.Visible = false;
            // 
            // lbSpotifyLogin
            // 
            this.lbSpotifyLogin.Name = "lbSpotifyLogin";
            this.lbSpotifyLogin.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(641, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // lblRowCount
            // 
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 564);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvSongs);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Spotify Library";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSongs)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSongs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbSpotifyWhatever;
        private System.Windows.Forms.RadioButton rbSpotifyNo;
        private System.Windows.Forms.RadioButton rbSpotifyYes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLocalWhatever;
        private System.Windows.Forms.RadioButton rbLocalNo;
        private System.Windows.Forms.RadioButton rbLocalYes;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblLibrarySyncStatus;
        private System.Windows.Forms.ToolStripMenuItem loginToSpotifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblRowCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.ToolStripMenuItem aliasManagerToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lookUpInSpotifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lbSpotifyLogin;
        private System.Windows.Forms.ToolStripProgressBar pbLibrarySync;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbLastFmWhatever;
        private System.Windows.Forms.RadioButton rbLastFmNo;
        private System.Windows.Forms.RadioButton rbLastFmYes;
        private System.Windows.Forms.ToolStripMenuItem playbacksToolStripMenuItem;
    }
}

