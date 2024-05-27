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
    public partial class GuestForm : Form
    {
        private BooksDAL booksDAL;
        private AuthorDAL authorDAL;
        private string errMess;
        private int errNumber;
        private SignInForm signInForm;
        public GuestForm()
        {
            InitializeComponent();
            string error = string.Empty;
            booksDAL = new BooksDAL(ref error);
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
     

        private void GuestForm_Load(object sender, EventArgs e)
        {
            if (errMess == "OK")
            {
                FillCbAuthors();

                FillDgvBooks(-1, null);
            }
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
                                          item.Szid.Szuletett
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Occurs whenever the form is first shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuestForm_Shown(object sender, EventArgs e)
        {
            if (errMess != "OK")
            {
                ErrorForm errorForm = new ErrorForm(errMess);
                errorForm.Show();
                errorForm.Focus();
            }
        }

        private void logoutClick(object sender, EventArgs e)
        {
            this.Hide();
            signInForm = new SignInForm();
            signInForm.ShowDialog();
            this.Close();
        }
    }
}
