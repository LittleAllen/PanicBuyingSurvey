using PanicBuyingSurvey.DataLayer;

namespace PanicBuyingSurvey.Services;

public class ProductService : IProductService
{
    private static readonly object shoppingLock = new object();
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

        if (quantity <= product.stock)
        {
            lock (shoppingLock)
            {
                if (quantity <= product.stock)
                {
                    //// update stock
                    product.stock -= quantity;
                    productData.UpdateStock(product);
                }
                else
                {
                    throw new Exception("second stock check fail");
                }
            }
        }
        else
        {
            throw new Exception("fist stock check fail");
        }
    }

}