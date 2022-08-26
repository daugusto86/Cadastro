namespace Cadastro.Cliente.Application.ViewModels
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<EnderecoViewModel> Enderecos { get; set; } = new List<EnderecoViewModel>();
    }
}
