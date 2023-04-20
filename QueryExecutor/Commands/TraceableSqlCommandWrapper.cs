using System.Text;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using Amazon.XRay.Recorder.Handlers.SqlServer;

namespace QueryExecutor.Commands;

public class TraceableSqlCommandWrapper : ICommandWrapper
{
    private readonly SqlConnection connection;
    private readonly TraceableSqlCommand command;

    public TraceableSqlCommandWrapper(string cmdText, string connectionString, bool collectSqlQueries)
    {
        connection = new SqlConnection(connectionString);

        command = new TraceableSqlCommand(cmdText, connection, collectSqlQueries);
    }

    public virtual JToken GetJsonResponse()
    {
        using (connection)
        {
            OpenConnection();

            using var reader = ExecuteReader();

            var jsonResponse = new StringBuilder();
            while (reader.Read())
            {
                jsonResponse.Append(reader.GetValue(0).ToString());
            }

            // TDO: Add catch exception on try parse string to json.
            // Throw custom exception with information what user try to parse.
            return JToken.Parse(jsonResponse.ToString());
        }
    }

    public virtual SqlDataReader ExecuteReader() => command.ExecuteReader();

    public virtual void OpenConnection() => connection.Open();
}
