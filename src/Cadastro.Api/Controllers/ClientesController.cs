using Cadastro.Api.Extensions;
using Cadastro.Cliente.Application.Interfaces;
using Cadastro.Cliente.Application.ViewModels;
using Cadastro.Core.DomainObjects;
using Cadastro.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientesController : MainController
    {
        private readonly IClienteService clienteService;

        public ClientesController(IClienteService clienteService, 
            INotificador notificador) : base(notificador)
        {
            this.clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            return await clienteService.ObterTodos();
        }

        [HttpGet("clientes-paginado")]
        public async Task<PagedResult<ClienteViewModel>> ObterTodosPaginado([FromQuery] int pageSize = 15, [FromQuery] int pageIndex = 1)
        {
            return await clienteService.ObterTodosPaginado(pageSize, pageIndex);
        }

        [HttpGet("nome/{nome}")]
        public async Task<IEnumerable<ClienteViewModel>> ObterPorNome(string nome)
        {
            return await clienteService.ObterPorNome(nome);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<ClienteViewModel>> ObterPorEmail(string email)
        {
            var cliente = await clienteService.ObterPorEmail(email);

            if (cliente == null) return NotFound();

            return cliente;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> ObterPorId(Guid id)
        {
            var cliente = await clienteService.ObterPorId(id);

            if (cliente == null) return NotFound();

            return cliente;
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<NovoClienteViewModel>> Adicionar(NovoClienteViewModel cliente)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await clienteService.Adicionar(cliente);

            return CustomResponse(cliente);
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AtualizarClienteViewModel>> Atualizar(Guid id, AtualizarClienteViewModel cliente)
        {
            if (id != cliente.Id)
            {
                NotificarErro("Id informado na query diferente do Id informado no post");
                return CustomResponse(cliente);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await clienteService.Atualizar(cliente);

            return CustomResponse(cliente);
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Atualizar")]
        [HttpPut("atualizar-email/{id:guid}")]
        public async Task<ActionResult<AtualizarEmailClienteViewModel>> AtualizarEmail(Guid id, AtualizarEmailClienteViewModel cliente)
        {
            if (id != cliente.Id)
            {
                NotificarErro("Id informado na query diferente do Id informado no post");
                return CustomResponse(cliente);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await clienteService.AtualizarEmail(cliente);

            return CustomResponse(cliente);
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Atualizar")]
        [HttpPut("ativar/{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Ativar(Guid id)
        {
            var clienteVm = await clienteService.Ativar(id);

            return CustomResponse(clienteVm);
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Atualizar")]
        [HttpPut("desativar/{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Desativar(Guid id)
        {
            var clienteVm = await clienteService.Desativar(id);

            return CustomResponse(clienteVm);
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Excluir(Guid id)
        {
            var cliente = await clienteService.ObterPorId(id);

            if (cliente == null) return NotFound();

            await clienteService.Remover(id);

            return CustomResponse(cliente);
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Adicionar")]
        [HttpPost("novo-endereco")]
        public async Task<ActionResult<EnderecoViewModel>> AdicionarEndereco(EnderecoViewModel endereco)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await clienteService.AdicionarEndereco(endereco);

            return CustomResponse(endereco);
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Atualizar")]
        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<ActionResult<EnderecoViewModel>> AtualizarEndereco(Guid id, EnderecoViewModel endereco)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != endereco.Id)
            {
                NotificarErro("Id informado na query diferente do Id informado no post");
                return CustomResponse(endereco);
            }

            await clienteService.AtualizarEndereco(endereco);

            return CustomResponse(endereco);
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Excluir")]
        [HttpDelete("excluir-endereco/{id:guid}")]
        public async Task<ActionResult<EnderecoViewModel>> ExcluirEndereco(Guid id)
        {
            var endereco = await clienteService.ObterEnderecoPorId(id);

            if (endereco == null) return NotFound();

            await clienteService.RemoverEndereco(id);

            return CustomResponse(endereco);
        }
    }
}
