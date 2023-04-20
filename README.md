# QueryExecutor

## Overview
The QueryExecutor library is a lightweight and easy-to-use library for executing SQL commands and retrieving JSON results. It also supports AWS X-Ray tracing for monitoring and analyzing your database calls.

## Installation
To install the QueryExecutor library, add the NuGet package to your project.

## Usage
To use the QueryExecutor library, follow these steps:

1. Create a custom class that extends the `Executor` class:

```C#
namespace YourNamespace
{
    public class YourQueryExecutor : Executor
    {
        public YourQueryExecutor(string cmdText) : base(cmdText) { }

        public override bool EnableAWSXRay { get; set; } = false;

        public override string GetConnectionString() =>
            "Server=localhost;Database=mydatabase;User Id=myusername;Password=mypassword;";
    }
}

```
Replace YourNamespace, YourQueryExecutor, and the connection string with your desired values.


2. Create an instance of your custom `Executor` class and execute the command:

```C#
var query = "SELECT * FROM TableName FOR JSON AUTO"; // request that returns json
var executor = new YourQueryExecutor(query);
var jsonResponse = executor.Execute();
```

### Configuration

You can customize the behavior of the QueryExecutor library by changing the properties of your custom `Executor` class:

- **EnableAWSXRay**: Set this property to `true` to enable AWS X-Ray tracing. By default, it is set to `false`.
```C#
var queryExecutor = new YourQueryExecutor(cmdText)
{
    EnableAWSXRay = true
};
```

- **GetConnectionString()**: Override this method to provide the connection string for your database. This method must return a valid connection string.

```C#
public override string GetConnectionString() =>
    "Server=localhost;Database=mydatabase;User Id=myusername;Password=mypassword;";
```
