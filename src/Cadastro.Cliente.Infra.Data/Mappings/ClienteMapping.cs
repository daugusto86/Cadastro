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

            // mapeamento de Objeto de Valor
            builder.OwnsOne(x => x.Cpf, p =>
            {
                p.Property(x => x.Numero)
                .IsRequired()
                .HasMaxLength(Cpf.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            // mapeamento de Objeto de Valor
            builder.OwnsOne(x => x.Email, p =>
            {
                p.Property(x => x.Endereco)
                .IsRequired()
                .HasMaxLength(Email.EnderecoMaxLength)
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.EnderecoMaxLength})");
            });

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(x => x.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            // 1 : N => Cliente : Enderecos
            builder.HasMany(x => x.Enderecos)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.IdCliente);

            builder.ToTable("Clientes");
        }
    }
}
