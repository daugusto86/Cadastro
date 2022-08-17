using Cadastro.Api.Extensions;
using Cadastro.Cliente.Application.Interfaces;
using Cadastro.Cliente.Application.ViewModels;
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
        public async Task<ActionResult<ClienteViewModel>> Adicionar(ClienteViewModel cliente)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await clienteService.Adicionar(cliente);

            return CustomResponse(cliente);
        }

        [Authorize]
        [ClaimsAuthorize("Cliente", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Atualizar(Guid id, ClienteViewModel cliente)
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
        [ClaimsAuthorize("Cliente", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Excluir(Guid id)
        {
            var cliente = await clienteService.ObterPorId(id);

            if (cliente == null) return NotFound();

            await clienteService.Remover(id);

            return CustomResponse(cliente);
        }
    }
}
