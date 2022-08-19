using Cadastro.Cliente.Domain.Events;
using Cadastro.Cliente.Domain.Interfaces;
using Cadastro.Core.Mediator;
using System.Linq.Expressions;

namespace Cadastro.Cliente.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IMediatorHandler mediator;

        public ClienteService(IClienteRepository clienteRepository, IMediatorHandler mediator)
        {
            this.clienteRepository = clienteRepository;
            this.mediator = mediator;
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

        public async Task<IEnumerable<Models.Cliente>> Buscar(Expression<Func<Models.Cliente, bool>> predicate)
        {
            return await clienteRepository.Buscar(predicate);
        }

        public async Task<bool> Adicionar(Models.Cliente cliente)
        {
            clienteRepository.Adicionar(cliente);

            await mediator.PublicarEvento(new NotificarCadastroEvent(cliente.Id, cliente.Cpf, cliente.Email));

            return await clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Atualizar(Models.Cliente cliente)
        {
            clienteRepository.Atualizar(cliente);
            return await clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Remover(Guid id)
        {
            await clienteRepository.Remover(id);
            return await clienteRepository.UnitOfWork.Commit();
        }
    }
}
