
using Cadastro.Cliente.Domain.Models;
using System.Linq.Expressions;

namespace Cadastro.Cliente.Domain.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Models.Cliente>> ObterTodos();
        Task<Models.Cliente> ObterPorId(Guid id);
        Task<Models.Cliente> ObterPorEmail(string email);
        Task<IEnumerable<Models.Cliente>> ObterPorNome(string nome);
        Task<IEnumerable<Models.Cliente>> Buscar(Expression<Func<Models.Cliente, bool>> predicate);
        Task<Endereco> ObterEnderecoPorId(Guid id);
        Task<bool> Adicionar(Models.Cliente cliente);
        Task<bool> Atualizar(Models.Cliente cliente);
        Task<bool> Remover(Guid id);
        Task<bool> RemoverEndereco(Guid id);
    }
}
