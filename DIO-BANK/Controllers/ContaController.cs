using DIO_BANK.Models;
using DIO_BANK.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DIO_BANK.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ContaController : ControllerBase
    {
        private readonly IContaContract _contaContract;
        public ContaController(IContaContract contaContract)
        {
            _contaContract = contaContract;
        }
        [HttpGet]
        public IActionResult ObterTodas()
        {
            var contas = _contaContract.GetContaList();
            return Ok(contas);
        }
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var conta = _contaContract.GetConta(id);
            if (conta == null) return NotFound();
            return Ok(conta);
        }

        [HttpPost]
        public IActionResult CriarConta([FromBody] Conta conta)
        {
            if (ModelState.IsValid)
            {
                var confirmacao = _contaContract.CriarConta(conta);
                if (confirmacao == false) return BadRequest(ModelState);
                return CreatedAtAction(nameof(ObterPorId), new { id = conta.Id }, conta);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("Atualizar")]

        public IActionResult Atualizar([FromBody] Conta conta)
        {

            if (ModelState.IsValid)
            {
                var confirmacao = _contaContract.AtualizarConta(conta);
                if (confirmacao == false) return NotFound();
                return Ok(confirmacao);
            }
            return NotFound(ModelState.Keys);

        }
        [HttpPut("Transferencia")]
        public IActionResult Transferencia([FromHeader] int idContaFonte, double valor, int idContaDestino)
        {
            var confirmacao = _contaContract.TransferirValor(idContaFonte, valor, idContaDestino);
            return confirmacao == false ? NotFound("Não foi possível realizar a transferência") : Ok("Transferência realizada com sucesso");
        }

        [HttpPut("Depositar")]
        public IActionResult Depositar(int id, double valor)
        {
            var confirmacao = _contaContract.Depositar(id, valor);
            return confirmacao == true ? Ok() : NotFound();
        }
        [HttpPut("Saque")]
        public IActionResult Saque(int id, double valor)
        {
            var confirmacao = _contaContract.Sacar(id, valor);
            return confirmacao == true ? Ok("Saque realizado com sucesso!") : NotFound("Não foi possivel realizar saque verifique o seu saldo em conta");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
           var confirmacao = _contaContract.ApagarConta(id);
            return confirmacao == true ? NoContent() : NotFound();
        }
    }
}
