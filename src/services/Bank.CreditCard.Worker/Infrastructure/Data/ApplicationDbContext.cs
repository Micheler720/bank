using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Bank.Core.Data;
using Bank.Core.DomainObjects;
using Bank.Core.Mediator;
using Bank.Core.Messages;
using Bank.CreditCard.Worker.Domain;
using Bank.CreditCard.Worker.Infrastructure.Data.DbMappings;
using Microsoft.EntityFrameworkCore;

namespace Bank.CreditCard.Worker.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    public DbSet<CreditCardEntity> CreditCards { get; set; }

    public ApplicationDbContext()
    {

    }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=127.0.0.1;Port=5433;Database=bank;User Id=postgres;enlist=true;Password=Bank@2024;";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.ApplyConfiguration(new CreditCardEntityMapping());
    }

    public async Task<bool> CommitAsync()
    {
        UpdadteCreateAndUpdatedDate();

        return await base.SaveChangesAsync() > 0;
    }

    private void UpdadteCreateAndUpdatedDate()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Entity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((Entity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((Entity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
            }
        }
    }

}
