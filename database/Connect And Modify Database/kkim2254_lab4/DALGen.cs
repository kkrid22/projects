using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace DatabaseConnectionAndModification
{

    /// <summary>
    /// Summary description for DAL.
    /// </summary>
    public abstract class DALGen
    {
        protected static bool isConnected;

        protected static SqlConnection sqlConnection;


        protected string strSqlConn = "Data Source=DESKTOP-BMVQHCU;Initial Catalog=lab3_tabla;Integrated Security=SSPI"; 
        protected void CreateConnection(ref string errMess)
        {
            if (isConnected != true)
            {
                try { 
                    sqlConnection = new SqlConnection(strSqlConn);

                    sqlConnection.Open();
                    errMess = "OK";
                    
                }
                catch (SqlException ex)
                {
                    errMess = ex.Message;
                }
                finally
                {
                    if (sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }



        /// <summary>
        /// Open the Connection when the state is not already open.
        /// </summary>
        private void OpenConnection(ref string errMess)
        {
            if (isConnected == false)
            {
                try
                {
                    sqlConnection.Open();
                    isConnected = true;
                    errMess = "OK";
                }
                catch (SqlException ex)
                {
                    errMess = ex.Message;
                }
            }
        }

        private void CloseConnection()
        {
            // Close the Connection when the connection is opened.
            if (isConnected == true)
            {
                sqlConnection.Close();
                isConnected = false;
            }
        }

        protected SqlDataReader ExecuteReader(string sQuery, ref string errMess)
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                OpenConnection(ref errMess);

                SqlCommand sqlCommand = new SqlCommand(sQuery, sqlConnection);

                sqlDataReader = sqlCommand.ExecuteReader();
                errMess = "OK";
            }
            catch (Exception e)
            {
                errMess = e.Message;
                CloseDataReader(sqlDataReader);
            }
            return sqlDataReader;
        }

        /// <summary>
        /// Executes a given query, and returns the result in a dataset.
        /// A megfelelo select utasitassal feltolti a DataSet-et a megfelelo adatokkal egy DataAdapter objektum segitsegevel
        /// </summary>
        /// <param name="sQuery"> The query to be executed </param>
        /// <param name="sTableName"> The name of the DataSet. </param>
        /// <param name="ErrMess"> Output error message </param>
        /// <returns></returns>
        protected DataSet ExecuteDS(string query, ref string errMess)
        {
            
            DataSet dataSet = new DataSet();
            try
            {
                OpenConnection(ref errMess);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);

                dataAdapter.Fill(dataSet);

                errMess = "OK";
            }
            catch (SqlException e)
            {
                errMess = e.Message;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    CloseConnection();
                }
            }
            return dataSet;
        }

        /// <summary>
        /// Closes the data reader given as a parameter, and also closes the connection
        /// </summary>
        /// <param name="rdr">The SqlDataReader to be closed</param>
        protected void CloseDataReader(SqlDataReader rdr)
        {
            if (rdr != null)
                rdr.Close();
            CloseConnection();
        }

        protected void ExecuteProcedure(SqlCommand sqlCommand, ref string errMess) 
        {
            try
            {
                // Open the connection
                OpenConnection(ref errMess);

                sqlCommand.Connection = sqlConnection;
               
                sqlCommand.ExecuteNonQuery();

                errMess = "OK";
            }
            catch (SqlException e)
            {
                errMess = e.Message;

            }
            finally
            {
                // Close the connection
                CloseConnection();
            }
        }

        protected int SelectRegisteredUsers(SqlCommand command, ref string errMess) 
        {
            int rowsFound = -1;
            try
            {
                // Open the connection
                OpenConnection(ref errMess);
                command.Connection = sqlConnection;
                // Execute the command
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Check if there are any rows to read
                    if (reader.HasRows)
                    {
                        rowsFound = 1;
                        // You can process the rows here if needed
                    }
                }
                errMess = "OK";
            }
            catch (SqlException e)
            {
                errMess = e.Message;

            }
            finally
            {
                // Close the connection
                CloseConnection();
            }
            return rowsFound;
        }

        protected void InserRowsIntoUsers(SqlCommand command, ref string errMess) 
        {
            try
            {
                // Open the connection
                OpenConnection(ref errMess);
                command.Connection = sqlConnection;
                // Execute the command
                int rowsAffected = command.ExecuteNonQuery();
                errMess = "OK";
            }
            catch (SqlException e)
            {
                errMess = e.Message;

            }
            finally
            {
                // Close the connection
                CloseConnection();
            }
        }

        protected void DeleteRowFromAuthors(SqlCommand command, ref string errMess) 
        {
            try
            {
                // Open the connection
                OpenConnection(ref errMess);
                command.Connection = sqlConnection;
                // Execute the command
                int rowsAffected = command.ExecuteNonQuery();
                errMess = "OK";
            }
            catch (SqlException e)
            {
                errMess = e.Message;
               
            }
            finally
            {
                // Close the connection
                CloseConnection();
            }
        }

        protected string GetSzerzoNev(SqlCommand command, ref string errMess)
        {
            string result = null;
            try
            {
           
                OpenConnection(ref errMess);
                command.Connection = sqlConnection;
                Console.WriteLine(command.ToString);
                object SzerzoNev = command.ExecuteScalar();
            

                if (SzerzoNev != null)
                {
                    // Convert the scalar value to a string
                    result = SzerzoNev.ToString();
                }

                errMess = "OK";
          
            }
            catch (SqlException e) 
            {
               errMess=e.Message;
            }
            finally { 
                CloseConnection();
            }

            return result;
        }

        protected void UpdateRowFromAuthors(SqlCommand command, ref string errMess)
        {
            try
            {
                // Open the connection
                OpenConnection(ref errMess);
                command.Connection = sqlConnection;
                // Execute the command
                int rowsAffected = command.ExecuteNonQuery();
                errMess = "OK";
            }
            catch (SqlException e)
            {
                errMess = e.Message;

            }
            finally
            {
                // Close the connection
                CloseConnection();
            }
        }

        protected string ExecuteCheckLogin(SqlCommand command, ref string errMess) 
        {
            string result = null;
            try
            {
                // Open the connection
                OpenConnection(ref errMess);
                command.Connection = sqlConnection;

                // Execute the command and obtain the result
                object scalarResult = command.ExecuteScalar();
                if (scalarResult != null)
                {
                    result = scalarResult.ToString();
                    // Do something with the result...
                    Console.WriteLine("Retrieved string: " + result);
                }
                else
                {
                    // Handle case where no result is returned
                    Console.WriteLine("No result returned.");
                }
            }
            catch (SqlException e)
            {
                errMess = e.Message;

            }
            finally
            {
                // Close the connection
                CloseConnection();
            }
            return result;
        }

        protected DataSet ExecuteParameterizedQuery(int SzerzoID, string konyvCim, ref string errMess) 
        {
            string query = "SELECT Szerzok.SZID, Szerzok.SzerzoNev, Szerzok.Szuletett, Konyvek.KID ,Konyvek.Cim " +
                           "FROM Konyvek, Szerzok " +
                           "WHERE Konyvek.SZID = Szerzok.SZID ";
                  
            if (SzerzoID > 0)
            {
                query += " AND Konyvek.SZID = @pSzerzoID ";

            }

            if (konyvCim != null) 
            {
                query += "AND Konyvek.Cim LIKE @pKonyvCim";
            }

            DataSet dataSet = new DataSet();
            try
            {
                OpenConnection(ref errMess);
                SqlDataAdapter adapter = new()
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey,
                    SelectCommand = new SqlCommand(query, sqlConnection)
                };

                if (SzerzoID > 0)
                {
                    adapter.SelectCommand.Parameters.Add("@pSzerzoID", SqlDbType.Int).Value = SzerzoID;
                }

                if (konyvCim != null)
                {
                    adapter.SelectCommand.Parameters.Add("@pKonyvCim", SqlDbType.NVarChar).Value = "%" + konyvCim + "%";
                }

                adapter.SelectCommand.CommandText = query;
                adapter.Fill(dataSet, "BookData");

                errMess = "OK";

            }
            catch (Exception e)
            {
                dataSet = null;
                errMess = e.Message;
            }
            finally 
            {
                CloseConnection();
            }
            return dataSet;
        }

    }
}
