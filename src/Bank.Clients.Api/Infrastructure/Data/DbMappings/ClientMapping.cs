using Bank.Clients.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Clients.Api.Infrastructure.Data.DbMappings;

public class ClientMapping : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
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

        builder.Property(x => x.Name)
            .HasColumnType("varchar(100)")
            .HasColumnName("name");
        
        builder.Property(x => x.Document)
            .HasColumnType("varchar(100)")
            .HasColumnName("document");
        
        builder.Property(x => x.Email)
            .HasColumnType("varchar(100)")
            .HasColumnName("email");

        builder.Property(x => x.BirthDate)
            .HasColumnType("timestamptz")
            .HasColumnName("birth_date");
        
        builder.ToTable("clients");
    }
}