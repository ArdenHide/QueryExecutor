using System.Data.Common;
using Amazon.XRay.Recorder.Handlers.SqlServer;

namespace QueryExecutor.Commands;

public class TraceableSqlCommandWrapper : CommandWrapper
{
    private readonly bool collectSqlQueries;

    public TraceableSqlCommandWrapper(string connectionString, bool collectSqlQueries)
        : base(connectionString)
    {
        this.collectSqlQueries = collectSqlQueries;
    }

    public override DbDataReader ExecuteReader(string cmdText) =>
        new TraceableSqlCommand(cmdText, connection, collectSqlQueries).ExecuteReader();
}
