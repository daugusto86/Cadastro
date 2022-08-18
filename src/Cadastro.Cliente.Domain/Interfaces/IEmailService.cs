namespace Cadastro.Cliente.Domain.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmail(string para, string cc, string cco, string corpo, bool html);
    }
}
