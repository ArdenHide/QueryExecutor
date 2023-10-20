using System.Data.Common;
using System.Data.SqlClient;

namespace QueryExecutor.Commands;

public class SqlCommandWrapper : CommandWrapper
{
    public SqlCommandWrapper(string connectionString)
        : base(connectionString)
    { }

    public override DbDataReader ExecuteReader(string cmdText) =>
        new SqlCommand(cmdText, connection).ExecuteReader();
}
