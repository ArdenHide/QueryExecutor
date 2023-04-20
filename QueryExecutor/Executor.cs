using Newtonsoft.Json.Linq;
using QueryExecutor.Commands;

namespace QueryExecutor;

public abstract class Executor
{
    public ICommandWrapper Command { get; set; }
    public abstract bool EnableAWSXRay { get; set; }

    public Executor(string cmdText)
    {
        Command = GetCommand(cmdText);
    }

    public virtual JToken Execute() => Command.GetJsonResponse();

    private ICommandWrapper GetCommand(string cmdText) => EnableAWSXRay
        ? new TraceableSqlCommandWrapper(cmdText, GetConnectionString(), true)
        : new SqlCommandWrapper(cmdText, GetConnectionString());

    public abstract string GetConnectionString();
}
