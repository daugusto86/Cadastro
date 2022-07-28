using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Models;
using Cadastro.Infra.Context;

namespace Cadastro.Infra.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(CadastroContext context)
            : base(context) { }
    }
}
