using Cadastro.Cliente.Application.ViewModels;

namespace Cadastro.Cliente.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteViewModel>> ObterTodos();
        Task<ClienteViewModel> ObterPorId(Guid id);
        Task<ClienteViewModel> ObterPorEmail(string email);
        Task<IEnumerable<ClienteViewModel>> ObterPorNome(string nome);
        Task<bool> Adicionar(ClienteViewModel cliente);
        Task<bool> Atualizar(ClienteViewModel cliente);
        Task<bool> Remover(Guid id);
    }
}
