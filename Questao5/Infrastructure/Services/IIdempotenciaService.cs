using Questao5.Application.Commands.Responses;

namespace Questao5.Infrastructure.Services
{
    public interface IIdempotenciaService
    {
        Task<RespostaGeral?> ConsultarResposta(string chave);
        Task<int> SalvarIdempotencia(string chave, string requisicao, string resultado);
    }
}
