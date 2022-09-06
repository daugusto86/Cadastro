using Cadastro.Cliente.Domain.Interfaces;
using Cadastro.Cliente.Domain.Models;
using Cadastro.Cliente.Infra.Data.Context;
using Cadastro.Core.Data;
using Cadastro.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cadastro.Cliente.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly CadastroContext context;
        public IUnitOfWork UnitOfWork => context;

        public ClienteRepository(CadastroContext context)
        {
            this.context = context;
        }

        public async Task<PagedResult<Domain.Models.Cliente>> ObterTodosPaginado(int pageSize, int pageIndex)
        {
            var clientes = await context.Clientes.AsNoTracking()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalResults = await context.Clientes.CountAsync();

            var pagedResult = new PagedResult<Domain.Models.Cliente>
            {
                List = clientes,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalResults = totalResults
            };

            return pagedResult;
        }

        public async Task<List<Domain.Models.Cliente>> ObterTodos()
        {
            return await context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<Domain.Models.Cliente> ObterPorId(Guid id)
        {
            return await context.Clientes
                .Include(x => x.Enderecos)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Domain.Models.Cliente>> Buscar(Expression<Func<Domain.Models.Cliente, bool>> predicate)
        {
            return await context.Clientes.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<Endereco> ObterEnderecoPorId(Guid id)
        {
            return await context.Enderecos.FindAsync(id);
        }

        public void Adicionar(Domain.Models.Cliente entity)
        {
            context.Clientes.Add(entity);
        }

        public void Atualizar(Domain.Models.Cliente entity)
        {
            context.Entry(entity).Property(x => x.DataCadastro).IsModified = false;
            context.Clientes.Update(entity);
        }

        public async Task Remover(Guid id)
        {
            var cliente = await context.Clientes.FindAsync(id);
            context.Clientes.Remove(cliente);
        }

        public async Task RemoverEndereco(Guid id)
        {
            var endereco = await context.Enderecos.FindAsync(id);
            context.Enderecos.Remove(endereco);
        }

        public void Dispose() => context?.Dispose();
    }
}
