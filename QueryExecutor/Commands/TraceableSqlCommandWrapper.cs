using System.Data.SqlClient;
using Amazon.XRay.Recorder.Handlers.SqlServer;

namespace QueryExecutor.Commands;

public class TraceableSqlCommandWrapper : CommandWrapper, ICommandWrapper
{
    private readonly TraceableSqlCommand command;

    public TraceableSqlCommandWrapper(string cmdText, string connectionString, bool collectSqlQueries)
        : base(connectionString)
    {
        command = new TraceableSqlCommand(cmdText, connection, collectSqlQueries);
    }

    public override SqlDataReader ExecuteReader() => command.ExecuteReader();
}
