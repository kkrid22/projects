using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace DatabaseConnectionAndModification
{
    public class User 
    { 
        string nev;
        string jelszo;
        int csoportNev;

        public string Nev 
        { 
            get {return nev;}
            set { nev = value;}
        }
        public string Jelszo 
        {
            get { return jelszo;}
            set { jelszo = value;}
        }
        public int CsoportNev 
        {
            get { return csoportNev;}
            set { csoportNev = value;}
        } 
        User(string nev, string jelszo, int csoportNev) 
        {
            this.nev = nev;
            this.csoportNev=csoportNev;
            this.jelszo = jelszo;
        }
    }
   
    public class UserDAL: DALGen
    {
        public UserDAL(ref string error)
        {
            base.CreateConnection(ref error);
        }

        public void insertUser(string nev, string jelszo, int csoportId, ref string errMess) 
        {
            string query = "INSERT INTO Felhasznalok(Nev, Jelszo, Salt, CsoportID) " +
                        "VALUES (@pNev, @pJelszo, @pSalt, @pCsoportID)";

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(jelszo, salt);
            

            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@pNev";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Direction = System.Data.ParameterDirection.Input;
            parameter.Value = nev;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@pJelszo";
            parameter2.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter2.Direction = System.Data.ParameterDirection.Input;
            parameter2.Value = encryptedPassword;
            command.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@pSalt";
            parameter3.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter3.Direction = System.Data.ParameterDirection.Input;
            parameter3.Value = salt;
            command.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@pCsoportID";
            parameter4.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter4.Direction = System.Data.ParameterDirection.Input;
            parameter4.Value = csoportId;
            command.Parameters.Add(parameter4);

            base.InserRowsIntoUsers(command, ref errMess);
        }

        public int findRegistered(string felhasznalo, ref string errMess) 
        {
            string query = "SELECT Felhasznalok.Nev FROM Felhasznalok " +
                            "WHERE Felhasznalok.Nev = @pNev ";

            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@pNev";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Direction = System.Data.ParameterDirection.Input;
            parameter.Value = felhasznalo;
            command.Parameters.Add(parameter);

            return base.SelectRegisteredUsers(command, ref errMess);
        }

        public void procedureExec(string nev, string telefon, string felhasz, string jelszo, ref string errMess) 
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(jelszo, salt);

            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.CommandText = "regisztracio";
            command.Parameters.AddWithValue("@pNev", nev);
            command.Parameters.AddWithValue("@pTelefon", telefon);
            command.Parameters.AddWithValue("@pFelhasznalo", felhasz);
            command.Parameters.AddWithValue("@pJelszo", encryptedPassword);
            command.Parameters.AddWithValue("@pSalt", salt);

            base.ExecuteProcedure(command,ref errMess);
        }

        public int loginExec(string username, string password, ref string errMess) 
        {
            int result = -1;

            string query = "SELECT Felhasznalok.Jelszo FROM Felhasznalok " +
                          "WHERE Felhasznalok.Nev = @pNev ";

            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@pNev";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Direction = System.Data.ParameterDirection.Input;
            parameter.Value = username;
            command.Parameters.Add(parameter);

            string kapottJelszo = base.ExecuteCheckLogin(command,ref errMess);
            if (kapottJelszo != null)
            {
                string query2 = "SELECT Felhasznalok.Salt FROM Felhasznalok " +
                              "WHERE Felhasznalok.Nev = @pNev AND Felhasznalok.Jelszo = @pJelszo";

                SqlCommand command2 = new SqlCommand();
                command2.CommandText = query2;
                command2.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@pNev";
                parameter2.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter2.Direction = System.Data.ParameterDirection.Input;
                parameter2.Value = username;
                command2.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@pJelszo";
                parameter3.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter3.Direction = System.Data.ParameterDirection.Input;
                parameter3.Value = kapottJelszo;
                command2.Parameters.Add(parameter3);

                string salt = base.ExecuteCheckLogin(command2, ref errMess);

                string hasedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

                ///-------------------------------------------------------------------------------------------------------------------

                if (kapottJelszo == hasedPassword)
                {
                    string query3 = "SELECT Felhasznalok.CsoportID FROM Felhasznalok " +
                             "WHERE Felhasznalok.Nev = @pNev AND Felhasznalok.Jelszo = @pJelszo";


                    SqlCommand command3 = new SqlCommand();
                    command3.CommandText = query3;
                    command3.CommandType = System.Data.CommandType.Text;

                    SqlParameter parameter4 = new SqlParameter();
                    parameter4.ParameterName = "@pNev";
                    parameter4.SqlDbType = System.Data.SqlDbType.NVarChar;
                    parameter4.Direction = System.Data.ParameterDirection.Input;
                    parameter4.Value = username;
                    command3.Parameters.Add(parameter4);

                    SqlParameter parameter5 = new SqlParameter();
                    parameter5.ParameterName = "@pJelszo";
                    parameter5.SqlDbType = System.Data.SqlDbType.NVarChar;
                    parameter5.Direction = System.Data.ParameterDirection.Input;
                    parameter5.Value = kapottJelszo;
                    command3.Parameters.Add(parameter5);

                    result = Convert.ToInt32(base.ExecuteCheckLogin(command3, ref errMess));
                }
            }
            
            return result;
        }
    }
}
