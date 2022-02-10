using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using IdGen;
using Microsoft.EntityFrameworkCore;
using Pamaxie.Data;

namespace Pamaxie.Database.Native.Sql;

/// <summary>
/// Stores known Ips users connected from
/// </summary>
public class KnownUserIp : IPamSqlObject
{
    private static IdGenerator KnownIpsIdsGenerator = new IdGenerator(4);
    
    public KnownUserIp()
    {
        Id = (ulong)KnownIpsIdsGenerator.CreateId();
    }
    
    /// <inheritdoc cref="IPamSqlObject.Id"/>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    
    /// <summary>
    /// User who logged in with this IP previously
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// IP address of new login
    /// </summary>
    public string IpAddress { get; set; }
    
    /// <inheritdoc cref="IPamSqlObject.TTL"/>
    public DateTime? TTL { get; set; }
}