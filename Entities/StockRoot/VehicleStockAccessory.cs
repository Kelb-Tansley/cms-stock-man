using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CMS.Systems.StockManagement.Entities.BaseEntities;

namespace CMS.Systems.StockManagement.Entities.StockRoot
{
    public class VehicleStockAccessory : ManyKeyBase
    {
        [Key, Column(Order = 0)]
        [ForeignKey("VehicleStock")]
        public int VehicleStockId { get; set; }
        [NotMapped]
        public VehicleStock VehicleStock { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Accessory")]
        public int AccessoryId { get; set; }
        [NotMapped]
        public Accessory Accessory { get; set; }

        internal static void SoftDeleteVehicleStockAccessories(List<VehicleStockAccessory> vehicleStockAccessories)
        {
            if (vehicleStockAccessories == null)
                return;

            vehicleStockAccessories.ForEach(v => v.IsDeleted = true);
        }
    }
}
