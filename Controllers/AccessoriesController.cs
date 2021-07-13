using CMS.Systems.StockManagement.Entities.StockRoot;
using CMS.Systems.StockManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Systems.StockManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessoriesController : ControllerBase
    {
        private readonly ILogger<AccessoriesController> _logger;
        private readonly IAccesoryService _accesoryService;

        public AccessoriesController(ILogger<AccessoriesController> logger, IAccesoryService accesoryService)
        {
            _logger = logger;
            _accesoryService = accesoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<Accessory>> Get()
        {
            return await _accesoryService.GetAllAccessoriesAsync();
        }
    }
}
