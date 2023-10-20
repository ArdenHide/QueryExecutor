using Moq;
using Xunit;
using System.Data.Common;
using Newtonsoft.Json.Linq;
using QueryExecutor.Models;
using QueryExecutor.Commands;
using QueryExecutor.Tests.Mock;

namespace QueryExecutor.Tests.Commands;

public class CommandWrapperTests
{
    [Fact]
    internal void JsonResponse_ExpectedString()
    {
        var expected = new JObject
        {
            { "message", "hello world" }
        };
        var response = new Response(Status.Success, expected);
        var dataReader = SetupMockDbDataReader(expected.ToString());

        var commandWrapper = new Mock<CommandWrapper>(ConnectionString.String);
        commandWrapper
            .Setup(x => x.Response())
            .CallBase();
        commandWrapper
            .Setup(x => x.ExecuteReader())
            .Returns(dataReader);

        var result = commandWrapper.Object.Response();

        Assert.Equal(response.Status, result.Status);
        Assert.Equal(response.Result, result.Result);
    }

    [Fact]
    internal void JsonResponse_ExpectedEmptyResponseError()
    {
        var dataReader = SetupMockDbDataReader("");

        var commandWrapper = new Mock<CommandWrapper>(ConnectionString.String);
        commandWrapper
            .Setup(x => x.Response())
            .CallBase();
        commandWrapper
            .Setup(x => x.ExecuteReader())
            .Returns(dataReader);

        var result = commandWrapper.Object.Response();

        var expected = new JObject
        {
            { "error", "Received empty string from DB." }
        }.ToString();
        var response = new Response(Status.Error, expected);
        Assert.Equal(response.Status, result.Status);
        Assert.Equal(response.Result, result.Result);
    }

    private static DbDataReader SetupMockDbDataReader(string getValueReturns)
    {
        var dataReader = new Mock<DbDataReader>();
        dataReader.SetupSequence(reader => reader.Read())
            .Returns(true)
            .Returns(false);
        dataReader
            .Setup(x => x.GetValue(0))
            .Returns(getValueReturns);

        return dataReader.Object;
    }
}