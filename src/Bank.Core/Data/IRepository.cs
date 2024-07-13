namespace Bank.Core.Data; 

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }

}