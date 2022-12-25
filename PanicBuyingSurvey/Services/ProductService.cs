using PanicBuyingSurvey.DataLayer;

namespace PanicBuyingSurvey.Services;

public class ProductService : IProductService
{
    private IProductDataLayer productData { get; set; }

    public ProductService(IProductDataLayer productData)
    {
        this.productData = productData;
    }

    public void Shopping(int id, int quantity)
    {
        //// get product
        var product = productData.Get(id);

        //// check product & stock
        if (product == null)
        {
            throw new Exception("product not exist");
        }

        if (quantity > product.stock)
        {
            throw new Exception("over stock");
        }

        //// update stock
        product.stock -= quantity;
        productData.UpdateStock(product);
    }

}