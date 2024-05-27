namespace DatabaseConnectionAndModification
{
    partial class GuestForm
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
            this.btnfilter = new System.Windows.Forms.Button();
            this.tbCim = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cbAuthorFilter = new System.Windows.Forms.ComboBox();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.KonyvCim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SzerzoNev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SzerzoSzul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblAuthorName = new System.Windows.Forms.Label();
            this.lblBookName = new System.Windows.Forms.Label();
            this.Guesttsm = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnfilter
            // 
            this.btnfilter.Location = new System.Drawing.Point(436, 70);
            this.btnfilter.Name = "btnfilter";
            this.btnfilter.Size = new System.Drawing.Size(75, 23);
            this.btnfilter.TabIndex = 0;
            this.btnfilter.Text = "Filter";
            this.btnfilter.UseVisualStyleBackColor = true;
            this.btnfilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // tbCim
            // 
            this.tbCim.Location = new System.Drawing.Point(176, 47);
            this.tbCim.Name = "tbCim";
            this.tbCim.Size = new System.Drawing.Size(169, 22);
            this.tbCim.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // cbAuthorFilter
            // 
            this.cbAuthorFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuthorFilter.FormattingEnabled = true;
            this.cbAuthorFilter.Location = new System.Drawing.Point(176, 89);
            this.cbAuthorFilter.Name = "cbAuthorFilter";
            this.cbAuthorFilter.Size = new System.Drawing.Size(169, 24);
            this.cbAuthorFilter.TabIndex = 3;
            // 
            // dgvBooks
            // 
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KonyvCim,
            this.SzerzoNev,
            this.SzerzoSzul});
            this.dgvBooks.Location = new System.Drawing.Point(54, 156);
            this.dgvBooks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.RowHeadersWidth = 51;
            this.dgvBooks.RowTemplate.Height = 24;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.ShowCellToolTips = false;
            this.dgvBooks.Size = new System.Drawing.Size(606, 150);
            this.dgvBooks.TabIndex = 4;
            // 
            // KonyvCim
            // 
            this.KonyvCim.HeaderText = "KonyvCim";
            this.KonyvCim.MinimumWidth = 6;
            this.KonyvCim.Name = "KonyvCim";
            this.KonyvCim.Width = 200;
            // 
            // SzerzoNev
            // 
            this.SzerzoNev.HeaderText = "SzerzoNev";
            this.SzerzoNev.MinimumWidth = 6;
            this.SzerzoNev.Name = "SzerzoNev";
            this.SzerzoNev.Width = 125;
            // 
            // SzerzoSzul
            // 
            this.SzerzoSzul.HeaderText = "SzerzoSzul";
            this.SzerzoSzul.MinimumWidth = 6;
            this.SzerzoSzul.Name = "SzerzoSzul";
            this.SzerzoSzul.Width = 125;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(570, 329);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(112, 35);
            this.btnExit.TabIndex = 28;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            // 
            // lblAuthorName
            // 
            this.lblAuthorName.AutoSize = true;
            this.lblAuthorName.Location = new System.Drawing.Point(82, 92);
            this.lblAuthorName.Name = "lblAuthorName";
            this.lblAuthorName.Size = new System.Drawing.Size(88, 16);
            this.lblAuthorName.TabIndex = 32;
            this.lblAuthorName.Text = "Author Name:";
            // 
            // lblBookName
            // 
            this.lblBookName.AutoSize = true;
            this.lblBookName.Location = new System.Drawing.Point(82, 47);
            this.lblBookName.Name = "lblBookName";
            this.lblBookName.Size = new System.Drawing.Size(85, 16);
            this.lblBookName.TabIndex = 33;
            this.lblBookName.Text = "Book Name: ";
            // 
            // Guesttsm
            // 
            this.Guesttsm.Name = "Guesttsm";
            this.Guesttsm.Size = new System.Drawing.Size(60, 24);
            this.Guesttsm.Text = "Guest";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Guesttsm});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(696, 28);
            this.menuStrip2.TabIndex = 44;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(26, 329);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(138, 35);
            this.btnLogout.TabIndex = 85;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.logoutClick);
            // 
            // GuestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 385);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.lblBookName);
            this.Controls.Add(this.lblAuthorName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.cbAuthorFilter);
            this.Controls.Add(this.tbCim);
            this.Controls.Add(this.btnfilter);
            this.Name = "GuestForm";
            this.Text = "Szures";
            this.Load += new System.EventHandler(this.GuestForm_Load);
            this.Shown += new System.EventHandler(this.GuestForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnfilter;
        private System.Windows.Forms.TextBox tbCim;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ComboBox cbAuthorFilter;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn KonyvCim;
        private System.Windows.Forms.DataGridViewTextBoxColumn SzerzoNev;
        private System.Windows.Forms.DataGridViewTextBoxColumn SzerzoSzul;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.Label lblAuthorName;
        private System.Windows.Forms.Label lblBookName;
        private System.Windows.Forms.ToolStripMenuItem Guesttsm;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.Button btnLogout;
    }
}

