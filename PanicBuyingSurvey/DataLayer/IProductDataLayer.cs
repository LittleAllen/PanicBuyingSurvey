using PanicBuyingSurvey.Entity;

namespace PanicBuyingSurvey.DataLayer;

public interface IProductDataLayer
{
    ProductEntity Get(int id);
    void UpdateStock(ProductEntity product);
}