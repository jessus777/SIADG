using System.Data;

namespace SIADG.Application.Contracts.Interfaces;

public interface IUnitOfWork
    : IDisposable, IAsyncDisposable
{
    //void BeginWork();

    //void SaveWork();
    //Task SaveWorkAsync(CancellationToken cancellationToken);

    //void DiscardWork();
    //Task DiscardWorkAsync(CancellationToken cancellationToken);
    // Transacciones
    void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

    // Confirmar cambios
    int Commit();
    Task<int> CommitAsync(CancellationToken cancellationToken);

    // Descartar cambios
    void Rollback();
    Task RollbackAsync(CancellationToken cancellationToken);

    // Ejecutar operaciones en transacción (patrón Execute Around)
    void ExecuteInTransaction(Action<IUnitOfWork> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    Task ExecuteInTransactionAsync(Func<IUnitOfWork, Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);
}