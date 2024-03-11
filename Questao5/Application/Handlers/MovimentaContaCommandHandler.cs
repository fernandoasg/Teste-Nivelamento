using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;
using Questao5.Infrastructure.Services;
using System.Text.Json;

namespace Questao5.Application.Handlers
{
    public class MovimentaContaCommandHandler : IRequestHandler<MovimentoContaCommand, RespostaGeral>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdempotenciaService _idempotenciaService;

        public MovimentaContaCommandHandler(IUnitOfWork unitOfWork, IIdempotenciaService idempotenciaService)
        {
            _unitOfWork = unitOfWork;
            _idempotenciaService = idempotenciaService;
        }

        public async Task<RespostaGeral> Handle(MovimentoContaCommand request, CancellationToken cancellationToken)
        {
            RespostaGeral resposta = new RespostaGeral();
            RespostaGeral? respostaSalva = await _idempotenciaService.ConsultarResposta(request.IdRequisicao);

            if (respostaSalva != null)
            {
                return respostaSalva;
            }

            var contaCorrente = await _unitOfWork.ContaCorrenteRepository.GetByIdAsync(request.IdContaCorrente);

            if (contaCorrente == null)
            {
                resposta.SetDadosInconsistentes("Apenas contas correntes cadastradas podem receber movimentação", "INVALID_ACCOUNT");
            }
            else if (!contaCorrente.Ativo)
            {
                resposta.SetDadosInconsistentes("Apenas contas correntes ativas podem receber movimentação", "INACTIVE_ACCOUNT");
            }
            else
            {
                var movimento = new Movimento { IdMovimento = Guid.NewGuid().ToString(), IdContaCorrente = request.IdContaCorrente, DataMovimento = DateTime.Now.ToString(), TipoMovimento = request.TipoMovimento, Valor = request.Valor };
            
                int status = await _unitOfWork.MovimentoRepository.AddAsync(movimento);

                if (status > 0)
                {
                    resposta.ResultadoAcao = movimento.IdMovimento;
                }
                else
                {
                    resposta.SetDadosInconsistentes("Ocorreu um erro durante a inserção dos dados", "ERRO_INTERNO");
                }
            }

            SalvarIdempotencia(request, resposta);

            return resposta;
        }

        private void SalvarIdempotencia(MovimentoContaCommand requisicao, RespostaGeral retorno)
        {
            string resultado = JsonSerializer.Serialize(retorno);

            _idempotenciaService.SalvarIdempotencia(requisicao.IdRequisicao, requisicao.ToString(), resultado);
        }
    }
}
