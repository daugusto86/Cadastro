using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Models;

namespace Cadastro.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await clienteRepository.ObterTodos();
        }

        public async Task<IEnumerable<Cliente>> ObterPorNome(string nome)
        {
            return await clienteRepository.Buscar(x => x.Nome == nome);
        }

        public async Task<Cliente> ObterPorId(Guid id)
        {
            return await clienteRepository.ObterPorId(id);
        }

        public async Task<Cliente> ObterPorEmail(string email)
        {
            return (await clienteRepository.Buscar(x => x.Email == email))
                .FirstOrDefault();
        }

        public async Task<bool> Adicionar(Cliente cliente)
        {
            clienteRepository.Adicionar(cliente);
            return await clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Atualizar(Cliente cliente)
        {
            clienteRepository.Atualizar(cliente);
            return await clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Remover(Guid id)
        {
            clienteRepository.Remover(id);
            return await clienteRepository.UnitOfWork.Commit();
        }
    }
}
