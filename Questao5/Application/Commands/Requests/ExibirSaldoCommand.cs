using MediatR;
using Questao5.Application.Commands.Responses;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Application.Commands.Requests
{
    public class ExibirSaldoCommand : IRequest<RespostaGeral>
    {
        [Required(ErrorMessage = "o Id da conta corrente é obrigatório")]
        public string IdContaCorrente { get; set; }
    }
}
