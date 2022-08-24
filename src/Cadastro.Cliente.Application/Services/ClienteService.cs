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

            if (!await EmailValido(model.Id, cliente.Email))
                return false;

            if (!await CpfValido(model.Id, cliente.Cpf))
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

            if (!await EmailValido(cliente.Id, cliente.Email))
                return false;

            model.MudarEmail(cliente.Email);

            if (!ExecutarValidacao(model.ValidationResult))
                return false;

            return await clienteService.Atualizar(model);
        }

        public async Task<ClienteViewModel> Ativar(Guid id)
        {
            var cliente = await clienteService.ObterPorId(id);

            if (cliente == null)
            {
                Notificar("Cliente não encontrado.");
                return null;
            }

            cliente.Ativar();
            await clienteService.Atualizar(cliente);

            return mapper.Map<ClienteViewModel>(cliente);
        }

        public async Task<ClienteViewModel> Desativar(Guid id)
        {
            var cliente = await clienteService.ObterPorId(id);

            if (cliente == null)
            {
                Notificar("Cliente não encontrado.");
                return null;
            }

            cliente.Desativar();
            await clienteService.Atualizar(cliente);

            return mapper.Map<ClienteViewModel>(cliente);
        }

        public async Task<bool> Remover(Guid id)
        {
            return await clienteService.Remover(id);
        }

        private async Task<bool> EmailValido(Guid id, string email)
        {
            var emUso = (await clienteService.Buscar(x => x.Email.Endereco == email && x.Id != id)).Any();
            
            if (!emUso) return true;

            Notificar("O e-mail informado já está utilizado. Informe outro.");
            return false;
        }

        private async Task<bool> CpfValido(Guid id, string cpf)
        {
            cpf = cpf?.Trim().Replace(".", "").Replace("-", "");
            var emUso = (await clienteService.Buscar(x => x.Cpf.Numero == cpf && x.Id != id)).Any();
            
            if (!emUso) return true;

            Notificar("O CPF informado já está cadastrado no sistema.");
            return false;
        }
    }
}
