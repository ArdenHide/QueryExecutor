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

    public virtual Response Response() => ResponseBuilder.Response(Read());

    protected StringBuilder Read()
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

            return jsonResponse;
        }
    }

    protected virtual void OpenConnection() => connection.Open();
    public abstract DbDataReader ExecuteReader();
}
