using System.Text.Json;
using System.Text.Json.Serialization;

namespace SIADG.Toolkit.Text.Json;

public static class JsonSerializationDefaults
{
    public static readonly JsonSerializerOptions Options = new();

    static JsonSerializationDefaults() => Configure();

    public static void Configure()
    {
        Configure(
            Options,
            JsonNamingPolicy.CamelCase,
            JsonNamingPolicy.CamelCase,
            JsonIgnoreCondition.WhenWritingNull,
            [
                new JsonStringEnumConverter(),
                new JsonDictionaryConverter()
            ]
        );
    }

    public static void Configure(
        JsonSerializerOptions options,
        JsonNamingPolicy? propertyNamingPolicy,
        JsonNamingPolicy? dictionaryKeyPolicy,
        JsonIgnoreCondition defaultIgnoreCondition,
        IEnumerable<JsonConverter> converters
    )
    {
        options.PropertyNamingPolicy = propertyNamingPolicy;
        options.DictionaryKeyPolicy = dictionaryKeyPolicy;
        options.DefaultIgnoreCondition = defaultIgnoreCondition;
        options.PropertyNameCaseInsensitive = true;

        foreach (var converter in converters)
            options.Converters.Add(converter);
    }

    public static void Configure(this JsonSerializerOptions target, JsonSerializerOptions? source = null)
    {
        var s = source ?? Options;
        Configure(
            target,
            s.PropertyNamingPolicy,
            s.DictionaryKeyPolicy,
            s.DefaultIgnoreCondition,
            s.Converters
        );
    }
}