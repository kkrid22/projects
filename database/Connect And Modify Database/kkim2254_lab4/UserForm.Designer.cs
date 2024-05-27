namespace DatabaseConnectionAndModification
{
    partial class UserForm
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
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.Usertsm = new System.Windows.Forms.ToolStripMenuItem();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.rbCurrent = new System.Windows.Forms.RadioButton();
            this.rbOld = new System.Windows.Forms.RadioButton();
            this.lblOld = new System.Windows.Forms.Label();
            this.lblNew = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblAuthorName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnGetName = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNewAuthor = new System.Windows.Forms.TextBox();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.KonyvCim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SzerzoNev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SzerzoSzul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SzerzoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbCim = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAuthorFilter = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Usertsm});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1083, 28);
            this.menuStrip2.TabIndex = 45;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // Usertsm
            // 
            this.Usertsm.Name = "Usertsm";
            this.Usertsm.Size = new System.Drawing.Size(52, 24);
            this.Usertsm.Text = "User";
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.Location = new System.Drawing.Point(853, 209);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(55, 20);
            this.rbNew.TabIndex = 82;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "New";
            this.rbNew.UseVisualStyleBackColor = true;
            // 
            // rbCurrent
            // 
            this.rbCurrent.AutoSize = true;
            this.rbCurrent.Location = new System.Drawing.Point(853, 183);
            this.rbCurrent.Name = "rbCurrent";
            this.rbCurrent.Size = new System.Drawing.Size(70, 20);
            this.rbCurrent.TabIndex = 81;
            this.rbCurrent.TabStop = true;
            this.rbCurrent.Text = "Current";
            this.rbCurrent.UseVisualStyleBackColor = true;
            // 
            // rbOld
            // 
            this.rbOld.AutoSize = true;
            this.rbOld.Location = new System.Drawing.Point(853, 157);
            this.rbOld.Name = "rbOld";
            this.rbOld.Size = new System.Drawing.Size(49, 20);
            this.rbOld.TabIndex = 80;
            this.rbOld.TabStop = true;
            this.rbOld.Text = "Old";
            this.rbOld.UseVisualStyleBackColor = true;
            // 
            // lblOld
            // 
            this.lblOld.AutoSize = true;
            this.lblOld.Location = new System.Drawing.Point(750, 161);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(0, 16);
            this.lblOld.TabIndex = 79;
            // 
            // lblNew
            // 
            this.lblNew.AutoSize = true;
            this.lblNew.Location = new System.Drawing.Point(750, 213);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(0, 16);
            this.lblNew.TabIndex = 78;
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(748, 187);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(0, 16);
            this.lblCurrent.TabIndex = 77;
            // 
            // lblAuthorName
            // 
            this.lblAuthorName.AutoSize = true;
            this.lblAuthorName.Location = new System.Drawing.Point(877, 112);
            this.lblAuthorName.Name = "lblAuthorName";
            this.lblAuthorName.Size = new System.Drawing.Size(0, 16);
            this.lblAuthorName.TabIndex = 76;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(850, 44);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(186, 16);
            this.lblDescription.TabIndex = 75;
            this.lblDescription.Text = "selected authors current name";
            // 
            // btnGetName
            // 
            this.btnGetName.Location = new System.Drawing.Point(881, 73);
            this.btnGetName.Name = "btnGetName";
            this.btnGetName.Size = new System.Drawing.Size(115, 23);
            this.btnGetName.TabIndex = 74;
            this.btnGetName.Text = "Current name";
            this.btnGetName.UseVisualStyleBackColor = true;
            this.btnGetName.Click += new System.EventHandler(this.CurrentList);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(575, 112);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(104, 30);
            this.btnUpdate.TabIndex = 73;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.Update);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(535, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 16);
            this.label3.TabIndex = 72;
            this.label3.Text = "kivalosztott sor szerzoenek uj neve";
            // 
            // tbNewAuthor
            // 
            this.tbNewAuthor.Location = new System.Drawing.Point(556, 74);
            this.tbNewAuthor.Name = "tbNewAuthor";
            this.tbNewAuthor.Size = new System.Drawing.Size(148, 22);
            this.tbNewAuthor.TabIndex = 71;
            // 
            // dgvBooks
            // 
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KonyvCim,
            this.SzerzoNev,
            this.SzerzoSzul,
            this.SzerzoID});
            this.dgvBooks.Location = new System.Drawing.Point(49, 201);
            this.dgvBooks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.RowHeadersWidth = 51;
            this.dgvBooks.RowTemplate.Height = 24;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.ShowCellToolTips = false;
            this.dgvBooks.Size = new System.Drawing.Size(606, 150);
            this.dgvBooks.TabIndex = 70;
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
            // tbCim
            // 
            this.tbCim.Location = new System.Drawing.Point(144, 51);
            this.tbCim.Name = "tbCim";
            this.tbCim.Size = new System.Drawing.Size(177, 22);
            this.tbCim.TabIndex = 69;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "Szerzo Nev:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 67;
            this.label1.Text = "Konyv Cime:";
            // 
            // cbAuthorFilter
            // 
            this.cbAuthorFilter.FormattingEnabled = true;
            this.cbAuthorFilter.Location = new System.Drawing.Point(144, 104);
            this.cbAuthorFilter.Name = "cbAuthorFilter";
            this.cbAuthorFilter.Size = new System.Drawing.Size(177, 24);
            this.cbAuthorFilter.TabIndex = 66;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(347, 44);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(105, 37);
            this.btnFilter.TabIndex = 65;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(347, 97);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(105, 37);
            this.btnDelete.TabIndex = 64;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.DeleteRow);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(696, 351);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(125, 56);
            this.btnExit.TabIndex = 63;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(897, 351);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(152, 56);
            this.btnLogout.TabIndex = 84;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.logoutClick);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 450);
            this.Controls.Add(this.btnLogout);
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
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.menuStrip2);
            this.Name = "UserForm";
            this.Text = "UserForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem Usertsm;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.RadioButton rbCurrent;
        private System.Windows.Forms.RadioButton rbOld;
        private System.Windows.Forms.Label lblOld;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblAuthorName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnGetName;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNewAuthor;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.DataGridViewTextBoxColumn KonyvCim;
        private System.Windows.Forms.DataGridViewTextBoxColumn SzerzoNev;
        private System.Windows.Forms.DataGridViewTextBoxColumn SzerzoSzul;
        private System.Windows.Forms.DataGridViewTextBoxColumn SzerzoID;
        private System.Windows.Forms.TextBox tbCim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAuthorFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogout;
    }
}