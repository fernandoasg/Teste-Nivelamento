using System.Text.Json;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Infrastructure.Services
{
    public class IdempotenciaService : IIdempotenciaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IdempotenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaGeral?> ConsultarResposta(string chave)
        {
            RespostaGeral? resposta = null;

            Idempotencia? idempotencia  = await _unitOfWork.IdempotenciaRepository.GetByIdAsync(chave);

            if(idempotencia != null)
            {
                resposta = JsonSerializer.Deserialize<RespostaGeral>(idempotencia.Resultado);
            }

            return resposta;
        }

        public async Task<int> SalvarIdempotencia(string chave, string requisicao, string resultado)
        {
            Idempotencia idempotencia = new Idempotencia();
            idempotencia.ChaveIdempotencia = chave;
            idempotencia.Requisicao = requisicao;
            idempotencia.Resultado = resultado;

            return await _unitOfWork.IdempotenciaRepository.AddAsync(idempotencia);
        }
    }
}
