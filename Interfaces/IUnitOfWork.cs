using CMS.Systems.StockManagement.Entities.StockRoot;

namespace CMS.Systems.StockManagement.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<VehicleStock> VehicleStockRepository { get; }
        IGenericRepository<VehicleStockAccessory> VehicleStockAccessoryRepository { get; }
        IGenericRepository<Accessory> AccessoryRepository { get; }
        void Dispose();
        void Save();
    }
}
