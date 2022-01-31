﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;
using Pamaxie.Database.Extensions;
using Pamaxie.Database.Extensions.DataInteraction;
using StackExchange.Redis;

namespace Pamaxie.Database.Native;

/// <inheritdoc cref="IPamaxieDatabaseService"/>
internal sealed class PamaxieDatabaseService : IPamaxieDatabaseService
{
    private readonly PamaxieDatabaseDriver _owner;

    internal PamaxieDatabaseService(PamaxieDatabaseDriver owner)
    {
        this._owner = owner;
        
    }
    
    /// <inheritdoc cref="IPamaxieDatabaseService.IsDbConnected"/>
    public bool IsDbConnected { get; private set; }

    /// <inheritdoc cref="IPamaxieDatabaseService.DbConnectionHost1"/>
    public object DbConnectionHost1 { get; private set; }
        
    /// Not needed since we work over DbContext
    public object DbConnectionHost2 { get; }

    /// <inheritdoc cref="IPamaxieDatabaseService.Projects"/>
    public IPamProjectInteraction Projects { get; }

    /// <inheritdoc cref="IPamaxieDatabaseService.Users"/>
    public IPamUserInteraction Users { get; }
        
    /// <inheritdoc cref="IPamaxieDatabaseService.Orgs"/>
    public IPamOrgInteraction Orgs { get; }
        
    /// <inheritdoc cref="IPamaxieDatabaseService.Scans"/>
    public IPamScanInteraction Scans { get; }

    /// <inheritdoc cref="IPamaxieDatabaseService.ValidateConfiguration"/>
    public bool ValidateConfiguration(IPamaxieDatabaseConfiguration connectionParams)
    {
        if (connectionParams == null) throw new ArgumentNullException(nameof(connectionParams));
            
        using var conn = ConnectionMultiplexer.Connect(connectionParams.ToString());
        return conn.IsConnected;
    }

    /// <inheritdoc cref="IPamaxieDatabaseService.ConnectToDatabase"/>
    public void ConnectToDatabase(IPamaxieDatabaseConfiguration connectionParams = null)
    {
        if (connectionParams != null || string.IsNullOrWhiteSpace(_owner.Configuration.Db1Config))
        {
            throw new ArgumentException($"You need to either set/create {nameof(connectionParams)} or make sure that" +
                                        $"{nameof(_owner.Configuration.Db1Config)} is loaded and not null or whitespace", nameof(connectionParams));
        }
            
        if (!string.IsNullOrWhiteSpace(_owner.Configuration.Db1Config))
        {
            connectionParams = _owner.Configuration;
        }

        if (connectionParams == null)
        {
            throw new ArgumentNullException(nameof(connectionParams), "Suffered a critical error while trying to use the Database configuration");
        }
        
        CleanRedisConnection();
        DbConnectionHost1 = ConnectionMultiplexer.Connect(connectionParams.Db1Config);
        PgSqlContext.SqlConnectionString = connectionParams.Db2Config;

        using (var dbContext = new PgSqlContext())
        {

        }
        
        //Stating our driver is in a functional state again.
        IsDbConnected = true;
    }

    public async Task ConnectToDatabaseAsync(IPamaxieDatabaseConfiguration connectionParams = null)
    { 
        if (connectionParams != null || string.IsNullOrWhiteSpace(_owner.Configuration.Db1Config))
        {
            throw new ArgumentException($"You need to either set/create {nameof(connectionParams)} or make sure that" +
                                        $"{nameof(_owner.Configuration.Db1Config)} is loaded and not null or whitespace", nameof(connectionParams));
        }
            
        if (!string.IsNullOrWhiteSpace(_owner.Configuration.Db1Config))
        {
            connectionParams = _owner.Configuration;
        }

        if (connectionParams == null)
        {
            throw new ArgumentNullException(nameof(connectionParams), "Suffered a critical error while trying to use the Database configuration");
        }
        
        CleanRedisConnection();
        DbConnectionHost1 = await ConnectionMultiplexer.ConnectAsync(connectionParams.Db1Config);
        PgSqlContext.SqlConnectionString = connectionParams.Db2Config;
        


        //Stating our driver is in a functional state again.
        IsDbConnected = true;
    }
    
    /// <inheritdoc cref="IPamaxieDatabaseService.ValidateDatabase"/>
    public void ValidateDatabase()
    { 
        using var dbContext = new PgSqlContext();
        
        Debug.WriteLine("Ensuring Database is postgres");
        
        if (!dbContext.Database.IsNpgsql())
        {
            throw new InvalidOperationException("The underlying database is not postgres");
        }

        var pendingMigrations = dbContext.Database.GetPendingMigrations();
        
        if (pendingMigrations.Any())
        {
            Debug.Write("Migrations pending... applying them now! " +
                        "Also automatically creating your database if it doesn't exist yet");
            dbContext.Database.Migrate();
        }
        
        if (!dbContext.Database.CanConnect())
        {
            throw new InvalidOperationException("A connection to our database could not be established. " +
                                                "Please validate the connection string is correct.");
        }
    }
    
    /// <inheritdoc cref="IPamaxieDatabaseService.ValidateDatabaseAsync"/>
    public async Task ValidateDatabaseAsync()
    {
        await using var dbContext = new PgSqlContext();
        
        Debug.WriteLine("Ensuring Database is postgres");
        
        if (!dbContext.Database.IsNpgsql())
        {
            throw new InvalidOperationException("The underlying database is not postgres");
        }

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        
        if (pendingMigrations.Any())
        {
            Debug.Write("Migrations pending... applying them now! " +
                        "Also automatically creating your database if it doesn't exist yet");
            await dbContext.Database.MigrateAsync();
        }
        
        if (!await dbContext.Database.CanConnectAsync())
        {
            throw new InvalidOperationException("A connection to our database could not be established. " +
                                                "Please validate the connection string is correct.");
        }
    }

    /// <summary>
    /// Cleans up connections to the database
    /// </summary>
    private void CleanRedisConnection()
    {
        //Setting this to false in case commands fly in while we clean everything up
        IsDbConnected = false;

        if (DbConnectionHost1 is not ConnectionMultiplexer redisDbConnection)
        {
            if (DbConnectionHost1 is IDisposable disposable)
            {
                disposable.Dispose();
            }
                
            DbConnectionHost1 = null;
        }
    }
}