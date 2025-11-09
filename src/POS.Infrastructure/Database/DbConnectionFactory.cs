using System.Data;
using Npgsql;
using POS.Application.Abstractions.Data;

namespace POS.Infrastructure.Database;
internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public IDbConnection GetOpenConnection()
    {
        NpgsqlConnection connection = dataSource.OpenConnection();

        return connection;
    }
}
