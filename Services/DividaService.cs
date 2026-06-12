using System.ComponentModel.DataAnnotations;
using Vendinha_TrabalhoFinal.Data;
using Vendinha_TrabalhoFinal.Models;

namespace Vendinha_TrabalhoFinal.Services
{
    public class DividaService
    {
        public bool Validar(Divida a, out List<ValidationResult> erros)
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

        public bool Criar(Divida d)
        {
            if (!Validar(d, out var erros))
            {
                return false;
            }

            using var context = new VendinhaDbContext();

            var cliente = context.Clientes.FirstOrDefault(
                (item) => item.CPF == d.Cliente.CPF
            );

            if (cliente == null)
            {
                return false;
            }

            var dividaAberta = context.Dividas.Any(
                (item) => item.Cliente.CPF == cliente.CPF
                && item.Paga == false
            );

            if (dividaAberta)
            {
                Console.WriteLine("O cliente já possui uma dívida em aberto");
                return false;
            }

            d.Cliente = cliente;
            d.Paga = false;
            d.DataCriacao = DateTime.Now;
            d.DataPagamento = null;

            context.Dividas.Add(d);
            context.SaveChanges();

            return true;
        }

        public List<Divida> Listar(string cpf)
        {
            using var context = new VendinhaDbContext();

            var dividas = context.Dividas
                .Where(
                    (item) => item.Cliente.CPF == cpf
                )
                .ToList();

            return dividas;
        }

        public Divida Buscar(int id)
        {
            using var context = new VendinhaDbContext();

            var divida = context.Dividas.FirstOrDefault(
                (item) => item.Id == id
            );

            return divida;
        }

        public bool Pagar(int id)
        {
            using var context = new VendinhaDbContext();

            var divida = context.Dividas.FirstOrDefault(
                (item) => item.Id == id
            );

            if (divida == null || divida.Paga == true)
            {
                return false;
            }

            divida.Paga = true;
            divida.DataPagamento = DateTime.Now;

            context.SaveChanges();

            return true;
        }
    }
}