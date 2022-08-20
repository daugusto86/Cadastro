﻿using Cadastro.Core.Data;
using System.Linq.Expressions;

namespace Cadastro.Cliente.Domain.Interfaces
{
    public interface IClienteRepository : IRepository<Models.Cliente>
    {
        Task<List<Models.Cliente>> ObterTodos();

        Task<Models.Cliente> ObterPorId(Guid id);

        Task<IEnumerable<Models.Cliente>> Buscar(Expression<Func<Models.Cliente, bool>> predicate);

        void Adicionar(Models.Cliente entity);

        void Atualizar(Models.Cliente entity);

        Task Remover(Guid id);
    }
}