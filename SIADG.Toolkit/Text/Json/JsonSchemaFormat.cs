namespace SIADG.Toolkit.Text.Json;

public static class JsonSchemaFormat
{
    public const string DateTime = "date-time";
    public const string Date = "date";
    public const string Decimal = "decimal";
    public const string Time = "time";
    public const string Email = "email";
    public const string IdnEmail = "idn-email";
    public const string Hostname = "hostname";
    public const string IdnHostname = "idn-hostname";
    public const string Ipv4 = "ipv4";
    public const string Ipv6 = "ipv6";
    public const string Uri = "uri";
    public const string UriReference = "uri-reference";
    public const string Uuid = "uuid";
    public const string Iri = "iri";
    public const string IriReference = "iri-reference";
    public const string UriTemplate = "uri-template";
    public const string JsonPointer = "json-pointer";
    public const string RelativeJsonPointer = "relative-json-pointer";
    public const string Regex = "regex";
    
    public static readonly IReadOnlyDictionary<string, string> Expressions = new Dictionary<string, string>
    {
        { DateTime, @"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d+)?(?:Z|[+-]\d{2}:\d{2})$" },
        { Date, @"^\d{4}-\d{2}-\d{2}$" },
        { Decimal, @"^-?\d+(\.\d+)?$" },
        { Time, @"^\d{2}:\d{2}:\d{2}(?:\.\d+)?$" },
        { Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" },
        
        // Simplified, consider using a library for IDN email validation
        { IdnEmail, @"^[^\s@]+@[^\s@]+\.[^\s@]+$" },
        { Hostname, @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$" },
        
        // Simplified, consider using a library for IDN hostname validation
        { IdnHostname, @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$" },
        { Ipv4, @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$" },
        { Ipv6, @"^([0-9a-fA-F]{1,4}:){7}([0-9a-fA-F]{1,4}|:)|(([0-9a-fA-F]{1,4}:){6}(:[0-9a-fA-F]{1,4}|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))|::([0-9a-fA-F]{1,4}:){5})" },
        
        // Simplified
        { Uri, @"^[\w+:\/\/]?[\w\d\.-]+\.[\w\d\.\/:%+&=?#-]+$" },
        
        // Simplified and similar to Uri for demonstration
        { UriReference, @"^[\w+:\/\/]?[\w\d\.-]+\.[\w\d\.\/:%+&=?#-]+$" },
        { Uuid, @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[1-5][0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}$" },
        
        // Very permissive, IRI can include virtually all Unicode characters
        { Iri, @"^[\u0000-\uFFFF]+$" },
        
        // Similar to Iri, for demonstration
        { IriReference, @"^[\u0000-\uFFFF]+$" },
        
        // Simplified, real URI templates can be more complex
        { UriTemplate, @"^\{?[^\}]+\}?$" },
        { JsonPointer, @"^(\/[a-zA-Z0-9-_]+)*$" },
        { RelativeJsonPointer, @"^(0|[1-9][0-9]*)(#[a-zA-Z0-9-_]+)?$" },
    };
}