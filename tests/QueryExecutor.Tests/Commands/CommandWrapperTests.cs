using Moq;
using Xunit;
using System.Data.Common;
using Newtonsoft.Json.Linq;
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
        var dataReader = SetupMockDbDataReader(expected.ToString());

        var commandWrapper = new Mock<CommandWrapper>(ConnectionString.String);
        commandWrapper
            .Setup(x => x.JsonResponse())
            .CallBase();
        commandWrapper
            .Setup(x => x.ExecuteReader())
            .Returns(dataReader);

        var result = commandWrapper.Object.JsonResponse();

        Assert.Equal(expected, result);
    }

    [Fact]
    internal void JsonResponse_ExpectedEmptyResponseError()
    {
        var dataReader = SetupMockDbDataReader("");

        var commandWrapper = new Mock<CommandWrapper>(ConnectionString.String);
        commandWrapper
            .Setup(x => x.JsonResponse())
            .CallBase();
        commandWrapper
            .Setup(x => x.ExecuteReader())
            .Returns(dataReader);

        var result = commandWrapper.Object.JsonResponse();

        var expected = new JObject
        {
            { "error", "Received empty string from DB." }
        }.ToString();
        Assert.Equal(expected, result);
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