using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Models;
using Cadastro.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Infra.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        private readonly CadastroContext context;
        protected readonly DbSet<TEntity> DbSet;
        public IUnitOfWork UnitOfWork => context;

        public Repository(CadastroContext context)
        {
            this.context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> Buscar(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public void Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
        }

        public void Dispose() => context?.Dispose();
        
    }
}
