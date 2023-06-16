namespace QueryExecutor.Tests;

public class TestQueryExecutor : Executor
{
    public TestQueryExecutor(string cmdText) : base(cmdText, "Server=localhost;Database=mydatabase;User Id=myusername;Password=mypassword;") { }

    public override bool EnableAWSXRay { get; set; } = false;
    public string ConnectionString { get; set; } = "Server=localhost;Database=mydatabase;User Id=myusername;Password=mypassword;";
}
