using System.ComponentModel.DataAnnotations.Schema;
using CMS.Systems.StockManagement.Entities.BaseEntities;

namespace CMS.Systems.StockManagement.Entities.StockRoot
{
    public class VehicleStockImage : EntityBase
    {
        [ForeignKey("VehicleStock")]
        public int VehicleStockId { get; set; }
        public byte[] StockImage { get; set; }
        public string Name { get; set; }
    }
}
