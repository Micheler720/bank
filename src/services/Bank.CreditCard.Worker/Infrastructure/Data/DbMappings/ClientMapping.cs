using System.Diagnostics.CodeAnalysis;
using Bank.CreditCard.Worker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.CreditCard.Worker.Infrastructure.Data.DbMappings;

[ExcludeFromCodeCoverage]
public class CreditCardEntityMapping : IEntityTypeConfiguration<CreditCardEntity>
{
    public void Configure(EntityTypeBuilder<CreditCardEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(x => x.Id)
            .HasColumnType("varchar(36)")
            .HasColumnName("id");

        builder.Property(x => x.CreatedDate)
            .HasColumnType("timestamptz")
            .HasColumnName("created_date");

        builder.Property(x => x.UpdatedDate)
            .HasColumnType("timestamptz")
            .HasColumnName("updated_date");

        builder.Property(x => x.ClientId)
            .HasColumnType("varchar(36)")
            .HasColumnName("client_id");

        builder.Property(x => x.CardNumber)
            .HasColumnType("varchar(100)")
            .HasColumnName("card_number");
        
        builder.Property(x => x.CreditLimit)
            .HasColumnType("decimal(10, 2)")
            .HasColumnName("credit_limit");
        
        builder.Property(x => x.SecurityCode)
            .HasColumnType("varchar(10)")
            .HasColumnName("security_code");
        
        builder.ToTable("credit_cards");
    }
}