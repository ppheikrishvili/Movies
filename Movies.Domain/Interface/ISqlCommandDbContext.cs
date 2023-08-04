using System.Data;

namespace Movies.Domain.Interface;

public interface ISqlCommandDbContext
{
    Task<DataTable> SqlCommandExecuteReader(string sqlCommandText, CancellationToken cancellationToken = default);
    Task<int> SqlCommandExecuteCommand(string sqlCommandText, CancellationToken cancellationToken = default);
}