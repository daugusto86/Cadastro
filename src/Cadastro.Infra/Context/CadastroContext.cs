using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Infra.Context
{
    public class CadastroContext : DbContext, IUnitOfWork
    {
        public CadastroContext(DbContextOptions<CadastroContext> options) 
            : base(options) { }
        
        public DbSet<Cliente> Clientes { get; set; }

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
