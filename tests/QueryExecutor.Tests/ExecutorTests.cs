using Moq;
using Xunit;
using Newtonsoft.Json.Linq;
using QueryExecutor.Commands;
using QueryExecutor.Models;
using QueryExecutor.Tests.Mock;

namespace QueryExecutor.Tests;

public class ExecutorTests
{
    private const string CmdText = "SELECT * FROM TableName FOR JSON AUTO";
    private readonly JObject expected = new()
    {
        { "message", "Hello World!" }
    };

    [Fact]
    internal void Ctor()
    {
        var executor = new Executor(CmdText, ConnectionString.String);

        Assert.NotNull(executor);
    }

    [Fact]
    internal void Execute_WithoutAWSXRay_SqlCommandWrapper()
    {
        var response = new Response(Status.Success, expected);
        var mockCommand = new Mock<SqlCommandWrapper>(CmdText, ConnectionString.String);
        mockCommand
            .Setup(x => x.Response())
            .Returns(response);

        var result = new Executor(mockCommand.Object).Execute();

        Assert.Equal(response, result);
    }

    [Fact]
    internal void Execute_WithAWSXRay_TraceableSqlCommandWrapper()
    {
        var response = new Response(Status.Success, expected);
        var mockCommand = new Mock<TraceableSqlCommandWrapper>(CmdText, ConnectionString.String, true);
        mockCommand
            .Setup(x => x.Response())
            .Returns(response);

        var result = new Executor(mockCommand.Object).Execute();

        Assert.Equal(response, result);
    }
}