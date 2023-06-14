using DIO_BANK.Context;
using DIO_BANK.Models;
using DIO_BANK.Repositories.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace DIO_BANK.Repositories
{
    public class ContaContract : IContaContract
    {
        private readonly BankContext _bankContext;
        public ContaContract(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
      
        public bool ApagarConta(int id)
        {
            var contaDb = GetConta(id);
            if(contaDb==null) return false;
            _bankContext.Contas.Remove(contaDb);
            var verifacacao=_bankContext.SaveChanges();
            return (verifacacao > 0);
        }     

        public bool AtualizarConta(Conta conta)
        {
            var contaDb = GetConta(conta.Id);
            if (contaDb == null) return false;
            contaDb.Nome = conta.Nome;
            contaDb.TipoConta = conta.TipoConta;
            _bankContext.Contas.Update(contaDb);
            var verificacao= _bankContext.SaveChanges();
            return (verificacao > 0);
        }      

        public bool CriarConta(Conta conta)
        {
            _bankContext.Contas.Add(conta);
            var verificacao=_bankContext.SaveChanges();
            return (verificacao>0);
        }
        public bool Depositar(int id, double valor)
        {
            var contaDb = GetConta(id);
            if(contaDb == null) return false;
            contaDb.Saldo += valor;
            _bankContext.Update(contaDb);
           var confirmacao= _bankContext.SaveChanges();
            return (confirmacao>0);            
        }

        public ICollection<Conta> GetContaList()
        {
            return _bankContext.Contas.ToList();
        }

        public bool Sacar(int id, double valor)
        {
            var contaDb = GetConta(id);
            if (contaDb == null) return false;
            if (contaDb.Saldo - valor < (contaDb.Credito*-1)) return false;
            contaDb.Saldo -= valor;
            _bankContext.Contas.Update(contaDb);
            var verificacao= _bankContext.SaveChanges();
          

            return (verificacao>0);
        }
        public bool TransferirValor(int contaFonte, double valor, int idContaDestino)
        {
            var contaFonteDb = GetConta(contaFonte);
            var contaDestinoDb = GetConta(idContaDestino);
            if (contaDestinoDb == null || contaFonteDb == null) return false;
            if (contaFonteDb.Saldo - valor < (contaFonteDb.Credito * -1)) return false;
            contaFonteDb.Saldo-= valor;
            _bankContext.Contas.Update(contaFonteDb);
            var verificacaoFonte= _bankContext.SaveChanges();
            if(verificacaoFonte==0) return false;
            contaDestinoDb.Saldo+= valor;
            _bankContext.Contas.Update(contaDestinoDb);
            var verificacao = _bankContext.SaveChanges();
            return (verificacao>0);

        }

        public Conta? GetConta(int id)
        {
          var conta = _bankContext.Contas.Find(id);
          return conta;
        }
    }
}
