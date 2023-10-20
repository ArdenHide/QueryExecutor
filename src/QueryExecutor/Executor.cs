using QueryExecutor.Models;
using QueryExecutor.Commands;

namespace QueryExecutor;

public class Executor
{
    private readonly CommandWrapper command;

    public Executor(string connectionString, bool enableAwsXRay = false)
    {
        command = GetCommand(connectionString, enableAwsXRay);
    }

    public Executor(CommandWrapper command)
    {
        this.command = command;
    }

    public virtual Response Execute(string cmdText) => command.Response(cmdText);

    private static CommandWrapper GetCommand(string connectionString, bool enableAwsXRay) => enableAwsXRay
        ? new TraceableSqlCommandWrapper(connectionString, true)
        : new SqlCommandWrapper(connectionString);
}
