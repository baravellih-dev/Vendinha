using System.ComponentModel.DataAnnotations;
using Vendinha_TrabalhoFinal.Data;
using Vendinha_TrabalhoFinal.Models;

namespace Vendinha_TrabalhoFinal.Services
{
    public class ClienteService
    {
        public bool Validar(Cliente a, out List<ValidationResult> erros)
        {
            var contexto = new ValidationContext(a);
            erros = new List<ValidationResult>();
            var objetoValido = Validator.
                    TryValidateObject(
                        a,
                        contexto,
                        erros,
                        true
                    );

            foreach (var erro in erros)
            {
                Console.WriteLine("{0}: {1}",
                    erro.MemberNames.First(),
                    erro.ErrorMessage);
            }

            return objetoValido;
        }

        public bool Criar(Cliente c)
        {
            if (!Validar(c, out var erros))
            {
                return false;
            }

            using var context = new VendinhaDbContext();

            var cpfExistente = context.Clientes.Any(
                (item) => item.CPF == c.CPF
            );

            if (cpfExistente)
            {
                Console.WriteLine("Já existe outro cliente com esse CPF");
                return false;
            }

            context.Clientes.Add(c);
            context.SaveChanges();

            return true;
        }

        public Cliente Buscar(string cpf)
        {
            using var context = new VendinhaDbContext();

            var cliente = context.Clientes.FirstOrDefault(
                (item) => item.CPF == cpf
            );

            return cliente;
        }

        public List<Cliente> Listar(int pageSize, int page)
        {
            using var context = new VendinhaDbContext();

            var take = pageSize;
            var skip = (page - 1) * pageSize;

            var clientes = context.Clientes.ToList();

            clientes = clientes
                .OrderByDescending(
                    (item) => TotalDividas(item.CPF)
                )
                .Skip(skip)
                .Take(take)
                .ToList();

            return clientes;
        }

        public List<Cliente> Pesquisa(string texto, int pageSize, int page)
        {
            using var context = new VendinhaDbContext();

            var take = pageSize;
            var skip = (page - 1) * pageSize;

            var clientes = context.Clientes
                .Where(
                    (item) => item.Nome.Contains(texto)
                )
                .ToList();

            clientes = clientes
                .OrderByDescending(
                    (item) => TotalDividas(item.CPF)
                )
                .Skip(skip)
                .Take(take)
                .ToList();

            return clientes;
        }

        public bool Alterar(Cliente c)
        {
            if (!Validar(c, out var erros))
            {
                return false;
            }

            using var context = new VendinhaDbContext();

            var cliente = context.Clientes.FirstOrDefault(
                (item) => item.CPF == c.CPF
            );

            if (cliente == null)
            {
                return false;
            }

            cliente.Nome = c.Nome;
            cliente.DataNascimento = c.DataNascimento;
            cliente.Email = c.Email;

            context.SaveChanges();

            return true;
        }

        public bool Excluir(string cpf)
        {
            using var context = new VendinhaDbContext();

            var cliente = context.Clientes.FirstOrDefault(
                (item) => item.CPF == cpf
            );

            if (cliente == null)
            {
                return false;
            }

            var dividas = context.Dividas
                .Where(
                    (item) => item.Cliente.CPF == cpf
                )
                .ToList();

            foreach (var item in dividas)
            {
                context.Dividas.Remove(item);
            }

            context.Clientes.Remove(cliente);
            context.SaveChanges();

            return true;
        }

        public decimal TotalDividas(string cpf)
        {
            using var context = new VendinhaDbContext();

            var dividas = context.Dividas
                .Where(
                    (item) => item.Cliente.CPF == cpf
                    && item.Paga == false
                )
                .ToList();

            decimal total = 0;

            foreach (var item in dividas)
            {
                total += item.Valor;
            }

            return total;
        }
    }
}