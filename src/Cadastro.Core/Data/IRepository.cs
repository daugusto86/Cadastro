using Cadastro.Core.DomainObjects;
using System.Linq.Expressions;

namespace Cadastro.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
