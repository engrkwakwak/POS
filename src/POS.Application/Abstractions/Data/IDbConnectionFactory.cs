using System.Data;

namespace POS.Application.Abstractions.Data;
public interface IDbConnectionFactory
{
    IDbConnection GetOpenConnection();
}
