using System.Text;
using Newtonsoft.Json.Linq;
using QueryExecutor.Models;

namespace QueryExecutor.Utils;

public static class ResponseBuilder
{
    public static Response Response(StringBuilder jsonResponse)
    {
        if (jsonResponse.Length == 0)
        {
            return new Response(Status.Error, EmptyResponseError);
        }

        try
        {
            var response = JToken.Parse(jsonResponse.ToString());
            return new Response(Status.Success, response);
        }
        catch (Exception)
        {
            return new Response(Status.Error, InvalidJsonError);
        }
    }

    private static JObject EmptyResponseError => new()
    {
        { "error", "Received empty string from DB." }
    };
    private static JObject InvalidJsonError => new()
    {
        { "error", "Received data from DB cannot convert to JSON." }
    };
}