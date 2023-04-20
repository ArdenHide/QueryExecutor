namespace QueryExecutor.Tests;

public class QueryExecutor : Executor
{
    public QueryExecutor(string cmdText) : base(cmdText) { }

    public override bool EnableAWSXRay { get; set; } = false;

    public override string GetConnectionString() => "Server=localhost;Database=mydatabase;User Id=myusername;Password=mypassword;";
}
