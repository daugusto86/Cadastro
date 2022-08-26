using AutoMapper;
using Cadastro.Cliente.Application.ViewModels;
using Cadastro.Cliente.Domain.Models;

namespace Cadastro.Cliente.Application.AutoMapper
{
    public class EnderecoMappingProfile : Profile
    {
        public EnderecoMappingProfile()
        {
            CreateMap<Endereco, EnderecoViewModel>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ReverseMap()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
