using System.Text;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace QueryExecutor.Commands;

public class SqlCommandWrapper : ICommandWrapper
{
    private readonly SqlConnection connection;
    private readonly SqlCommand command;

    public SqlCommandWrapper(string cmdText, string connectionString)
    {
        connection = new SqlConnection(connectionString);

        command = new SqlCommand(cmdText, connection);
    }

    public JToken GetJsonResponse()
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

    public SqlDataReader ExecuteReader() => command.ExecuteReader();

    public void OpenConnection() => connection.Open();
}
