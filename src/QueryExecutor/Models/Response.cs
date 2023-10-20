using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace QueryExecutor.Models;

public class Response
{
    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    public Status Status { get; set; }

    [JsonProperty("result")]
    public JToken Result { get; set; }

    public Response(Status status, JToken result)
    {
        Status = status;
        Result = result;
    }
}