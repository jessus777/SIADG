using System.Text.Json;
using System.Text.Json.Serialization;

namespace SIADG.Toolkit.Text.Json;

public class JsonDictionaryConverter : JsonConverter<IDictionary<string, object?>>
{
    private object? ReadValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.StartObject:
                return JsonSerializer.Deserialize<IDictionary<string, object?>>(ref reader, options);
            case JsonTokenType.StartArray:
                var list = new List<IDictionary<string, object?>>();
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    if (reader.TokenType != JsonTokenType.StartObject)
                        continue;
                    
                    var dictionary = JsonSerializer.Deserialize<IDictionary<string, object?>>(ref reader, options);
                    if (dictionary is null)
                        throw new JsonException();
                            
                    list.Add(dictionary);
                }
                return list;
            case JsonTokenType.String:
                return reader.GetString();
            case JsonTokenType.Number:
                if (reader.TryGetInt32(out int intValue))
                {
                    return intValue;
                }
                return reader.GetDouble();
            case JsonTokenType.True:
                return true;
            case JsonTokenType.False:
                return false;
            case JsonTokenType.Null:
                return null;
            default:
                throw new JsonException();
        }
    }
    
    public override IDictionary<string, object?>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var dictionary = new Dictionary<string, object?>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return dictionary;
            
            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException();

            var propertyName = reader.GetString();
            if (string.IsNullOrEmpty(propertyName))
                throw new JsonException();
            
            reader.Read();
            dictionary[propertyName] = ReadValue(ref reader, options);
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, IDictionary<string, object?> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var kvp in value)
        {
            writer.WritePropertyName(kvp.Key);

            switch (kvp.Value)
            {
                case IDictionary<string, object?> nestedDict:
                    JsonSerializer.Serialize(writer, nestedDict, options);
                    break;
                case IEnumerable<IDictionary<string, object?>> list:
                    writer.WriteStartArray();
                    foreach (var item in list)
                    {
                        JsonSerializer.Serialize(writer, item, options);
                    }
                    writer.WriteEndArray();
                    break;
                case string str:
                    writer.WriteStringValue(str);
                    break;
                case int intValue:
                    writer.WriteNumberValue(intValue);
                    break;
                case double doubleValue:
                    writer.WriteNumberValue(doubleValue);
                    break;
                case bool boolValue:
                    writer.WriteBooleanValue(boolValue);
                    break;
                case null:
                    writer.WriteNullValue();
                    break;
                default:
                    JsonSerializer.Serialize(writer, kvp.Value, kvp.Value?.GetType() ?? typeof(object), options);
                    break;
            }
        }

        writer.WriteEndObject();
    }
}