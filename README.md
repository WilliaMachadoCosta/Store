
# Projeto Avaliação Técnica - CRUD de Produtos

Este projeto foi desenvolvido como parte de uma avaliação técnica, com o objetivo de criar uma API para realizar operações CRUD (Create, Read, Update, Delete) de produtos.

## Tecnologias Utilizadas

- **Plataforma**: .NET 8
- **Princípios de Design**: SOLID
- **Padrões de Arquitetura**: Repository Pattern, Domain-Driven Design (DDD)
- **Persistência de Dados**: Entity Framework Core (InMemory para este projeto)
- **Abordagem de Desenvolvimento**: Code First
- **GitHub Actions**: Pipe de testes acionado por Pull Request na branch Main

## Estrutura do Projeto

Esta solução é composta por 5 projetos, cada um com sua responsabilidade específica:

1. **Dominio**: Este projeto contém o coração da aplicação, definindo as entidades de domínio, como a entidade `Product`. Utiliza o conceito de "Domain-Driven Design" para modelar o domínio de forma rica e expressiva.
2. **Infra**: Responsável pela infraestrutura da aplicação, incluindo o acesso aos dados e a implementação do repositório utilizando o Entity Framework Core.
3. **Shared**: Contém componentes compartilhados entre diferentes partes da aplicação, como constantes, enums, e utilitários.
4. **Test**: Projeto que engloba os testes unitários para garantir o funcionamento adequado das funcionalidades implementadas.

