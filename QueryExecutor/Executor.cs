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

    private ICommandWrapper GetCommand(string cmdText)
    {
        if (EnableAWSXRay)
        {
            return new TraceableSqlCommandWrapper(cmdText, GetConnectionString(), false);
        }
        else
        {
            return new SqlCommandWrapper(cmdText, GetConnectionString());
        }
    }

    public abstract string GetConnectionString();
}