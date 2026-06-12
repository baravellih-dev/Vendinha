using Microsoft.EntityFrameworkCore;
using Vendinha_TrabalhoFinal.Data;
using Vendinha_TrabalhoFinal.Models;
using Vendinha_TrabalhoFinal.Services;

using var context = new VendinhaDbContext();
context.Database.EnsureCreated();

var clienteService = new ClienteService();
var dividaService = new DividaService();

while (true)
{
    Console.Clear();

    Console.WriteLine("1 - Cadastrar cliente");
    Console.WriteLine("2 - Listar clientes");
    Console.WriteLine("3 - Buscar cliente");
    Console.WriteLine("4 - Pesquisar cliente pelo nome");
    Console.WriteLine("5 - Alterar cliente");
    Console.WriteLine("6 - Excluir cliente");
    Console.WriteLine("7 - Cadastrar dívida");
    Console.WriteLine("8 - Listar dívidas do cliente");
    Console.WriteLine("9 - Pagar dívida");
    Console.WriteLine("0 - Sair");
    Console.WriteLine();
    Console.WriteLine("Digite uma opção:");

    int opcao;

    try
    {
        opcao = int.Parse(Console.ReadLine());
    }
    catch (FormatException excecao)
    {
        Console.WriteLine("Opção inválida: {0}", excecao.Message);
        Console.ReadKey();
        continue;
    }

    if (opcao == 0)
    {
        break;
    }

    else if (opcao == 1)
    {
        Console.WriteLine("Digite o nome:");
        var nome = Console.ReadLine();

        Console.WriteLine("Digite o CPF:");
        var cpf = Console.ReadLine();

        Console.WriteLine("Digite a data de nascimento:");
        var dataNascimento = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Digite o email:");
        var email = Console.ReadLine();

        var cliente = new Cliente
        {
            Nome = nome,
            CPF = cpf,
            DataNascimento = dataNascimento,
            Email = email
        };

        var sucesso = clienteService.Criar(cliente);

        if (!sucesso)
        {
            Console.WriteLine("Erro ao cadastrar cliente");
        }
        else
        {
            Console.WriteLine("Cliente cadastrado");
        }
    }

    else if (opcao == 2)
    {
        Console.WriteLine("Digite a página:");
        var pagina = int.Parse(Console.ReadLine());

        var clientes = clienteService.Listar(10, pagina);

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente encontrado");
        }

        foreach (var item in clientes)
        {
            item.PrintDados();
            Console.WriteLine("Total em aberto: R$ {0:F2}",
                clienteService.TotalDividas(item.CPF));
            Console.WriteLine("====================");
        }
    }

    else if (opcao == 3)
    {
        Console.WriteLine("Digite o CPF:");
        var cpf = Console.ReadLine();

        var cliente = clienteService.Buscar(cpf);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado");
        }
        else
        {
            cliente.PrintDados();
            Console.WriteLine("Total em aberto: R$ {0:F2}",
                clienteService.TotalDividas(cliente.CPF));
        }
    }

    else if (opcao == 4)
    {
        Console.WriteLine("Pesquisa:");
        var pesquisa = Console.ReadLine();

        Console.WriteLine("Digite a página:");
        var pagina = int.Parse(Console.ReadLine());

        var clientes = clienteService.Pesquisa(
            pesquisa,
            10,
            pagina
        );

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente encontrado");
        }

        foreach (var item in clientes)
        {
            item.PrintDados();
            Console.WriteLine("Total em aberto: R$ {0:F2}",
                clienteService.TotalDividas(item.CPF));
            Console.WriteLine("====================");
        }
    }

    else if (opcao == 5)
    {
        Console.WriteLine("Digite o CPF:");
        var cpf = Console.ReadLine();

        var cliente = clienteService.Buscar(cpf);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado");
        }
        else
        {
            Console.WriteLine("Digite o nome:");
            var nome = Console.ReadLine();

            Console.WriteLine("Digite a data de nascimento:");
            var dataNascimento = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Digite o email:");
            var email = Console.ReadLine();

            cliente.Nome = nome;
            cliente.DataNascimento = dataNascimento;
            cliente.Email = email;

            var sucesso = clienteService.Alterar(cliente);

            if (!sucesso)
            {
                Console.WriteLine("Erro ao alterar cliente");
            }
            else
            {
                Console.WriteLine("Cliente alterado");
            }
        }
    }

    else if (opcao == 6)
    {
        Console.WriteLine("Digite o CPF:");
        var cpf = Console.ReadLine();

        var sucesso = clienteService.Excluir(cpf);

        if (!sucesso)
        {
            Console.WriteLine("Cliente não encontrado");
        }
        else
        {
            Console.WriteLine("Cliente excluído");
        }
    }

    else if (opcao == 7)
    {
        Console.WriteLine("Digite o CPF:");
        var cpf = Console.ReadLine();

        var cliente = clienteService.Buscar(cpf);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado");
        }
        else
        {
            Console.WriteLine("Digite o valor da dívida:");
            var valor = decimal.Parse(Console.ReadLine());

            var divida = new Divida
            {
                Valor = valor,
                Cliente = cliente
            };

            var sucesso = dividaService.Criar(divida);

            if (!sucesso)
            {
                Console.WriteLine("Erro ao cadastrar dívida");
            }
            else
            {
                Console.WriteLine("Dívida cadastrada");
            }
        }
    }

    else if (opcao == 8)
    {
        Console.WriteLine("Digite o CPF:");
        var cpf = Console.ReadLine();

        var dividas = dividaService.Listar(cpf);

        if (dividas.Count == 0)
        {
            Console.WriteLine("Nenhuma dívida encontrada");
        }

        foreach (var item in dividas)
        {
            Console.WriteLine("Código: {0}", item.Id);
            Console.WriteLine("Valor: R$ {0:F2}", item.Valor);
            Console.WriteLine("Data de criação: {0:dd/MM/yyyy}",
                item.DataCriacao);

            if (item.Paga == true)
            {
                Console.WriteLine("Situação: Paga");
                Console.WriteLine("Data de pagamento: {0:dd/MM/yyyy}",
                    item.DataPagamento);
            }
            else
            {
                Console.WriteLine("Situação: Em aberto");
            }

            Console.WriteLine("====================");
        }
    }

    else if (opcao == 9)
    {
        Console.WriteLine("Digite o código da dívida:");
        var id = int.Parse(Console.ReadLine());

        var sucesso = dividaService.Pagar(id);

        if (!sucesso)
        {
            Console.WriteLine("Não foi possível pagar a dívida");
        }
        else
        {
            Console.WriteLine("Dívida paga");
        }
    }

    else
    {
        Console.WriteLine("Opção inválida");
    }

    Console.WriteLine();
    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadKey();
}