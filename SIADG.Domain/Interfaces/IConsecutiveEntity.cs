namespace SIADG.Domain.Interfaces;
public interface IConsecutiveEntity
    : IEntity
{
    long Consecutivo { get; }
}
