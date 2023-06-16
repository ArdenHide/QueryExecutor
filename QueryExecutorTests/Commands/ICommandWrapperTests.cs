using Xunit;
using QueryExecutor.Tests;
using Newtonsoft.Json.Linq;

namespace QueryExecutor.Commands.Tests;

public class ICommandWrapperTests
{
    private readonly JToken expectedJsonResponse = new JObject
    {
        { "message", "Hello World!" }
    };

    [Fact]
    public void GetJsonResponse_ShouldWorkCorrectly()
    {
        var commandWrapper = new TestCommandWrapper();

        JToken result = commandWrapper.GetJsonResponse();

        Assert.NotNull(result);
        Assert.Equal(expectedJsonResponse, result);
    }

    [Fact]
    public void ReadJson_ShouldWorkCorrectly()
    {
        var commandWrapper = new TestCommandWrapper();

        string result = commandWrapper.ReadJson();

        Assert.NotNull(result);
        Assert.Equal(expectedJsonResponse.ToString(), result);
    }
}
