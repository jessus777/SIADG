namespace SIADG.Toolkit.Text.Json;

public static class JsonSchemaFormatExpression
{
    public const string DateTime = @"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d+)?(?:Z|[+-]\d{2}:\d{2})$";
    public const string Date = @"^\d{4}-\d{2}-\d{2}$";
    public const string Decimal = @"^-?\d+(\.\d+)?$";
    public const string Time = @"^\d{2}:\d{2}:\d{2}(?:\.\d+)?$";
    public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public const string IdnEmail = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
    public const string Hostname = @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$";
    public const string IdnHostname = @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$";
    public const string Ipv4 = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
    public const string Ipv6 = @"^([0-9a-fA-F]{1,4}:){7}([0-9a-fA-F]{1,4}|:)|(([0-9a-fA-F]{1,4}:){6}(:[0-9a-fA-F]{1,4}|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))|::([0-9a-fA-F]{1,4}:){5})";
    public const string Uri = @"^[\w+:\/\/]?[\w\d\.-]+\.[\w\d\.\/:%+&=?#-]+$";
    public const string UriReference = @"^[\w+:\/\/]?[\w\d\.-]+\.[\w\d\.\/:%+&=?#-]+$";
    public const string Uuid = @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[1-5][0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}$";
    public const string Iri = @"^[\u0000-\uFFFF]+$";
    public const string IriReference = @"^[\u0000-\uFFFF]+$";
    public const string UriTemplate = @"^\{?[^\}]+\}?$";
    public const string JsonPointer = @"^(\/[a-zA-Z0-9-_]+)*$";
    public const string RelativeJsonPointer = @"^(0|[1-9][0-9]*)(#[a-zA-Z0-9-_]+)?$";
}