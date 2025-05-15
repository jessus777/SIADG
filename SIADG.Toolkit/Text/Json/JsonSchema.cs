// ReSharper disable CollectionNeverUpdated.Global

using System.Text.Json.Serialization;

namespace SIADG.Toolkit.Text.Json;

public sealed class JsonSchema
{
    [JsonPropertyName("$schema")]
    public string? Schema { get; set; }
    
    [JsonPropertyName("$id")]
    public string? Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Type { get; set; }
    public Dictionary<string, JsonSchema>? Properties { get; set; }
    public List<string>? Required { get; set; }
    public object? Items { get; set; } // Can be JsonSchema or List<JsonSchema>
    public int? MinItems { get; set; }
    public int? MaxItems { get; set; }
    public bool? UniqueItems { get; set; }
    public string? Pattern { get; set; }
    public double? Minimum { get; set; }
    public double? Maximum { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public List<object>? Enum { get; set; }
    public string? Format { get; set; }
    public object? AdditionalProperties { get; set; } // Can be boolean or JsonSchema
    public Dictionary<string, JsonSchema>? PatternProperties { get; set; }
    public Dictionary<string, object>? Dependencies { get; set; } // Can be List<string> or JsonSchema
    public List<JsonSchema>? AllOf { get; set; }
    public List<JsonSchema>? AnyOf { get; set; }
    public List<JsonSchema>? OneOf { get; set; }
    public JsonSchema? Not { get; set; }
    public double? ExclusiveMinimum { get; set; }
    public double? ExclusiveMaximum { get; set; }
    public double? MultipleOf { get; set; }
    public int? MinProperties { get; set; }
    public int? MaxProperties { get; set; }
    public List<object>? Examples { get; set; }
    public object? Default { get; set; }
    public Dictionary<string, JsonSchema>? Definitions { get; set; }
    
    public Dictionary<string, object?>? PmxMetadata { get; set; }
}