using System.Text;
using System.Data.Common;
using QueryExecutor.Utils;
using QueryExecutor.Models;
using System.Data.SqlClient;

namespace QueryExecutor.Commands;

public abstract class CommandWrapper
{
    protected readonly SqlConnection connection;

    protected CommandWrapper(string connectionString)
    {
        connection = new SqlConnection(connectionString);
    }

    public virtual Response Response() => ResponseBuilder.BuildResponse(Read());

    protected string Read()
    {
        using (connection)
        {
            OpenConnection();

            using var reader = ExecuteReader();

            var jsonResponse = new StringBuilder();
            while (reader.Read())
            {
                jsonResponse.Append(reader.GetValue(0));
            }

            return jsonResponse.ToString();
        }
    }

    protected virtual void OpenConnection() => connection.Open();
    public abstract DbDataReader ExecuteReader();
}
