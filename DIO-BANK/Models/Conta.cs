using DIO_BANK.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace DIO_BANK.Models
{
    public class Conta
    {
        [Key]       
        public  int Id { get; set; }
        public TipoConta TipoConta { get; set; }
        public double Saldo { get; set; }
        public double Credito { get; set; }
        public string Nome { get; set; } = null!;

        public Conta( TipoConta tipoConta,double saldo, double credito, string nome)
        {
            this.TipoConta = tipoConta;
            this.Saldo = saldo;
            this.Credito = credito;
            this.Nome = nome;
        }
        
    }
}
 