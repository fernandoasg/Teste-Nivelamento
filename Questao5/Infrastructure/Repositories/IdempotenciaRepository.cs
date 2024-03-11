using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories
{
    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public IdempotenciaRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<int> AddAsync(Idempotencia entidade)
        {
            var sql = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)";
            using var connection = new SqliteConnection(databaseConfig.Name);

            return await connection.ExecuteAsync(sql, entidade);
        }

        public async Task<Idempotencia?> GetByIdAsync(string id)
        {
            var sql = "SELECT * FROM idempotencia WHERE chave_idempotencia = {0}";
            using var connection = new SqliteConnection(databaseConfig.Name);

            var result = await connection.QuerySingleOrDefaultAsync<Idempotencia>(string.Format(sql, id));

            return result ?? null;
        }
    }
}
