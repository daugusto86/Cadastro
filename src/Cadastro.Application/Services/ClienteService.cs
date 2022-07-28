using Cadastro.Application.Interfaces;
using Cadastro.Application.ViewModels;
using Cadastro.Domain.Interfaces;

namespace Cadastro.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            throw new NotImplementedException();
        }
        
        public Task<ClienteViewModel> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }
        
        public Task<ClienteViewModel> ObterPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClienteViewModel>> ObterPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Adicionar(ClienteViewModel cliente)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Atualizar(ClienteViewModel cliente)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
