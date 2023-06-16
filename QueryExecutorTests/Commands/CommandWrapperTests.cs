using Moq;
using Xunit;
using System.Data.Common;
using Newtonsoft.Json.Linq;

namespace QueryExecutor.Commands.Tests;

public class CommandWrapperTests
{
    private const string connectionString = "Server=localhost;Database=mydatabase;User Id=myusername;Password=mypassword;";
    private readonly string expected = new JObject()
    {
        { "message", "HelloWorld!" }
    }.ToString();
    private readonly Mock<CommandWrapper> mockCommandWrapper;

    public CommandWrapperTests()
    {
        var readQueue = new Queue<bool>();
        readQueue.Enqueue(true);
        readQueue.Enqueue(false);

        var mockDbDataReader = new Mock<DbDataReader>();
        mockDbDataReader.Setup(x => x.GetValue(0)).Returns(expected);
        mockDbDataReader.Setup(x => x.Read()).Returns(() => readQueue.Dequeue());

        mockCommandWrapper = new Mock<CommandWrapper>(connectionString);
        mockCommandWrapper.Setup(x => x.ExecuteReader())
            .Returns(mockDbDataReader.Object);
    }

    [Fact]
    internal void ReadJson_ShouldExpectedJsonString()
    {
        mockCommandWrapper.Setup(x => x.ReadJson())
            .CallBase();

        var result = mockCommandWrapper.Object.ReadJson();

        Assert.Equal(expected, result);
    }
}
