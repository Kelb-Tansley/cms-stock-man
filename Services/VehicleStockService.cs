using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Systems.StockManagement.Entities.StockRoot;
using CMS.Systems.StockManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS.Systems.StockManagement.Services
{
    public class VehicleStockService : IVehicleStockService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleStockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Interface Methods
        public async Task<IEnumerable<VehicleStock>> GetAllVehiclesAsync()
        {
            var vehicleStocks = await _unitOfWork.VehicleStockRepository.GetAsync(vs => !vs.IsDeleted);

            foreach (var vehicleStock in vehicleStocks)
            {
                var vehicleStockAccessories = await _unitOfWork.VehicleStockAccessoryRepository.GetAsync(v => !v.IsDeleted && v.VehicleStockId == vehicleStock.Id);

                vehicleStock.Accessories = new List<Accessory>();
                foreach (var vehicleStockAccessory in vehicleStockAccessories)
                    vehicleStock.Accessories.Add(await _unitOfWork.AccessoryRepository.FirstOrDefaultAsync(a => !a.IsDeleted && a.Id == vehicleStockAccessory.AccessoryId));
            }

            if (vehicleStocks == null || !vehicleStocks.Any())
                return VehicleStock.GetTestData();

            return vehicleStocks;
        }

        public async Task<IList<VehicleStock>> GetAllVehiclesByUserNameAsync(string userName)
        {
            return await _unitOfWork.VehicleStockRepository.GetAsync(vsr => !vsr.IsDeleted && EF.Functions.Like(userName, vsr.CreatedBy));
        }

        public async Task<VehicleStock> SaveVehicleStock(VehicleStock vehicleStock)
        {
            if (vehicleStock == null)
                return vehicleStock;

            try
            {
                //Save or update details of accessories in seperate save proceedure
                var accessories = await SaveAccessories(vehicleStock.Accessories);
                var accessoriesSavedSuccesful = !accessories.Any(a => a.Id == 0);

                //Save VehicleStock-to-Accessory many-to-many relationship
                if (accessoriesSavedSuccesful)
                {
                    var vehicleStockAccessories = InitializeVehicleStockAccessories(vehicleStock, accessories);

                    //Remove already saved accessories to optimise save proceedure
                    //vehicleStock.Accessories = null;

                    //Save vehicle stocks
                    var vehicleStockExists = vehicleStock.Id == 0 ? false : await _unitOfWork.VehicleStockRepository.FirstOrDefaultAsync(v => v.Id == vehicleStock.Id, true) != null;
                    if (!vehicleStockExists)
                    {
                        vehicleStock.Id = 0;
                        await _unitOfWork.VehicleStockRepository.AddAsync(vehicleStock);
                        _unitOfWork.Save();
                    }
                    else
                    {
                        _unitOfWork.VehicleStockRepository.Update(vehicleStock);
                        _unitOfWork.Save();
                    }

                    //After saving VehicleStock in many-to-many relationship Id will be present for vehicleStockAccessories save
                    if (vehicleStock.Id > 0)
                        await SaveVehicleStockAccessories(vehicleStock, vehicleStockAccessories);
                }

                return vehicleStock;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Private Methods

        private async Task<IEnumerable<Accessory>> SaveAccessories(IEnumerable<Accessory> accessories)
        {
            foreach (var accessory in accessories)
            {
                if (accessory.Id == 0)
                {
                    var matchingAccesory = await _unitOfWork.AccessoryRepository.FirstOrDefaultAsync(a => EF.Functions.Like(a.Name, accessory.Name));

                    //If there exists a match based on name field then update Id value
                    if (matchingAccesory != null)
                    {
                        //If the name matches but the description doesnt then update description value
                        if (matchingAccesory.Description != accessory.Description)
                        {
                            matchingAccesory.Description = accessory.Description;
                            _unitOfWork.AccessoryRepository.Update(matchingAccesory);
                            _unitOfWork.Save();
                        }

                        accessory.Id = matchingAccesory.Id;
                    }
                    else
                    {
                        await _unitOfWork.AccessoryRepository.AddAsync(accessory);
                        _unitOfWork.Save();
                    }
                }
                else
                {
                    _unitOfWork.AccessoryRepository.Update(accessory);
                    _unitOfWork.Save();
                }
            }

            return accessories;
        }

        private IEnumerable<VehicleStockAccessory> InitializeVehicleStockAccessories(VehicleStock vehicleStock, IEnumerable<Accessory> accessories)
        {
            var vehicleStockAccessories = new List<VehicleStockAccessory>();
            foreach (var accessory in accessories)
            {
                vehicleStockAccessories.Add(new VehicleStockAccessory() { VehicleStock = vehicleStock, Accessory = accessory });
            }
            return vehicleStockAccessories;
        }

        private async Task SaveVehicleStockAccessories(VehicleStock vehicleStock, IEnumerable<VehicleStockAccessory> vehicleStockAccessories)
        {
            foreach (var vehicleStockAccessory in vehicleStockAccessories)
            {
                var entryExists = await _unitOfWork.VehicleStockAccessoryRepository.FirstOrDefaultAsync(v => v.AccessoryId == vehicleStockAccessory.Accessory.Id && v.VehicleStockId == vehicleStockAccessory.VehicleStock.Id, true) != null;
                if (entryExists)
                    continue;

                if (vehicleStockAccessory.AccessoryId==0)
                    vehicleStockAccessory.AccessoryId = vehicleStockAccessory.Accessory.Id;

                if (vehicleStockAccessory.VehicleStockId == 0)
                    vehicleStockAccessory.VehicleStockId = vehicleStockAccessory.VehicleStock.Id;

                if (vehicleStockAccessory.VehicleStockId == 0 || vehicleStockAccessory.AccessoryId == 0)
                    continue;

                await _unitOfWork.VehicleStockAccessoryRepository.AddAsync(vehicleStockAccessory);
                _unitOfWork.Save();
            }
        }

        #endregion
    }
}
