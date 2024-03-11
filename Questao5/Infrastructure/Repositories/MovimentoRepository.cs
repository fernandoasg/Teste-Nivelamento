using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<Movimento?> GetByIdAsync(string id)
        {
            var sql = "SELECT * FROM movimento WHERE idmovimento = {0}";
            using var connection = new SqliteConnection(databaseConfig.Name);

            var result = await connection.QuerySingleOrDefaultAsync<Movimento>(string.Format(sql, id));

            return result ?? null;
        }

        public async Task<int> AddAsync(Movimento entidade)
        {
            var sql = "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";
            using var connection = new SqliteConnection(databaseConfig.Name);

            return await connection.ExecuteAsync(sql, entidade);
        }

        public async Task<IReadOnlyList<Movimento>> GetAllAsync(string idConta)
        {
            var sql = "SELECT * FROM movimento WHERE idcontacorrente = '{0}'";
            using var connection = new SqliteConnection(databaseConfig.Name);

            var result = await connection.QueryAsync<Movimento>(string.Format(sql, idConta));

            return result.ToList();
        }
    }
}
