using Cadastro.Cliente.Domain.Interfaces;

namespace Cadastro.Cliente.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Models.Cliente>> ObterTodos()
        {
            return await clienteRepository.ObterTodos();
        }

        public async Task<IEnumerable<Models.Cliente>> ObterPorNome(string nome)
        {
            return await clienteRepository.Buscar(x => x.Nome == nome);
        }

        public async Task<Models.Cliente> ObterPorId(Guid id)
        {
            return await clienteRepository.ObterPorId(id);
        }

        public async Task<Models.Cliente> ObterPorEmail(string email)
        {
            return (await clienteRepository.Buscar(x => x.Email == email))
                .FirstOrDefault();
        }

        public async Task<bool> Adicionar(Models.Cliente cliente)
        {
            clienteRepository.Adicionar(cliente);
            return await clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Atualizar(Models.Cliente cliente)
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
