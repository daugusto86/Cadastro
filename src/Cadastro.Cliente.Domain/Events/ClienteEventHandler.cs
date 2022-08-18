using Cadastro.Cliente.Domain.Interfaces;
using MediatR;

namespace Cadastro.Cliente.Domain.Events
{
    public class ClienteEventHandler : INotificationHandler<NotificarCadastroEvent>
    {
        private readonly IEmailService emailService;

        public ClienteEventHandler(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public async Task Handle(NotificarCadastroEvent mensagem, CancellationToken cancellationToken)
        {
            var corpo = $"Foi realizado um novo cadastro Email: {mensagem.Email} CPF: {mensagem.Cpf}";
            await emailService.EnviarEmail(para: "teste@teste.com", cc: "", cco: "", corpo: corpo, html: true);
        }
    }
}
