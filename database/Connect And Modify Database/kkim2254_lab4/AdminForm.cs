using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DatabaseConnectionAndModification
{
    public partial class AdminForm : Form
    {
        private BooksDAL booksDAL;
        private AuthorDAL authorDAL;
        private string errMess;
        private int errNumber;
        private UserDAL userDAL;
        private SignInForm signInForm;

        public AdminForm()
        {
            InitializeComponent();
            string error = string.Empty;
            booksDAL = new BooksDAL(ref error);
            userDAL = new UserDAL(ref error);
            if (error != "OK")
            {
                errNumber = 1;
                errMess = "Errro" + errNumber + Environment.NewLine + "Books objektumot nem tudtam letrehozni. " + error;
            }
            else
            {
                errMess = "OK";
                authorDAL = new AuthorDAL();
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            if (errMess == "OK")
            {
                FillCbAuthors();

                FillDgvBooks(-1, null);
            }
        }

        private void CurrentList(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count != 0)
            {
                int SzerzoID = (int)dgvBooks.SelectedRows[0].Cells[3].Value;
                string SzerzoNev = dgvBooks.SelectedRows[0].Cells[1].Value.ToString();
                //kell ellenorizni hogy ugyanaz e ez mint ami van a rendszerben

                string returnedSzerzo = authorDAL.SzerzoTablaban(SzerzoID, ref errMess);

                if (returnedSzerzo.Equals(SzerzoNev))
                {
                    lblAuthorName.Text = returnedSzerzo;
                }
            }

        }

        private void logoutClick(object sender, EventArgs e) 
        {
            this.Hide();
            signInForm = new SignInForm();
            signInForm.ShowDialog();
            this.Close();
        }

        private void FillCbAuthors()
        {
            string error = string.Empty;
            List<Author> authorList = authorDAL.GetAuthorList(ref error);

            if (error != "OK")
            {
                errNumber++;
                if (errMess == "OK")
                {
                    errMess = string.Empty;
                }
                errMess = errMess + Environment.NewLine +
                    "Error" + errNumber + Environment.NewLine + "Hiba a ComboBox feltoltesenel." + error;
            }
            else
            {
                cbAuthorFilter.DataSource = authorList;

                cbAuthorFilter.DisplayMember = "SzerzoNev";

                cbAuthorFilter.ValueMember = "SZID";
            }

        }

        private void FillDgvBooks(int szerzoID, string konyvCim)
        {
            string error = string.Empty;
            dgvBooks.Rows.Clear();
            List<Books> bookList = booksDAL.GetBookListDataSetParameterized(szerzoID, konyvCim, ref error);

            if ((bookList.Count != 0) && (error == "OK"))
            {
                foreach (Books item in bookList)
                {

                    try
                    {
                        dgvBooks.Rows.Add(
                                          item.Cim,
                                          item.Szid.SzerzoNev,
                                          item.Szid.Szuletett,
                                          item.Szid.Szid
                                          );
                    }
                    catch (Exception ex)
                    {
                        errNumber++;
                        if (errMess == "OK") errMess = string.Empty;
                        errMess = errMess + Environment.NewLine +
                            "Error" + errNumber + Environment.NewLine + "Invalid data " + ex.Message;
                    }
                }
            }
            else if (error != "OK")
            {
                errNumber++;
                if (errMess == "OK") errMess = string.Empty;
                errMess = errMess + Environment.NewLine +
                    "Error" + errNumber + Environment.NewLine + "Hiba a DataGridView feltoltesenel." + error;
            }
        }



        private void guestToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            this.Hide();
            GuestForm guestForm = new GuestForm();
            guestForm.ShowDialog();
            this.Close();
        }
        private void userToolStripMenuItem_Click( object sender, EventArgs e) 
        {
            this.Hide();
            UserForm userForm = new UserForm();
            userForm.ShowDialog();
            this.Close();
        }

        private void btnExit_click(object sender, EventArgs e) 
        {
            Application.Exit();
        }

        private void DeleteRow(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count != 0)
            {
                string szerzoNev = Convert.ToString(dgvBooks.SelectedRows[0].Cells["SzerzoNev"].Value);
                DateTime szuletett = Convert.ToDateTime(dgvBooks.SelectedRows[0].Cells["SzerzoSzul"].Value);
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    authorDAL.DeleteAuthor(szerzoNev, szuletett, ref errMess);

                    if (errMess == "OK")
                    {
                        MessageBox.Show("Sikerult kitorolni a szemelyt");
                        FillDgvBooks(-1, null);
                    }
                    else
                    {
                        Console.WriteLine(errMess);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nincs kivalasztott sor");
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FillDgvBooks(Convert.ToInt32(cbAuthorFilter.SelectedValue), tbCim.Text);

            if (errMess != "OK")
            {
                ErrorForm errorForm = new ErrorForm(errMess);
                errorForm.Show();
                errorForm.Focus();
            }
        }

        private void AdminUpdate(object sender, EventArgs e)
        {
            if (tbNewAuthor != null)
            {
                if (dgvBooks.SelectedRows.Count != 0)
                {
                    int SzerzoID = (int)dgvBooks.SelectedRows[0].Cells[3].Value;
                    string SzerzoNev = dgvBooks.SelectedRows[0].Cells[1].Value.ToString();
                 
                    string returnedSzerzo = authorDAL.SzerzoTablaban(SzerzoID, ref errMess);

                    if (returnedSzerzo.Equals(SzerzoNev))
                    {
                        string ujSzerzoNev = tbNewAuthor.Text;
                        string szerzoNev = Convert.ToString(dgvBooks.SelectedRows[0].Cells["SzerzoNev"].Value);
                        DateTime szuletett = Convert.ToDateTime(dgvBooks.SelectedRows[0].Cells["SzerzoSzul"].Value);

                        authorDAL.UpdateWriter(ujSzerzoNev, szerzoNev, szuletett, ref errMess);

                        if (errMess == "OK")
                        {
                            MessageBox.Show("Sikerult updatelni a szemely nevet");
                            FillDgvBooks(-1, null);
                        }
                        else
                        {
                            Console.WriteLine(errMess);
                        }
                    }
                    else
                    {
                        lblCurrent.Text = returnedSzerzo;
                        lblOld.Text = SzerzoNev;
                        lblNew.Text = tbNewAuthor.Text;

                        string updateName = null;

                        if (rbOld.Checked)
                        {
                            updateName = SzerzoNev;
                        }
                        else if (rbCurrent.Checked)
                        {
                            updateName = returnedSzerzo;
                        }
                        else if (rbNew.Checked)
                        {
                            updateName = tbNewAuthor.Text;
                        }

                        if (updateName != null)
                        {
                            string ujSzerzoNev = updateName;
                            string szerzoNev = authorDAL.SzerzoTablaban(SzerzoID, ref errMess);
                            DateTime szuletett = Convert.ToDateTime(dgvBooks.SelectedRows[0].Cells["SzerzoSzul"].Value);

                            authorDAL.UpdateWriter(ujSzerzoNev, szerzoNev, szuletett, ref errMess);

                            if (errMess == "OK")
                            {
                                MessageBox.Show("Sikerult updatelni a szemely nevet");
                                FillDgvBooks(-1, null);
                            }
                            else
                            {
                                Console.WriteLine(errMess);
                            }
                        }
                        if (rbOld.Checked)
                        {
                            rbOld.Checked = false;
                        }
                        else if (rbCurrent.Checked)
                        {
                            rbCurrent.Checked = false;
                        }
                        else if (rbNew.Checked)
                        {
                            rbNew.Checked = false;
                        }
                        updateName = null;
                    }
                }
                else
                {
                    MessageBox.Show("Nem volt kivalasztva semmilyen sor sem");
                }
            }
        }

        private void btnUserIsert_Click(object sender, EventArgs e) 
        {
            if (tbUserName == null || tbUserPassword == null || tbGroupID == null)
            {
                MessageBox.Show("Kerem adjon meg minden kotelezo fieldet");
            }
            else 
            {
                int csoportID;
                if (tbGroupID.Text == "Admin")
                {
                    csoportID = 0;
                }
                else if (tbGroupID.Text == "User")
                {
                    csoportID = 1;
                }
                else if (tbGroupID.Text == "Guest")
                {
                    csoportID = 2;
                }
                else 
                {
                    csoportID=-1;
                    MessageBox.Show("A csoport nev erteke vagy Admin vagy User vagy Guest kell legyen");
                }

                if (csoportID >= 0) 
                {
                    userDAL.insertUser(tbUserName.Text, tbUserPassword.Text, csoportID, ref errMess);

                    if (errMess == "OK")
                    {
                        MessageBox.Show("Sikerult beszurni az uj usert");
                        FillDgvBooks(-1, null);
                    }
                    else
                    {
                        Console.WriteLine(errMess);
                    }
                }

            }
        }
    }
}
