using QueryExecutor.Models;
using QueryExecutor.Commands;

namespace QueryExecutor;

public class Executor
{
    private readonly CommandWrapper command;

    public Executor(string cmdText, string connectionString, bool enableAwsXRay = false)
    {
        command = GetCommand(cmdText, connectionString, enableAwsXRay);
    }

    public Executor(CommandWrapper command)
    {
        this.command = command;
    }

    public virtual Response Execute() => command.Response();

    private static CommandWrapper GetCommand(string cmdText, string connectionString, bool enableAwsXRay) => enableAwsXRay
        ? new TraceableSqlCommandWrapper(cmdText, connectionString, true)
        : new SqlCommandWrapper(cmdText, connectionString);
}
