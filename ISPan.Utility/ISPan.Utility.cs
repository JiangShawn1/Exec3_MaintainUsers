using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISPan.Utility
{
    
    public class SqlDbHelper
    {
        public string connstring;
        public SqlDbHelper(string keyofConnString)
        {
            connstring = System.Configuration.ConfigurationManager
                               .ConnectionStrings[keyofConnString].ConnectionString;
        }

        public void ExecuteNonQuery(string sql, SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connstring))
            {
                SqlCommand command = new SqlCommand(sql, conn);

                conn.Open();


                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }

        public DataTable Select(string sql, SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connstring))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "table");

                return dataSet.Tables["table"];

            }

        }




    }
}
