using DIO_BANK.Models;

namespace DIO_BANK.Repositories.Contracts
{
    public interface IContaContract
    {
        public bool CriarConta(Conta conta);
        public bool AtualizarConta(Conta conta);
        public bool ApagarConta(int Id);
        public bool Depositar(int id, double valor);
        public bool Sacar(int id,double valor);
        public bool TransferirValor(int idContaFonte, double valor, int idContaDestino);
        public ICollection<Conta> GetContaList();
        public Conta GetConta(int id);
    }
}
