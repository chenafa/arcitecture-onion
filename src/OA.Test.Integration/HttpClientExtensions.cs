using System.Text.Json;
using System.Text;

namespace OA.Test.Integration;

public static class HttpClientExtensions
{
    static readonly JsonSerializerOptions DefaultJsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static StringContent SerializeAndCreateContent<T>(T model)
    {
        string jsonContent = JsonSerializer.Serialize(model, DefaultJsonOptions);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return content;
    }
}
