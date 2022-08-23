using Cadastro.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Cliente.Infra.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Domain.Models.Cliente>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.OwnsOne(x => x.Cpf, p =>
            {
                p.Property(x => x.Numero)
                .IsRequired()
                .HasMaxLength(Cpf.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(x => x.Ativo)
                .IsRequired()
                .HasColumnType("bit");
            
            builder.ToTable("Clientes");
        }
    }
}
