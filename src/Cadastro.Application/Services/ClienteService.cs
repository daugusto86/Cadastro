using AutoMapper;
using Cadastro.Application.Interfaces;
using Cadastro.Application.ViewModels;
using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Models;
using Cadastro.Domain.Models.Validations;

namespace Cadastro.Application.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IMapper mapper;

        public ClienteService(IClienteRepository clienteRepository, 
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            this.clienteRepository = clienteRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            var clientes = await clienteRepository.ObterTodos();
            return mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
        }
        
        public async Task<ClienteViewModel> ObterPorId(Guid id)
        {
            var cliente = await clienteRepository.ObterPorId(id);
            return mapper.Map<ClienteViewModel>(cliente);
        }
        
        public async Task<ClienteViewModel> ObterPorEmail(string email)
        {
            var cliente = (await clienteRepository.Buscar(x => x.Email == email))
                .FirstOrDefault();

            return mapper.Map<ClienteViewModel>(cliente);
        }

        public async Task<IEnumerable<ClienteViewModel>> ObterPorNome(string nome)
        {
            var clientes = await clienteRepository.Buscar(x => x.Nome == nome);
            return mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
        }

        public async Task<bool> Adicionar(ClienteViewModel cliente)
        {
            var model = mapper.Map<Cliente>(cliente);
            if (!ExecutarValidacao(new ClienteValidation(), model))
                return false;

            clienteRepository.Adicionar(model);

            return await clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Atualizar(ClienteViewModel cliente)
        {
            var model = mapper.Map<Cliente>(cliente);
            if (!ExecutarValidacao(new ClienteValidation(), model))
                return false;
            
            clienteRepository.Atualizar(model);

            return await clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Remover(Guid id)
        {
            clienteRepository.Remover(id);
            return await clienteRepository.UnitOfWork.Commit();
        }
    }
}
