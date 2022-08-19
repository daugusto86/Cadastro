using AutoMapper;
using Cadastro.Cliente.Application.ViewModels;
using Cadastro.Core.Interfaces;

namespace Cadastro.Cliente.Application.Services
{
    public class ClienteService : BaseService, Interfaces.IClienteService
    {
        private readonly Domain.Interfaces.IClienteService clienteService;
        private readonly IMapper mapper;

        public ClienteService(Domain.Interfaces.IClienteService clienteService,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            this.clienteService = clienteService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            var clientes = await clienteService.ObterTodos();
            return mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
        }

        public async Task<ClienteViewModel> ObterPorId(Guid id)
        {
            var cliente = await clienteService.ObterPorId(id);
            return mapper.Map<ClienteViewModel>(cliente);
        }

        public async Task<ClienteViewModel> ObterPorEmail(string email)
        {
            var cliente = await clienteService.ObterPorEmail(email);

            return mapper.Map<ClienteViewModel>(cliente);
        }

        public async Task<IEnumerable<ClienteViewModel>> ObterPorNome(string nome)
        {
            var clientes = await clienteService.ObterPorNome(nome);
            return mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
        }

        public async Task<bool> Adicionar(NovoClienteViewModel cliente)
        {
            var model = mapper.Map<Domain.Models.Cliente>(cliente);
            if (!ExecutarValidacao(model.ValidationResult))
                return false;

            if (await EmailUtilizado(model.Id, cliente.Email))
                return false;

            return await clienteService.Adicionar(model);
        }

        public async Task<bool> Atualizar(AtualizarClienteViewModel cliente)
        {
            var model = await clienteService.ObterPorId(cliente.Id);

            if (model == null)
            {
                Notificar("Cliente não encontrado.");
                return false;
            }

            model.MudarNome(cliente.Nome);

            if (!ExecutarValidacao(model.ValidationResult))
                return false;

            return await clienteService.Atualizar(model);
        }

        public async Task<bool> AtualizarEmail(AtualizarEmailClienteViewModel cliente)
        {
            var model = await clienteService.ObterPorId(cliente.Id);

            if (model == null)
            {
                Notificar("Cliente não encontrado.");
                return false;
            }

            if (await EmailUtilizado(cliente.Id, cliente.Email))
                return false;

            model.MudarEmail(cliente.Email);

            if (!ExecutarValidacao(model.ValidationResult))
                return false;

            return await clienteService.Atualizar(model);
        }

        private async Task<bool> EmailUtilizado(Guid id, string email)
        {
            var emailJaCadastrado = await clienteService.Buscar(x => x.Email == email && x.Id != id);
            if (emailJaCadastrado != null)
            {
                Notificar("O e-mail informado já está utilizado. Informe outro.");
                return true;
            }

            return false;
        }

        public async Task<bool> Remover(Guid id)
        {
            return await clienteService.Remover(id);
        }
    }
}
