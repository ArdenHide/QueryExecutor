using System.Data.Common;
using Amazon.XRay.Recorder.Handlers.SqlServer;

namespace QueryExecutor.Commands;

public class TraceableSqlCommandWrapper : CommandWrapper
{
    private readonly TraceableSqlCommand command;

    public TraceableSqlCommandWrapper(string cmdText, string connectionString, bool collectSqlQueries)
        : base(connectionString)
    {
        command = new TraceableSqlCommand(cmdText, connection, collectSqlQueries);
    }

    public override DbDataReader ExecuteReader() => command.ExecuteReader();
}
