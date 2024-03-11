using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Responses;

namespace Questao5.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult GerarResposta(RespostaGeral resultado)
        {
            if(string.IsNullOrEmpty(resultado.MensagemValidacao))
            {
                return Ok(resultado.ResultadoAcao);
            }
            else
            {
				return BadRequest(new RespostaDadosInvalidos(resultado.MensagemValidacao, resultado.TipoValidacao));
            }
        }
    }
}
