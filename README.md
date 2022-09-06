# Sobre o Projeto
Este projeto tem por finalidade demonstrar a utilização de boas práticas de desenvolvimento, tendo por base os conceitos de DDD, Clean Code, SOLID, entre outros. A utilização de padrões como Repository e Unit of Work. Demonstrar a utilização de lançamento de eventos atravéz do Mediator, e da troca de Notificações entre as camadas.

## Autenticação
Para autenticação e autorização foi utilizado Asp Net Core Identity e Json Web Token (JWT).

### Bibliotecas
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.UI

## Banco de Dados e Ferramenta de ORM
Para o banco de dados foi utilizado o Microsoft Sql Server, e como ferramenta de ORM foi utilizado o Entity Framework Core.

### Bibliotecas
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Relational
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

## Testes
Para desenvolver os testes de unidade foi utilizado o xUnit. Para gerar massa de dados para teste foi utilizado o Bogus, e para geração de Mocks o Moq.

### Bibliotecas
- Bogus
- Moq
- Moq.Automock
- xUnit

## Outras Bibliotecas Utilizadas
- AutoMapper
- AutoMapper.Extensions.Microsoft.DependencyInjection
- Mediatr
- Mediatr.Extensions.Microsoft.DependencyInjection
- FluentValidation

## Referências
- [https://github.com/bchavez/Bogus](https://github.com/bchavez/Bogus)
- [https://github.com/moq/moq4](https://github.com/moq/moq4)
- [https://xunit.net/](https://xunit.net/)
- [https://automapper.org/](https://automapper.org/)
- [https://github.com/jbogard/MediatR](https://github.com/jbogard/MediatR)
- [https://docs.fluentvalidation.net/en/latest/](https://docs.fluentvalidation.net/en/latest/)
