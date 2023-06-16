using Newtonsoft.Json.Linq;
using QueryExecutor.Commands;

namespace QueryExecutor.Tests;

public class TestCommandWrapper : ICommandWrapper
{
    public JToken GetJsonResponse() =>
        new JObject
        {
            { "message", "Hello World!" }
        };

    public string ReadJson() =>
        GetJsonResponse()
        .ToString();
}
