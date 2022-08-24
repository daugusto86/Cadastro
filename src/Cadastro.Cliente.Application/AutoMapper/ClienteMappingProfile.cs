using AutoMapper;
using Cadastro.Cliente.Application.ViewModels;

namespace Cadastro.Cliente.Application.AutoMapper
{
    public class ClienteMappingProfile : Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<Domain.Models.Cliente, ClienteViewModel>()
                .ForMember(dest => dest.Id, orig => orig.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nome, orig => orig.MapFrom(src => src.Nome))
                .ForPath(dest => dest.Cpf, orig => orig.MapFrom(src => src.Cpf.Numero))
                .ForPath(dest => dest.Email, orig => orig.MapFrom(src => src.Email.Endereco))
                .ForMember(dest => dest.DataCadastro, orig => orig.MapFrom(src => src.DataCadastro))
                .ForMember(dest => dest.Ativo, orig => orig.MapFrom(src => src.Ativo))
                .ReverseMap()
                .ConstructUsing(x => new Domain.Models.Cliente(x.Nome, x.Cpf, x.Email))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Domain.Models.Cliente, NovoClienteViewModel>()
                .ReverseMap()
                .ConstructUsing(x => new Domain.Models.Cliente(x.Nome, x.Cpf, x.Email))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
