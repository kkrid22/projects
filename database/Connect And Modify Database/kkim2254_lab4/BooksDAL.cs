using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectionAndModification
{
    public struct Books
    {
        int kid;
        Author szid;
        string cim;

        public int Kid
        {
            get { return kid; }
            set { kid = value; }
        }

        public Author Szid
        {
            get { return szid; }
            set { szid = value; }
        }

        public string Cim
        {
            get { return cim; }
            set { cim = value; }
        }

        public Books(int kid, Author szid, string cim)
        {
            this.kid = kid;
            this.szid = szid;
            this.cim = cim;
        }
    }

    public class BooksDAL : DALGen
    {

        public BooksDAL(ref string error)
        {
            base.CreateConnection(ref error);
        }

        public List<Books> GetBookListDataSetParameterized(int SzerzoID, string konyCim, ref string error)
        {

            DataSet ds_tabla = ExecuteParameterizedQuery(SzerzoID, konyCim, ref error);

            List<Books> books = new List<Books>();

            if (error == "OK")
            {
                Books item = new Books();
                foreach (DataRow r in ds_tabla.Tables[0].Rows)
                {
                    try
                    {

                        item.Szid = new Author(Convert.ToInt32(r["SZID"]), (DateTime)r["szuletett"], r["szerzoNev"].ToString());
                        item.Cim = Convert.ToString(r["cim"]);
                        item.Kid = Convert.ToInt32(r["KID"]);

                    }
                    catch (Exception ex)
                    {
                        error = "Invalid data" + ex.Message;
                    }
                    books.Add(item);
                }
            }

            return books;

        }
    }
}
