# Vendinha Plena

Sistema simples para controle de clientes e dívidas de uma vendinha.

O projeto permite cadastrar clientes, consultar os dados cadastrados e controlar as dívidas penduradas de cada cliente.

## Funcionalidades

- Cadastro de clientes
- Listagem de clientes com paginação
- Busca de cliente pelo CPF
- Pesquisa de cliente pelo nome
- Alteração de cliente
- Exclusão de cliente
- Cadastro de dívida
- Listagem das dívidas de um cliente
- Pagamento de dívida
- Cálculo do total em aberto
- Ordenação dos clientes de quem mais deve para quem menos deve
- Bloqueio de CPF repetido
- Bloqueio de mais de uma dívida em aberto por cliente

## Tecnologias utilizadas

- C#
- .NET 10
- Entity Framework Core
- SQLite

## Estrutura do projeto

```text
Data/
    VendinhaDbContext.cs

Models/
    Cliente.cs
    Divida.cs

Services/
    ClienteService.cs
    DividaService.cs

Program.cs
schema.sql
```

## Como executar

Abra o terminal na pasta em que está o arquivo `Vendinha_TrabalhoFinal.csproj`.

Restaure os pacotes:

```bash
dotnet restore
```

Compile o projeto:

```bash
dotnet build
```

Execute o programa:

```bash
dotnet run
```

O arquivo `vendinha.db` será criado automaticamente na primeira execução.

## Banco de dados

O projeto utiliza SQLite.

O arquivo `schema.sql` contém o script de criação das tabelas:

- `clientes`
- `dividas`

A tabela `dividas` possui uma chave estrangeira para relacionar cada dívida ao CPF de um cliente.

## Observação

O CPF é validado pelo formato:

```text
000.000.000-00
```

A aplicação não realiza o cálculo matemático dos dígitos verificadores do CPF.
