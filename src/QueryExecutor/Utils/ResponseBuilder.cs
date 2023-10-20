using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QueryExecutor.Models;

namespace QueryExecutor.Utils;

public static class ResponseBuilder
{
    private static readonly JObject EmptyResponseError = new JObject
    {
        { "error", "Received empty string from DB." }
    };

    private static readonly JObject InvalidJsonError = new JObject
    {
        { "error", "Received data from DB cannot convert to JSON." }
    };

    public static Response BuildResponse(string jsonResponse)
    {
        if (string.IsNullOrWhiteSpace(jsonResponse))
        {
            return new Response(Status.Error, EmptyResponseError);
        }

        try
        {
            var response = JToken.Parse(jsonResponse);
            return new Response(Status.Success, response);
        }
        catch (JsonException)
        {
            return new Response(Status.Error, InvalidJsonError);
        }
    }
}