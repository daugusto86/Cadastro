using Cadastro.Cliente.Application.ViewModels;
using Cadastro.Core.DomainObjects;

namespace Cadastro.Cliente.Application.Interfaces
{
    public interface IClienteService
    {
        Task<PagedResult<ClienteViewModel>> ObterTodosPaginado(int pageSize, int pageIndex);
        Task<IEnumerable<ClienteViewModel>> ObterTodos();
        Task<ClienteViewModel> ObterPorId(Guid id);
        Task<ClienteViewModel> ObterPorEmail(string email);
        Task<IEnumerable<ClienteViewModel>> ObterPorNome(string nome);
        Task<bool> Adicionar(NovoClienteViewModel cliente);
        Task<bool> Atualizar(AtualizarClienteViewModel cliente);
        Task<bool> AtualizarEmail(AtualizarEmailClienteViewModel cliente);
        Task<ClienteViewModel> Ativar(Guid id);
        Task<ClienteViewModel> Desativar(Guid id);
        Task<EnderecoViewModel> ObterEnderecoPorId(Guid id);
        Task<bool> AdicionarEndereco(EnderecoViewModel enderecoVm);
        Task<bool> AtualizarEndereco(EnderecoViewModel enderecoVm);
        Task<bool> RemoverEndereco(Guid id);
        Task<bool> Remover(Guid id);
    }
}
