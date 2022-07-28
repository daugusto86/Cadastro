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

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
