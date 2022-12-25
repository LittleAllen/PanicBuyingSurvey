using System.Data;
using Dapper;
using PanicBuyingSurvey.Entity;

namespace PanicBuyingSurvey.DataLayer;

public class ProductDataLayer : IProductDataLayer
{
    private readonly DapperContext _context;
    private readonly DataBaseEnum _dbType;

    public ProductDataLayer(DapperContext context)
    {
        _context = context;
        _dbType = DataBaseEnum.PostgreDB;
    }

    public ProductEntity Get(int id)
    {
        var query = "SELECT * FROM products WHERE id = @id";
        using (var connection = _context.CreateConnection(_dbType))
        {
            return connection.QueryFirstOrDefault<ProductEntity>(query, new {id = id});
        }
    }

    public void UpdateStock(ProductEntity product)
    {
        using (var connection = _context.CreateConnection(_dbType))
        {
            connection.Execute("sp_UpdateStock"
                , new { idkey = product.id, stockvalue = product.stock }
                , commandType: CommandType.StoredProcedure);
        }
    }
}