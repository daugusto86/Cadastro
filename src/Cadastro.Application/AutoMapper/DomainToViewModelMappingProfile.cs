using AutoMapper;
using Cadastro.Application.ViewModels;
using Cadastro.Domain.Models;

namespace Cadastro.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>();
        }
    }
}
