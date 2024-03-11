using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public Task<int> AddAsync(ContaCorrente entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ContaCorrente?> GetByIdAsync(string id)
        {
            var sql = "SELECT * FROM contacorrente WHERE idcontacorrente = '{0}'";
            using var connection = new SqliteConnection(databaseConfig.Name);

            var result = await connection.QuerySingleOrDefaultAsync<ContaCorrente>(string.Format(sql, id));

            return result ?? null;
        }
    }
}
