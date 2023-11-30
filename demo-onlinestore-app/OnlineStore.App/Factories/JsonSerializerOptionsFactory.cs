using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineStore.App.Factories;

public static class JsonSerializerOptionsFactory
{
    public static JsonSerializerOptions ConfigureJsonSerializerOptions()
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        return jsonSerializerOptions;
    }
}
