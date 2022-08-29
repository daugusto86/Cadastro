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
                .ForMember(dest => dest.Id, orig => orig.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdCliente, orig => orig.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.Logradouro, orig => orig.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Numero, orig => orig.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Complemento, orig => orig.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.Bairro, orig => orig.MapFrom(src => src.Bairro))
                .ForMember(dest => dest.Cep, orig => orig.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Cidade, orig => orig.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Estado, orig => orig.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Principal, orig => orig.MapFrom(src => src.Principal))
                .ReverseMap()
                .ConstructUsing(x => new Endereco(x.Logradouro, x.Numero, x.Complemento, 
                    x.Bairro, x.Cep, x.Cidade, x.Estado, x.IdCliente, x.Principal))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
