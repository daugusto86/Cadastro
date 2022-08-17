using Cadastro.Core.Notificacoes;

namespace Cadastro.Core.Interfaces
{
    public interface INotificador
    {
        void Handle(Notificacao notificacao);
        List<Notificacao> ObterNotificacoes();
        bool TemNotificacao();
    }
}
