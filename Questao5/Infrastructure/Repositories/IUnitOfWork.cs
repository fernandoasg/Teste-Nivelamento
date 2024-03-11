namespace Questao5.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        IContaCorrenteRepository ContaCorrenteRepository { get; } 
        IIdempotenciaRepository IdempotenciaRepository { get; }
        IMovimentoRepository MovimentoRepository { get; }
    }
}
