using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectionAndModification
{
    public struct Author
    {
        int szid;
        DateTime szuletett;
        string szerzoNev;

        public int Szid 
        {
            get { return szid; }
            set { szid = value; }
        }

        public DateTime Szuletett 
        {
            get { return szuletett; }
            set { szuletett = value; }
        }

        public string SzerzoNev
        {
            get { return szerzoNev; }
            set { szerzoNev = value;}
        }

        public Author(int szid, DateTime szuletett, string szerzoNev)
        {
            this.szuletett = szuletett;
            this.szid = szid;
            this.szerzoNev = szerzoNev;
        }
    }

    public class AuthorDAL : DALGen
    {
        public List<Author> GetAuthorList(ref string error)
        {
            string query = "SELECT * FROM Szerzok";
            SqlDataReader dataReader = ExecuteReader(query, ref error);

            List<Author> authorList = new List<Author>();

            if (error == "OK") 
            {
                Author item = new Author();
                while (dataReader.Read()) 
                {
                    try 
                    {
                        item.Szid = Convert.ToInt32(dataReader[0]);
                        item.Szuletett = (DateTime)dataReader[1];
                        item.SzerzoNev = dataReader[2].ToString();
                        authorList.Add(item);
                    }
                    catch (Exception e)
                    {
                        error= "Invalid data" + e.Message;
                    }
                }
                DateTime date = DateTime.Now;
                item.Szid = -1;
                item.Szuletett = date;
                item.SzerzoNev = "ALL";
                authorList.Add(item);
            }
            CloseDataReader(dataReader);

            return authorList;
        }

        public void DeleteAuthor(string SzerzoNev, DateTime Szuletett, ref string errMess) 
        {
            string query = "DELETE FROM Szerzok " + 
                            "WHERE Szerzok.Szuletett = CAST(@pSzuletett AS DATE) AND Szerzok.SzerzoNev = @pSzerzoNev";

            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@pSzuletett";
            parameter.SqlDbType = System.Data.SqlDbType.DateTime;
            parameter.Direction = System.Data.ParameterDirection.Input;
            parameter.Value = Szuletett;

            command.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@pSzerzoNev";
            parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter2.Direction = System.Data.ParameterDirection.Input;
            parameter2.Value = SzerzoNev;
            command.Parameters.Add(parameter2);

            base.DeleteRowFromAuthors(command, ref errMess);


        }

        public string SzerzoTablaban(int SzerzoId,ref string errMess) 
        {
            string query = "SELECT Szerzok.SzerzoNev "+
                            "FROM Szerzok " +
                            "WHERE Szerzok.SZID = @pSzerzoID";


            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@pSzerzoID";
            parameter.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter.Direction = System.Data.ParameterDirection.Input;
            parameter.Value = SzerzoId;

            command.Parameters.Add(parameter);

            Console.WriteLine(command.ToString);

            string currentSzerzoNev = base.GetSzerzoNev(command, ref errMess);

            return currentSzerzoNev;
        }

        public void UpdateWriter(string ujSzerzoNev,string SzerzoNev, DateTime Szuletett, ref string errMess) 
        {
            string query = "UPDATE Szerzok " +
                           "SET SzerzoNev = @pUjSzerzoNev " +
                           "WHERE Szerzok.Szuletett = CAST(@pSzuletett AS DATE) AND Szerzok.SzerzoNev = @pSzerzoNev";

            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;

            SqlParameter ujNev = new SqlParameter();
            ujNev.ParameterName = "@pUjSzerzoNev";
            ujNev.SqlDbType = System.Data.SqlDbType.VarChar;
            ujNev.Direction = System.Data.ParameterDirection.Input;
            ujNev.Value = ujSzerzoNev;

            command.Parameters.Add(ujNev);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@pSzuletett";
            parameter.SqlDbType = System.Data.SqlDbType.DateTime;
            parameter.Direction = System.Data.ParameterDirection.Input;
            parameter.Value = Szuletett;

            command.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@pSzerzoNev";
            parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter2.Direction = System.Data.ParameterDirection.Input;
            parameter2.Value = SzerzoNev;
            command.Parameters.Add(parameter2);

            base.UpdateRowFromAuthors(command, ref errMess);
        }
    }
}
