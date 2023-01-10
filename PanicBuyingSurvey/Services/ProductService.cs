using PanicBuyingSurvey.DataLayer;
using RedLockNet.SERedis;

namespace PanicBuyingSurvey.Services;

public class ProductService : IProductService
{
    private static readonly object shoppingLock = new object();
    private readonly IProductDataLayer productData;
    private readonly RedLockFactory factory;

    public ProductService(IProductDataLayer productData, RedLockFactory factory)
    {
        this.productData = productData;
        this.factory = factory;
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
            var resource = "lock_key";//lock object
            var expiry = TimeSpan.FromSeconds(30);//lock object 失效時間
            var wait = TimeSpan.FromSeconds(10);//放棄重試時間
            var retry = TimeSpan.FromSeconds(1);//重試間隔時間
            using (var redLock = factory.CreateLock(resource, expiry))
            {
                // 確定取得 lock 所有權
                if (redLock.IsAcquired)
                {
                    // 執行需要獨佔資源的核心工作
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
            //lock (shoppingLock)
            //{
            //    if (quantity <= product.stock)
            //    {
            //        //// update stock
            //        product.stock -= quantity;
            //        productData.UpdateStock(product);
            //    }
            //    else
            //    {
            //        throw new Exception("second stock check fail");
            //    }
            //}
        }
        else
        {
            throw new Exception("fist stock check fail");
        }
    }

}