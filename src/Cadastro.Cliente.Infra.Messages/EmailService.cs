using Cadastro.Cliente.Domain.Interfaces;

namespace Cadastro.Cliente.Infra.Messages
{
    public class EmailService : IEmailService
    {
        public Task EnviarEmail(string para, string cc, string cco, string corpo, bool html)
        {

            // TODO: envia mensagem de e-mail

            return Task.CompletedTask;
        }
    }
}