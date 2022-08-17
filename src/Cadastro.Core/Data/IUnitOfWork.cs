namespace Cadastro.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
