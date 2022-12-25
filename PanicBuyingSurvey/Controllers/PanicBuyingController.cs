using Microsoft.AspNetCore.Mvc;
using PanicBuyingSurvey.Services;

namespace PanicBuyingSurvey.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanicBuyingController : ControllerBase
    {
        private IProductService productService;

        public PanicBuyingController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost(Name = "Shopping")]
        public void Shopping(int id, int stock)
        {
            productService.Shopping(id, stock);
        }
    }
}
