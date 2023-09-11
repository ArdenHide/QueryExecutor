using Moq;
using Xunit;
using Newtonsoft.Json.Linq;
using QueryExecutor.Commands;

namespace QueryExecutor.Tests;

public class ExecutorTests
{
    [Fact]
    public void Execute_WithoutAWSXRay_SqlCommandWrapper()
    {
        var cmdText = "SELECT * FROM TableName FOR JSON AUTO";
        var expected = new JObject
        {
            { "message", "Hello World!" }
        }.ToString();

        var queryExecutor = new QueryExecutor(cmdText)
        {
            EnableAWSXRay = false
        };

        var mockCommand = new Mock<SqlCommandWrapper>(cmdText, queryExecutor.ConnectionString);
        mockCommand.Setup(x => x.OpenConnection());
        mockCommand.Setup(x => x.ReadJson()).Returns(expected);
        mockCommand.Setup(x => x.GetJsonResponse()).CallBase();

        queryExecutor.Command = mockCommand.Object;

        var result = queryExecutor.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Execute_WithAWSXRay_TraceableSqlCommandWrapper()
    {
        var cmdText = "SELECT * FROM TableName FOR JSON AUTO";
        var expected = new JObject
        {
            { "message", "Hello World!" }
        }.ToString();

        var queryExecutor = new QueryExecutor(cmdText)
        {
            EnableAWSXRay = true
        };

        var mockCommand = new Mock<TraceableSqlCommandWrapper>(cmdText, queryExecutor.ConnectionString, true);
        mockCommand.Setup(x => x.OpenConnection());
        mockCommand.Setup(x => x.ReadJson()).Returns(expected);
        mockCommand.Setup(x => x.GetJsonResponse()).CallBase();

        queryExecutor.Command = mockCommand.Object;

        var result = queryExecutor.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CreateEmptyResponseError_WithEmptyResponse_ErrorMessage()
    {
        var cmdText = "SELECT * FROM TableName FOR JSON AUTO";
        var expected = new JObject
        {
            { "error", "From DB received empty string." }
        }.ToString();

        var queryExecutor = new QueryExecutor(cmdText)
        {
            EnableAWSXRay = true
        };

        var mockCommand = new Mock<TraceableSqlCommandWrapper>(cmdText, queryExecutor.ConnectionString, true);
        mockCommand.Setup(x => x.OpenConnection());
        mockCommand.Setup(x => x.ReadJson()).Returns(expected);
        mockCommand.Setup(x => x.GetJsonResponse()).CallBase();

        queryExecutor.Command = mockCommand.Object;

        var result = queryExecutor.Execute();

        Assert.Equal(expected, result);
    }
}