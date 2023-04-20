using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace QueryExecutor.Commands;

public interface ICommandWrapper
{
    public JToken GetJsonResponse();
    public  SqlDataReader ExecuteReader();
    public void OpenConnection();
}
