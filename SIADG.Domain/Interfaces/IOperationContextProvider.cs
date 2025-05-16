namespace SIADG.Domain.Interfaces;
public interface IOperationContextProvider
{
    void SetContext(OperationContext operationContext);
    OperationContext GetContext();
}
