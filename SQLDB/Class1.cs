using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SQLDB
{
    private string connectionString;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionString"></param>
    public SQLDB(string connectionString)
    {
        this.connectionString = connectionString;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spName"></param>
    public void ExecuteSP<T>(string spName)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
    }

   /// <summary>
   /// 
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="spName"></param>
   /// <param name="type"></param>
    public void ExecuteSP<T>(string spName, T type)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlCommandBuilder.DeriveParameters(sqlCommand);
                
                foreach (SqlParameter param in sqlCommand.Parameters)
                {
                    if(param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                    {
                        object val = type.GetType().GetProperty(param.ParameterName).GetValue(type, null);

                        if (val != null)
                        {
                            param.Value = val;
                        }                           
                    }      
                }
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}

