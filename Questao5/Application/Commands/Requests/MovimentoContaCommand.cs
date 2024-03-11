using MediatR;
using Questao5.Application.Commands.Responses;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentoContaCommand : IRequest<RespostaGeral>
    {
        [Required(ErrorMessage = "O Id da requisição é obrigatório")]
        public string IdRequisicao { get; set; }

        [Required(ErrorMessage = "O Id da conta corrente é obrigatório")]
        public string IdContaCorrente { get; set; }

        [Required(ErrorMessage = "O valor do movimento é obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "O Tipo de Movimento é obrigatório")]
        public char TipoMovimento { get; set; }

        public override string ToString()
        {
            return "IdRequisicao = " + IdRequisicao +
                    " IdContaCorrente = " + IdContaCorrente +
                    " Valor = " + Valor +
                    " TipoMovimento = " + TipoMovimento;
        }
    }
}
