using System;
using CMS.Systems.StockManagement.Data.Repository;
using CMS.Systems.StockManagement.Entities.StockRoot;
using CMS.Systems.StockManagement.Interfaces;

namespace CMS.Systems.StockManagement.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;

        private GenericRepository<VehicleStock> _vehicleStockRepository;
        private GenericRepository<Accessory> _accessoryRepository;
        private GenericRepository<VehicleStockAccessory> _vehicleStockAccessoryRepository;
        private GenericRepository<VehicleStockImage> _imagesRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<VehicleStock> VehicleStockRepository
        {
            get
            {
                return _vehicleStockRepository ??= new GenericRepository<VehicleStock>(_context);
            }
        }
        public IGenericRepository<VehicleStockAccessory> VehicleStockAccessoryRepository
        {
            get
            {
                return _vehicleStockAccessoryRepository ??= new GenericRepository<VehicleStockAccessory>(_context);
            }
        }
        public IGenericRepository<Accessory> AccessoryRepository
        {
            get
            {
                return _accessoryRepository ??= new GenericRepository<Accessory>(_context);
            }
        }
        public IGenericRepository<VehicleStockImage> ImagesRepository
        {
            get
            {
                return _imagesRepository ??= new GenericRepository<VehicleStockImage>(_context);
            }
        }
               

        public void Save()
        {
            _context.SaveChanges();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
