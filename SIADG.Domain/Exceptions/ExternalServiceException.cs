namespace SIADG.Domain.Exceptions;

public abstract class ExternalServiceException 
    : Exception
{
    protected ExternalServiceException(string? message, Exception? innerException = null) 
        : base(message, innerException)
    {
    }
}