namespace SIADG.Api.Security;

public class InvalidApiKeyFormatException : Exception
{
    public InvalidApiKeyFormatException() : base("Formato de API Key no válido.")
    {
    }

    public InvalidApiKeyFormatException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public InvalidApiKeyFormatException(Exception innerException) : base("Formato de API Key no válido.", innerException)
    {
    }
}
