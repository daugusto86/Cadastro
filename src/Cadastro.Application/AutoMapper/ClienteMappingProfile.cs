using AutoMapper;
using Cadastro.Application.ViewModels;
using Cadastro.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Application.AutoMapper
{
    public class ClienteMappingProfile : Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>().ReverseMap()
                .ConstructUsing(x => new Cliente(x.Nome, x.Cpf, x.Email, x.DataCadastro));
        }
    }
}
