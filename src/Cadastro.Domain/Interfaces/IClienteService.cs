using Cadastro.Domain.Models;

namespace Cadastro.Domain.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Cliente> ObterPorId(Guid id);
        Task<Cliente> ObterPorEmail(string email);
        Task<IEnumerable<Cliente>> ObterPorNome(string nome);
        Task<bool> Adicionar(Cliente cliente);
        Task<bool> Atualizar(Cliente cliente);
        Task<bool> Remover(Guid id);
    }
}
