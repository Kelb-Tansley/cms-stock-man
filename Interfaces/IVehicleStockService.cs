using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Systems.StockManagement.Entities.StockRoot;

namespace CMS.Systems.StockManagement.Interfaces
{
    public interface IVehicleStockService
    {
        Task<IEnumerable<VehicleStock>> GetAllVehiclesAsync();
        Task<IList<VehicleStock>> GetAllVehiclesByUserNameAsync(string userName);
        Task<VehicleStock> SaveVehicleStock(VehicleStock vehicleStock);
    }
}
