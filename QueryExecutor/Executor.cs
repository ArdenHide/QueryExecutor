using Newtonsoft.Json.Linq;
using QueryExecutor.Commands;

namespace QueryExecutor;

public abstract class Executor
{
    private readonly string connectionString;
    public ICommandWrapper Command { get; set; }
    public abstract bool EnableAWSXRay { get; set; }

    public Executor(string cmdText, string connectionString)
    {
        this.connectionString = connectionString;
        Command = GetCommand(cmdText);
    }

    public virtual JToken Execute() => Command.GetJsonResponse();

    private ICommandWrapper GetCommand(string cmdText) => EnableAWSXRay
        ? new TraceableSqlCommandWrapper(cmdText, connectionString, true)
        : new SqlCommandWrapper(cmdText, connectionString);
}
