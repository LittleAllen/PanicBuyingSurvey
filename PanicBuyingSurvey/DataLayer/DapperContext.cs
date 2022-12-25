using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;
using PanicBuyingSurvey.Entity;

namespace PanicBuyingSurvey.DataLayer;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection(DataBaseEnum dbType)
    {
        switch (dbType)
        {
            case DataBaseEnum.PostgreDB:
                return new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
            case DataBaseEnum.MSDB:
                return new SqlConnection(_configuration.GetConnectionString("MSSQL"));
        }

        throw new Exception("No db connection");
    }
}