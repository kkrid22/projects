using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DatabaseConnectionAndModification
{
    public partial class LoginForm : Form
    {
        private string errMess;
        private AdminForm adminForm;
        private GuestForm guestForm;
        private UserForm userForm;
        private UserDAL userDAL;
        private int errNumber;
        public LoginForm()
        {
            InitializeComponent();
            string error = string.Empty;
            userDAL = new UserDAL(ref error);
            if (error != "OK")
            {
                errNumber = 1;
                errMess = "Errro" + errNumber + Environment.NewLine + "Books objektumot nem tudtam letrehozni. " + error;
            }
            else
            {
                errMess = "OK";
            }
        }

        private void loginClicked(object sender, EventArgs e) 
        {
            int result = -1;
            if (tbUsername.Text == null || tbPassword.Text == null)
            {
                MessageBox.Show("nem szabad ures field legyen");
            }
            else 
            {
                result = userDAL.loginExec(tbUsername.Text,tbPassword.Text,ref errMess); 
                if (result == -1) 
                {
                    MessageBox.Show("Sajnalom rossz felhasznalo vagy jelszo");
                }
                else if (result == 0)
                {
                    this.Hide();
                    adminForm = new AdminForm();
                    adminForm.ShowDialog();
                    this.Close();
                }
                else if (result == 1)
                {
                    this.Hide();
                    userForm = new UserForm();
                    userForm.ShowDialog();
                    this.Close();
                }
                else if (result == 2)
                {
                    this.Hide();
                    guestForm = new GuestForm();
                    guestForm.ShowDialog();
                    this.Close();
                }
            }
        }

        private void newSignIn(object sender, EventArgs e) 
        {
            this.Hide();
            SignInForm form = new SignInForm();
            form.ShowDialog();
            this.Close();
        }
    }
}
