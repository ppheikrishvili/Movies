﻿using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Interface;

namespace Movies.Persistence.Data.Repository.SqlExecute;

public class SqlCommandDbContext(AppDbContext context) : ISqlCommandDbContext
{
    public AppDbContext AppContext { get; set; } = context;

    public async Task<DataTable> SqlCommandExecuteReader(string sqlCommandText,
        CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = AppContext.Database.GetDbConnection();

        if (connection.State.Equals(ConnectionState.Closed))
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using DbCommand command = connection.CreateCommand();
        command.CommandTimeout = 120;
        command.CommandText = sqlCommandText;
        await using DbDataReader reader =
            await command.ExecuteReaderAsync(CommandBehavior.CloseConnection, cancellationToken);
        DataTable table = new();
        // ReSharper disable once AccessToDisposedClosure
        await Task.WhenAll(Task.Run(() => { table.Load(reader); }, cancellationToken)).ConfigureAwait(false);
        return table;
    }

    public async Task<int> SqlCommandExecuteCommand(string sqlCommandText,
        CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = AppContext.Database.GetDbConnection();
        if (connection.State.Equals(ConnectionState.Closed)) await connection.OpenAsync(cancellationToken);
        await using DbCommand command = connection.CreateCommand();
        command.CommandTimeout = 120;
        command.CommandText = sqlCommandText;
        return await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }
}