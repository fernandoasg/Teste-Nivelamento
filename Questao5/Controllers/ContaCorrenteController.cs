using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ContaCorrenteController : ApiBaseController
	{
		private readonly IMediator _mediator;
		private readonly ILogger<ContaCorrenteController> _logger;

		public ContaCorrenteController(IMediator mediator, ILogger<ContaCorrenteController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[HttpPost]
		[Route("movimentar-conta")]
		public async Task<IActionResult> Movimentar([FromQuery] MovimentoContaCommand request)
		{
			RespostaGeral resultado = new RespostaGeral();

			if (request.Valor <= 0)
			{
				resultado.SetDadosInconsistentes("Apenas valores positivos podem ser recebidos", "INVALID_VALUE");
			}
			else if (!request.TipoMovimento.Equals('C') && !request.TipoMovimento.Equals('D'))
			{
				resultado.SetDadosInconsistentes("Apenas os tipos 'débito' ou 'crédito' são aceitos", "INVALID_TYPE");
			}
			else
			{ 
				resultado = await _mediator.Send(request);
            }

            return GerarResposta(resultado);
        }

		[HttpGet]
		[Route("consultar-saldo")]
        public async Task<IActionResult> ConsultarSaldo([FromQuery] ExibirSaldoCommand request)
		{
			RespostaGeral resultado = await _mediator.Send(request);

			return GerarResposta(resultado);
		}
	}
}