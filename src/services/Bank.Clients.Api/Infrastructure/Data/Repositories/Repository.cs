using System.Diagnostics.CodeAnalysis;
using Bank.Core.Data;

namespace Bank.Clients.Api.Infrastructure.Data.Repositories;

[ExcludeFromCodeCoverage]
public abstract class Repository : IRepository
{
    protected readonly ApplicationDbContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
}