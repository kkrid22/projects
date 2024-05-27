using System;

namespace DatabaseConnectionAndModification
{
    partial class AdminForm
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.AdministratorMain = new System.Windows.Forms.ToolStripMenuItem();
            this.Workertsm = new System.Windows.Forms.ToolStripMenuItem();
            this.purchasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Usertsm = new System.Windows.Forms.ToolStripMenuItem();
            this.Guesttsm = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cbAuthorFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCim = new System.Windows.Forms.TextBox();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.KonyvCim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SzerzoNev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SzerzoSzul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SzerzoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbNewAuthor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnGetName = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblAuthorName = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblNew = new System.Windows.Forms.Label();
            this.lblOld = new System.Windows.Forms.Label();
            this.rbOld = new System.Windows.Forms.RadioButton();
            this.rbCurrent = new System.Windows.Forms.RadioButton();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.btnUserInsert = new System.Windows.Forms.Button();
            this.tbGroupID = new System.Windows.Forms.TextBox();
            this.tbUserPassword = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(681, 357);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(125, 56);
            this.btnExit.TabIndex = 40;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(332, 103);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(105, 37);
            this.btnDelete.TabIndex = 41;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.DeleteRow);
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AdministratorMain,
            this.Workertsm,
            this.Usertsm,
            this.Guesttsm});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1268, 28);
            this.menuStrip2.TabIndex = 43;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // AdministratorMain
            // 
            this.AdministratorMain.Name = "AdministratorMain";
            this.AdministratorMain.Size = new System.Drawing.Size(114, 24);
            this.AdministratorMain.Text = "Administrator";
            // 
            // Workertsm
            // 
            this.Workertsm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.purchasesToolStripMenuItem,
            this.insertsToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.userToolStripMenuItem1,
            this.searchToolStripMenuItem});
            this.Workertsm.Name = "Workertsm";
            this.Workertsm.Size = new System.Drawing.Size(70, 24);
            this.Workertsm.Text = "Worker";
            // 
            // purchasesToolStripMenuItem
            // 
            this.purchasesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.purchasesToolStripMenuItem.Name = "purchasesToolStripMenuItem";
            this.purchasesToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.purchasesToolStripMenuItem.Text = "Purchases";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.newToolStripMenuItem.Text = "New";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.updateToolStripMenuItem.Text = "Update";
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.resetToolStripMenuItem.Text = "Reset";
            // 
            // insertsToolStripMenuItem
            // 
            this.insertsToolStripMenuItem.Name = "insertsToolStripMenuItem";
            this.insertsToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.insertsToolStripMenuItem.Text = "Insert";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.removeToolStripMenuItem.Text = "Delete";
            // 
            // userToolStripMenuItem1
            // 
            this.userToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuItem,
            this.updateToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.userToolStripMenuItem1.Name = "userToolStripMenuItem1";
            this.userToolStripMenuItem1.Size = new System.Drawing.Size(156, 26);
            this.userToolStripMenuItem1.Text = "User";
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.insertToolStripMenuItem.Text = "Insert";
            // 
            // updateToolStripMenuItem1
            // 
            this.updateToolStripMenuItem1.Name = "updateToolStripMenuItem1";
            this.updateToolStripMenuItem1.Size = new System.Drawing.Size(141, 26);
            this.updateToolStripMenuItem1.Text = "Update";
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(141, 26);
            this.deleteToolStripMenuItem1.Text = "Delete";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // Usertsm
            // 
            this.Usertsm.Name = "Usertsm";
            this.Usertsm.Size = new System.Drawing.Size(52, 24);
            this.Usertsm.Text = "User";
            this.Usertsm.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
            // 
            // Guesttsm
            // 
            this.Guesttsm.Name = "Guesttsm";
            this.Guesttsm.Size = new System.Drawing.Size(60, 24);
            this.Guesttsm.Text = "Guest";
            this.Guesttsm.Click += new System.EventHandler(this.guestToolStripMenuItem_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(332, 50);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(105, 37);
            this.btnFilter.TabIndex = 44;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // cbAuthorFilter
            // 
            this.cbAuthorFilter.FormattingEnabled = true;
            this.cbAuthorFilter.Location = new System.Drawing.Point(129, 110);
            this.cbAuthorFilter.Name = "cbAuthorFilter";
            this.cbAuthorFilter.Size = new System.Drawing.Size(177, 24);
            this.cbAuthorFilter.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 47;
            this.label1.Text = "Konyv Cime:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 48;
            this.label2.Text = "Szerzo Nev:";
            // 
            // tbCim
            // 
            this.tbCim.Location = new System.Drawing.Point(129, 57);
            this.tbCim.Name = "tbCim";
            this.tbCim.Size = new System.Drawing.Size(177, 22);
            this.tbCim.TabIndex = 49;
            // 
            // dgvBooks
            // 
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KonyvCim,
            this.SzerzoNev,
            this.SzerzoSzul,
            this.SzerzoID});
            this.dgvBooks.Location = new System.Drawing.Point(34, 207);
            this.dgvBooks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.RowHeadersWidth = 51;
            this.dgvBooks.RowTemplate.Height = 24;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.ShowCellToolTips = false;
            this.dgvBooks.Size = new System.Drawing.Size(606, 150);
            this.dgvBooks.TabIndex = 50;
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
            // SzerzoID
            // 
            this.SzerzoID.HeaderText = "SZID";
            this.SzerzoID.MinimumWidth = 6;
            this.SzerzoID.Name = "SzerzoID";
            this.SzerzoID.Visible = false;
            this.SzerzoID.Width = 125;
            // 
            // tbNewAuthor
            // 
            this.tbNewAuthor.Location = new System.Drawing.Point(541, 80);
            this.tbNewAuthor.Name = "tbNewAuthor";
            this.tbNewAuthor.Size = new System.Drawing.Size(148, 22);
            this.tbNewAuthor.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(520, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 16);
            this.label3.TabIndex = 52;
            this.label3.Text = "kivalosztott sor szerzoenek uj neve";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(560, 118);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(104, 30);
            this.btnUpdate.TabIndex = 53;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.AdminUpdate);
            // 
            // btnGetName
            // 
            this.btnGetName.Location = new System.Drawing.Point(866, 79);
            this.btnGetName.Name = "btnGetName";
            this.btnGetName.Size = new System.Drawing.Size(115, 23);
            this.btnGetName.TabIndex = 54;
            this.btnGetName.Text = "Current name";
            this.btnGetName.UseVisualStyleBackColor = true;
            this.btnGetName.Click += new System.EventHandler(this.CurrentList);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(835, 50);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(186, 16);
            this.lblDescription.TabIndex = 55;
            this.lblDescription.Text = "selected authors current name";
            // 
            // lblAuthorName
            // 
            this.lblAuthorName.AutoSize = true;
            this.lblAuthorName.Location = new System.Drawing.Point(862, 118);
            this.lblAuthorName.Name = "lblAuthorName";
            this.lblAuthorName.Size = new System.Drawing.Size(0, 16);
            this.lblAuthorName.TabIndex = 56;
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(733, 193);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(0, 16);
            this.lblCurrent.TabIndex = 57;
            // 
            // lblNew
            // 
            this.lblNew.AutoSize = true;
            this.lblNew.Location = new System.Drawing.Point(735, 219);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(0, 16);
            this.lblNew.TabIndex = 58;
            // 
            // lblOld
            // 
            this.lblOld.AutoSize = true;
            this.lblOld.Location = new System.Drawing.Point(735, 167);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(0, 16);
            this.lblOld.TabIndex = 59;
            // 
            // rbOld
            // 
            this.rbOld.AutoSize = true;
            this.rbOld.Location = new System.Drawing.Point(838, 163);
            this.rbOld.Name = "rbOld";
            this.rbOld.Size = new System.Drawing.Size(49, 20);
            this.rbOld.TabIndex = 60;
            this.rbOld.TabStop = true;
            this.rbOld.Text = "Old";
            this.rbOld.UseVisualStyleBackColor = true;
            // 
            // rbCurrent
            // 
            this.rbCurrent.AutoSize = true;
            this.rbCurrent.Location = new System.Drawing.Point(838, 189);
            this.rbCurrent.Name = "rbCurrent";
            this.rbCurrent.Size = new System.Drawing.Size(70, 20);
            this.rbCurrent.TabIndex = 61;
            this.rbCurrent.TabStop = true;
            this.rbCurrent.Text = "Current";
            this.rbCurrent.UseVisualStyleBackColor = true;
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.Location = new System.Drawing.Point(838, 215);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(55, 20);
            this.rbNew.TabIndex = 62;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "New";
            this.rbNew.UseVisualStyleBackColor = true;
            // 
            // btnUserInsert
            // 
            this.btnUserInsert.Location = new System.Drawing.Point(1139, 167);
            this.btnUserInsert.Name = "btnUserInsert";
            this.btnUserInsert.Size = new System.Drawing.Size(110, 31);
            this.btnUserInsert.TabIndex = 83;
            this.btnUserInsert.Text = "Insert user";
            this.btnUserInsert.UseVisualStyleBackColor = true;
            this.btnUserInsert.Click += new System.EventHandler(this.btnUserIsert_Click);
            // 
            // tbGroupID
            // 
            this.tbGroupID.Location = new System.Drawing.Point(1149, 108);
            this.tbGroupID.Name = "tbGroupID";
            this.tbGroupID.Size = new System.Drawing.Size(100, 22);
            this.tbGroupID.TabIndex = 82;
            // 
            // tbUserPassword
            // 
            this.tbUserPassword.Location = new System.Drawing.Point(1149, 80);
            this.tbUserPassword.Name = "tbUserPassword";
            this.tbUserPassword.Size = new System.Drawing.Size(100, 22);
            this.tbUserPassword.TabIndex = 81;
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(1149, 52);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(100, 22);
            this.tbUserName.TabIndex = 80;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1043, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 16);
            this.label6.TabIndex = 79;
            this.label6.Text = "Csoport nev:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1076, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 16);
            this.label5.TabIndex = 78;
            this.label5.Text = "Jelszo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1090, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 16);
            this.label4.TabIndex = 77;
            this.label4.Text = "Nev:";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(1093, 369);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(152, 56);
            this.btnLogout.TabIndex = 85;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.logoutClick);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUserInsert);
            this.Controls.Add(this.tbGroupID);
            this.Controls.Add(this.tbUserPassword);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rbNew);
            this.Controls.Add(this.rbCurrent);
            this.Controls.Add(this.rbOld);
            this.Controls.Add(this.lblOld);
            this.Controls.Add(this.lblNew);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.lblAuthorName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnGetName);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbNewAuthor);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.tbCim);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbAuthorFilter);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExit);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem AdministratorMain;
        private System.Windows.Forms.ToolStripMenuItem Workertsm;
        private System.Windows.Forms.ToolStripMenuItem purchasesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Usertsm;
        private System.Windows.Forms.ToolStripMenuItem Guesttsm;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cbAuthorFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCim;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.TextBox tbNewAuthor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnGetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn KonyvCim;
        private System.Windows.Forms.DataGridViewTextBoxColumn SzerzoNev;
        private System.Windows.Forms.DataGridViewTextBoxColumn SzerzoSzul;
        private System.Windows.Forms.DataGridViewTextBoxColumn SzerzoID;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblAuthorName;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.Label lblOld;
        private System.Windows.Forms.RadioButton rbOld;
        private System.Windows.Forms.RadioButton rbCurrent;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.Button btnUserInsert;
        private System.Windows.Forms.TextBox tbGroupID;
        private System.Windows.Forms.TextBox tbUserPassword;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLogout;
    }
}