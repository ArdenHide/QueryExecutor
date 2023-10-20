using Moq;
using Xunit;
using Newtonsoft.Json.Linq;
using QueryExecutor.Commands;
using QueryExecutor.Models;
using QueryExecutor.Tests.Mock;

namespace QueryExecutor.Tests;

public class ExecutorTests
{
    private readonly JObject expected = new()
    {
        { "message", "Hello World!" }
    };

    [Fact]
    internal void Ctor()
    {
        var executor = new Executor(MockData.ConnectionString);

        Assert.NotNull(executor);
    }

    [Fact]
    internal void Execute_WithoutAWSXRay_SqlCommandWrapper()
    {
        var response = new Response(Status.Success, expected);
        var mockCommand = new Mock<SqlCommandWrapper>(MockData.ConnectionString);
        mockCommand
            .Setup(x => x.Response(MockData.CmdText))
            .Returns(response);

        var result = new Executor(mockCommand.Object).Execute(MockData.CmdText);

        Assert.Equal(response, result);
    }

    [Fact]
    internal void Execute_WithAWSXRay_TraceableSqlCommandWrapper()
    {
        var response = new Response(Status.Success, expected);
        var mockCommand = new Mock<TraceableSqlCommandWrapper>(MockData.ConnectionString, true);
        mockCommand
            .Setup(x => x.Response(MockData.CmdText))
            .Returns(response);

        var result = new Executor(mockCommand.Object).Execute(MockData.CmdText);

        Assert.Equal(response, result);
    }
}