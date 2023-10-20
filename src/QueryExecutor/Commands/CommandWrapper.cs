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

    public virtual Response Response(string cmdText) => ResponseBuilder.BuildResponse(Read(cmdText));

    protected string Read(string cmdText)
    {
        OpenConnection();
        using var reader = ExecuteReader(cmdText);

        var jsonResponse = new StringBuilder();
        while (reader.Read())
        {
            jsonResponse.Append(reader.GetValue(0));
        }

        CloseConnection();
        return jsonResponse.ToString();
    }

    protected virtual void OpenConnection() => connection.Open();
    protected virtual void CloseConnection() => connection.Close();
    public abstract DbDataReader ExecuteReader(string cmdText);
}
