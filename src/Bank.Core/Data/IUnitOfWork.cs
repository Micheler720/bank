namespace Bank.Core.Data;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();    
}