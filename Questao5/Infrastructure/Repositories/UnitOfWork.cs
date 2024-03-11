namespace Questao5.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IContaCorrenteRepository ContaCorrenteRepository { get; set; }
        public IIdempotenciaRepository IdempotenciaRepository { get; set; }
        public IMovimentoRepository MovimentoRepository {  get; set; }

        public UnitOfWork(IContaCorrenteRepository contaCorrenteRepository, IIdempotenciaRepository idempotenciaRepository, IMovimentoRepository _movimentoRepository)
        {
            this.ContaCorrenteRepository = contaCorrenteRepository;
            this.IdempotenciaRepository = idempotenciaRepository;
            this.MovimentoRepository = _movimentoRepository;
        }
    }
}
