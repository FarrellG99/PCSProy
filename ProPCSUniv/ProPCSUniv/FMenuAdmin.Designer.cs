namespace ProPCSUniv
{
    partial class FMenuAdmin
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.masterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterJurusanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterMahasiswaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterDosenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterRuanganToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(670, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // masterToolStripMenuItem
            // 
            this.masterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.masterJurusanToolStripMenuItem,
            this.masterMahasiswaToolStripMenuItem,
            this.masterDosenToolStripMenuItem,
            this.masterRuanganToolStripMenuItem});
            this.masterToolStripMenuItem.Name = "masterToolStripMenuItem";
            this.masterToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.masterToolStripMenuItem.Text = "Master";
            // 
            // masterJurusanToolStripMenuItem
            // 
            this.masterJurusanToolStripMenuItem.Name = "masterJurusanToolStripMenuItem";
            this.masterJurusanToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.masterJurusanToolStripMenuItem.Text = "Master Jurusan";
            this.masterJurusanToolStripMenuItem.Click += new System.EventHandler(this.masterJurusanToolStripMenuItem_Click);
            // 
            // masterMahasiswaToolStripMenuItem
            // 
            this.masterMahasiswaToolStripMenuItem.Name = "masterMahasiswaToolStripMenuItem";
            this.masterMahasiswaToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.masterMahasiswaToolStripMenuItem.Text = "Master Mahasiswa";
            this.masterMahasiswaToolStripMenuItem.Click += new System.EventHandler(this.masterMahasiswaToolStripMenuItem_Click);
            // 
            // masterDosenToolStripMenuItem
            // 
            this.masterDosenToolStripMenuItem.Name = "masterDosenToolStripMenuItem";
            this.masterDosenToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.masterDosenToolStripMenuItem.Text = "Master Dosen";
            this.masterDosenToolStripMenuItem.Click += new System.EventHandler(this.masterDosenToolStripMenuItem_Click);
            // 
            // masterRuanganToolStripMenuItem
            // 
            this.masterRuanganToolStripMenuItem.Name = "masterRuanganToolStripMenuItem";
            this.masterRuanganToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.masterRuanganToolStripMenuItem.Text = "Master Ruangan";
            this.masterRuanganToolStripMenuItem.Click += new System.EventHandler(this.masterRuanganToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem1.Text = "Master Periode";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // FMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(670, 312);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FMenuAdmin";
            this.Text = "Menu Admin";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem masterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterJurusanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterMahasiswaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterDosenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterRuanganToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}