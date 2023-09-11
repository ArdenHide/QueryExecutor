using System.Text;
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

        return jsonResponse.Length == 0 ?
            CreateEmptyResponseError() :
            jsonResponse.ToString();
    }

    public virtual void OpenConnection() => connection.Open();

    public abstract SqlDataReader ExecuteReader();

    private static string CreateEmptyResponseError()
    {
        return new JObject
        {
            { "error", "Received empty string from DB." }
        }.ToString();
    }
}
