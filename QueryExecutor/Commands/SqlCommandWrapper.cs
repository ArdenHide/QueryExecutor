using System.Data.Common;
using System.Data.SqlClient;

namespace QueryExecutor.Commands;

public class SqlCommandWrapper : CommandWrapper, ICommandWrapper
{
    private readonly SqlCommand command;

    public SqlCommandWrapper(string cmdText, string connectionString)
        : base(connectionString)
    {
        command = new SqlCommand(cmdText, connection);
    }

    public override DbDataReader ExecuteReader() => command.ExecuteReader();
}
