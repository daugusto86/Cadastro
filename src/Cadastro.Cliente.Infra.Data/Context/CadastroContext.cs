using Cadastro.Cliente.Domain.Models;
using Cadastro.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Cliente.Infra.Data.Context
{
    public class CadastroContext : DbContext, IUnitOfWork
    {
        public CadastroContext(DbContextOptions<CadastroContext> options)
            : base(options) { }

        public DbSet<Domain.Models.Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var stringProperties = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(x => x.GetProperties().Where(x => x.ClrType == typeof(string)));

            foreach (var property in stringProperties)
                property.SetColumnType("varchar(255)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadastroContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
