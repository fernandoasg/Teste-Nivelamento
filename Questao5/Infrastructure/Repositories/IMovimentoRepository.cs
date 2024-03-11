using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories
{
    public interface IMovimentoRepository : IGenericRepository<Movimento>
    {
        Task<IReadOnlyList<Movimento>> GetAllAsync(string idConta);
    }
}
