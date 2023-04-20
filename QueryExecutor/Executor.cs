using Newtonsoft.Json.Linq;
using QueryExecutor.Commands;

namespace QueryExecutor;

public abstract class Executor
{
    public abstract bool EnableAWSXRay { get; set; }

    public virtual JToken Execute(string cmdText)
    {
        var command = GetCommand(cmdText);

        return command.GetJsonResponse();
    }

    private ICommandWrapper GetCommand(string cmdText) => EnableAWSXRay
        ? new TraceableSqlCommandWrapper(cmdText, GetConnectionString(), true)
        : new SqlCommandWrapper(cmdText, GetConnectionString());

    public abstract string GetConnectionString();
}