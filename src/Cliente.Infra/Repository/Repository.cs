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

        public Task<List<TEntity>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> Buscar(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Adicionar(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose() => context?.Dispose();
        
    }
}
