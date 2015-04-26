namespace DJimmy.GUI
{
    partial class AliasManager
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
            this.dgvTitleAlias = new System.Windows.Forms.DataGridView();
            this.dgvArtistAlias = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTitleAlias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtistAlias)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTitleAlias
            // 
            this.dgvTitleAlias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTitleAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTitleAlias.Location = new System.Drawing.Point(3, 3);
            this.dgvTitleAlias.Name = "dgvTitleAlias";
            this.dgvTitleAlias.Size = new System.Drawing.Size(743, 385);
            this.dgvTitleAlias.TabIndex = 0;
            // 
            // dgvArtistAlias
            // 
            this.dgvArtistAlias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArtistAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvArtistAlias.Location = new System.Drawing.Point(3, 3);
            this.dgvArtistAlias.Name = "dgvArtistAlias";
            this.dgvArtistAlias.Size = new System.Drawing.Size(743, 385);
            this.dgvArtistAlias.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(685, 431);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(757, 417);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvTitleAlias);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(749, 391);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Title aliases";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvArtistAlias);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(749, 391);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Artist aliases";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // AliasManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 459);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSave);
            this.Name = "AliasManager";
            this.Text = "AliasManager";
            this.Load += new System.EventHandler(this.AliasManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTitleAlias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtistAlias)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTitleAlias;
        private System.Windows.Forms.DataGridView dgvArtistAlias;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}