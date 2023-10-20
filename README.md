![Project logo](https://raw.githubusercontent.com/ArdenHide/QueryExecutor/main/logo/1000x1000.png)

# QueryExecutor

## Overview
The QueryExecutor library is a lightweight and easy-to-use library for executing SQL commands and retrieving JSON results. It also supports AWS X-Ray tracing for monitoring and analyzing your database calls.

## Installation
To install the QueryExecutor library, add the NuGet package to your project.

## Usage
To use the QueryExecutor library, follow these steps:

Create an instance of `Executor` class and execute the command:

```C#
var query = "SELECT * FROM TableName FOR JSON AUTO"; // request that returns JSON
var connectionString = "Server=localhost;Database=mydatabase;User Id=myusername;Password=mypassword;";
var executor = new Executor(connectionString);
var response = executor.Execute(query);

Console.WriteLine(response.Status);
Console.WriteLine(response.Result);
```

By following these steps, you can easily execute SQL commands and retrieve JSON results using the QueryExecutor library.
