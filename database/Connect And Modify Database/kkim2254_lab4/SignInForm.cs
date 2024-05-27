using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseConnectionAndModification
{
    public partial class SignInForm : Form
    {
        private UserDAL user;
        private string errMess;
        private int errNumber;
        public SignInForm()
        {
            InitializeComponent();
            string error = string.Empty;
            user = new UserDAL(ref error);
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

        private void newLoginClick(object sender, EventArgs e) 
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Close();
        }

        private void insertSignedIn(object sender, EventArgs e)
        {
            if (tbFelhaszNev.Text == null || tbNev.Text == null || tbJelszo.Text == null || tbJelszoUjra.Text == null || tbTelefon.Text == null)
            {
                MessageBox.Show("kerlek adj meg minden mezot");
            }
            else if (tbJelszo.Text != tbJelszoUjra.Text)
            {
                MessageBox.Show("a ket megadott jelszo nem egyezzik");
            }
            else 
            {
                int result = user.findRegistered(tbFelhaszNev.Text,ref errMess);

                if (result != 1)
                {
                    user.procedureExec(tbNev.Text, tbTelefon.Text, tbFelhaszNev.Text, tbJelszo.Text, ref errMess);

                    if (errMess == "OK")
                    {
                        MessageBox.Show("Sikerult beszurni az uj usert");
                        this.Hide();
                        GuestForm guestForm = new GuestForm();
                        guestForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        Console.WriteLine(errMess);
                    }
                }
                else 
                {
                    MessageBox.Show("Ez a felhasznalo nev mar foglalt");
                }
            }

        }
    }
}
