using Newtonsoft.Json.Linq;

namespace QueryExecutor.Commands;

public interface ICommandWrapper
{
    public JToken GetJsonResponse();
    public string ReadJson();
}
