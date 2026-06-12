using System.ComponentModel.DataAnnotations;

namespace Vendinha_TrabalhoFinal.Models
{
    public class Cliente
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [StringLength(100, MinimumLength = 10)]
        [RegularExpression("^[A-Z][A-zA-z]+ [A-Z][A-zA-z ]+[^ ]$")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF do cliente é obrigatório")]
        [RegularExpression("^\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}$")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        [Range(16, 99)]
        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var anos = hoje.Year - DataNascimento.Year;
                var diaAnoNascimento = hoje.AddYears(-anos);

                if (DataNascimento > diaAnoNascimento)
                {
                    anos--;
                }

                return anos;
            }
        }

        public string email;

        [EmailAddress]
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email
        {
            get { return email; }
            set { email = value.ToLower(); }
        }

        public virtual void PrintDados()
        {
            Console.WriteLine("Nome: {0}", Nome);
            Console.WriteLine("CPF: {0}", CPF);
            Console.WriteLine("Data de Nascimento: {0:dd/MM/yyyy}", DataNascimento);
            Console.WriteLine("Idade: {0}", Idade);
            Console.WriteLine("Email: {0}", Email);
        }
    }
}