using System.ComponentModel.DataAnnotations;

namespace Vendinha_TrabalhoFinal.Models
{
    public class Divida
    {
        public int Id { get; set; }

        [Range(0.01, 999999, ErrorMessage = "O valor da dívida deve ser maior que zero")]
        public decimal Valor { get; set; }

        public bool Paga { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório")]
        public Cliente Cliente { get; set; }
    }
}