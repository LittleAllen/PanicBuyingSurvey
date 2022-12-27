using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PanicBuyingSurvey.Models;
using PanicBuyingSurvey.Services;

namespace PanicBuyingSurvey.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanicBuyingController : ControllerBase
    {
        private IProductService productService;
        private readonly ILogger<PanicBuyingController> logger;
        public PanicBuyingController(IProductService productService, ILogger<PanicBuyingController> logger)
        {
            this.productService = productService;
            this.logger = logger;
        }

        [HttpPost(Name = "Shopping")]
        public void Shopping(Order order)
        {
            try{
                logger.LogInformation(JsonSerializer.Serialize(order));
                productService.Shopping(order.PId, order.Stock);
            }
            catch(Exception ex) {
                logger.LogError($"{nameof(Shopping)}",ex);
                throw;
            }
        }
    }
}
