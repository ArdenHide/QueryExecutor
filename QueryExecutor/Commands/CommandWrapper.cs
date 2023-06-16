using System.Text;
using System.Data.Common;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace QueryExecutor.Commands;

public abstract class CommandWrapper
{
    protected readonly SqlConnection connection;

    protected CommandWrapper(string connectionString)
    {
        connection = new SqlConnection(connectionString);
    }

    public virtual JToken GetJsonResponse()
    {
        using (connection)
        {
            OpenConnection();

            // TDO: Add catch exception on try parse string to json.
            // Throw custom exception with information what user try to parse.
            return JToken.Parse(ReadJson());
        }
    }

    public virtual string ReadJson()
    {
        using var reader = ExecuteReader();

        var jsonResponse = new StringBuilder();

        while (reader.Read())
        {
            jsonResponse.Append(reader.GetValue(0).ToString());
        }

        return jsonResponse.ToString();
    }

    public virtual void OpenConnection() => connection.Open();

    public abstract DbDataReader ExecuteReader();
}
