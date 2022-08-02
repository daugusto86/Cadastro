﻿using AutoMapper;
using Cadastro.Application.ViewModels;
using Cadastro.Domain.Models;

namespace Cadastro.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>();

            // exemplo mapeamento com paramentros no construtor
            //CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
            //   .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));
            //CreateMap<CustomerViewModel, UpdateCustomerCommand>()
            //    .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));
        }
    }
}
