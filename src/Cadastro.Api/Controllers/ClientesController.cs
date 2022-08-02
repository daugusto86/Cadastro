using Cadastro.Application.Interfaces;
using Cadastro.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientesController : MainController
    {
        private readonly IClienteService clienteService;

        public ClientesController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            return await clienteService.ObterTodos();
        }

        [HttpGet("{nome}")]
        public async Task<IEnumerable<ClienteViewModel>> ObterPorNome(string nome)
        {
            return await clienteService.ObterPorNome(nome);
        }

        [HttpGet("{email}")]
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


    }
}
