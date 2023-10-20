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

    public virtual JToken JsonResponse()
    {
        using (connection)
        {
            OpenConnection();

            return JToken.Parse(Read());
        }
    }

    protected string Read()
    {
        using var reader = ExecuteReader();

        var jsonResponse = new StringBuilder();
        while (reader.Read())
        {
            jsonResponse.Append(reader.GetValue(0));
        }

        return jsonResponse.Length == 0 ?
            CreateEmptyResponseError() :
            jsonResponse.ToString();
    }

    protected virtual void OpenConnection() => connection.Open();
    public abstract DbDataReader ExecuteReader();

    private static string CreateEmptyResponseError()
    {
        return new JObject
        {
            { "error", "Received empty string from DB." }
        }.ToString();
    }
}
