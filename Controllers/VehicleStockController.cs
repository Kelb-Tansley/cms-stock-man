using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Systems.StockManagement.Entities.StockRoot;
using CMS.Systems.StockManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CMS.Systems.StockManagement.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VehicleStockController : ControllerBase
    {
        private readonly ILogger<VehicleStockController> _logger;
        private readonly IVehicleStockService _vehicleStockService;

        public VehicleStockController(ILogger<VehicleStockController> logger, IVehicleStockService vehicleStockService)
        {
            _logger = logger;
            _vehicleStockService = vehicleStockService;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleStock>> Get()
        {
            return await _vehicleStockService.GetAllVehiclesAsync();
        }
        [HttpPost]
        public async Task<VehicleStock> Post(VehicleStock vehicleStock)
        {
            var storedStock = await _vehicleStockService.SaveVehicleStock(vehicleStock);
            return storedStock;
        }
    }
}
