namespace DatabaseConnectionAndModification
{
    partial class SignInForm
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
            this.btnSignin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNev = new System.Windows.Forms.TextBox();
            this.tbFelhaszNev = new System.Windows.Forms.TextBox();
            this.tbTelefon = new System.Windows.Forms.TextBox();
            this.tbJelszo = new System.Windows.Forms.TextBox();
            this.tbJelszoUjra = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loginTsm = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSignin
            // 
            this.btnSignin.Location = new System.Drawing.Point(327, 313);
            this.btnSignin.Name = "btnSignin";
            this.btnSignin.Size = new System.Drawing.Size(100, 35);
            this.btnSignin.TabIndex = 0;
            this.btnSignin.Text = "Sign In";
            this.btnSignin.UseVisualStyleBackColor = true;
            this.btnSignin.Click += new System.EventHandler(this.insertSignedIn);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nev:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Telefonszam:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Felhasznalo nev:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Jelszo:";
            // 
            // tbNev
            // 
            this.tbNev.Location = new System.Drawing.Point(327, 53);
            this.tbNev.Name = "tbNev";
            this.tbNev.Size = new System.Drawing.Size(100, 22);
            this.tbNev.TabIndex = 5;
            // 
            // tbFelhaszNev
            // 
            this.tbFelhaszNev.Location = new System.Drawing.Point(327, 165);
            this.tbFelhaszNev.Name = "tbFelhaszNev";
            this.tbFelhaszNev.Size = new System.Drawing.Size(100, 22);
            this.tbFelhaszNev.TabIndex = 6;
            // 
            // tbTelefon
            // 
            this.tbTelefon.Location = new System.Drawing.Point(327, 109);
            this.tbTelefon.Name = "tbTelefon";
            this.tbTelefon.Size = new System.Drawing.Size(100, 22);
            this.tbTelefon.TabIndex = 7;
            // 
            // tbJelszo
            // 
            this.tbJelszo.Location = new System.Drawing.Point(327, 211);
            this.tbJelszo.Name = "tbJelszo";
            this.tbJelszo.Size = new System.Drawing.Size(100, 22);
            this.tbJelszo.TabIndex = 8;
            // 
            // tbJelszoUjra
            // 
            this.tbJelszoUjra.Location = new System.Drawing.Point(327, 254);
            this.tbJelszoUjra.Name = "tbJelszoUjra";
            this.tbJelszoUjra.Size = new System.Drawing.Size(100, 22);
            this.tbJelszoUjra.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "ismet jelszo:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginTsm});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // loginTsm
            // 
            this.loginTsm.Name = "loginTsm";
            this.loginTsm.Size = new System.Drawing.Size(64, 24);
            this.loginTsm.Text = "Log in";
            this.loginTsm.Click += new System.EventHandler(this.newLoginClick);
            // 
            // SignInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbJelszoUjra);
            this.Controls.Add(this.tbJelszo);
            this.Controls.Add(this.tbTelefon);
            this.Controls.Add(this.tbFelhaszNev);
            this.Controls.Add(this.tbNev);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSignin);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SignInForm";
            this.Text = "SignInForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSignin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNev;
        private System.Windows.Forms.TextBox tbFelhaszNev;
        private System.Windows.Forms.TextBox tbTelefon;
        private System.Windows.Forms.TextBox tbJelszo;
        private System.Windows.Forms.TextBox tbJelszoUjra;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loginTsm;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}