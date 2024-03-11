using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers
{
    public class ExibirSaldoCommandHandler : IRequestHandler<ExibirSaldoCommand, RespostaGeral>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ExibirSaldoCommandHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaGeral> Handle(ExibirSaldoCommand request, CancellationToken cancellationToken)
        {
            RespostaGeral resposta = new RespostaGeral();
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
                var retorno = new RespostaSaldo(contaCorrente.Numero, contaCorrente.Nome);
                var listaMovimentos = await _unitOfWork.MovimentoRepository.GetAllAsync(request.IdContaCorrente);

                if (listaMovimentos != null)
                {
                    double somaCredito = 0;
                    double somaDebito = 0;

                    foreach(var item in listaMovimentos)
                    {
                        if (item.TipoMovimento.Equals('C'))
                        {
                            somaCredito += item.Valor;
                        }
                        else
                        {
                            somaDebito += item.Valor;
                        }
                    }

                    retorno.SaldoAtual = Math.Round(somaCredito - somaDebito, 2);
                }

                resposta.ResultadoAcao = retorno;
            }

            return resposta;
        }
    }
}
