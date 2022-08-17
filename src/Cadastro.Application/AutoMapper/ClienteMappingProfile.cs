using AutoMapper;
using Cadastro.Cliente.Application.ViewModels;

namespace Cadastro.Cliente.Application.AutoMapper
{
    public class ClienteMappingProfile : Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<Domain.Models.Cliente, ClienteViewModel>().ReverseMap()
                .ConstructUsing(x => new Domain.Models.Cliente(x.Nome, x.Cpf, x.Email, x.DataCadastro));
        }
    }
}
