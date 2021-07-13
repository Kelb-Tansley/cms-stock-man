using System.ComponentModel.DataAnnotations.Schema;
using CMS.Systems.StockManagement.Entities.BaseEntities;

namespace CMS.Systems.StockManagement.Entities.StockRoot
{
    public class VehicleStockImage : EntityBase
    {
        [ForeignKey("VehicleStock")]
        public int VehicleStockId { get; set; }
        public string StockImage { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Accepts an input object who's values are passed to the clone
        /// </summary>
        /// <param name="input"></param>
        /// <param name="clone"></param>
        public static void CloneWithoutId(VehicleStockImage clone, VehicleStockImage input)
        {
            clone.IsDeleted = input.IsDeleted;
            clone.IsPrimary = input.IsPrimary;
            clone.StockImage = input.StockImage;
            clone.Name = input.Name;
            clone.VehicleStockId = input.VehicleStockId;
        }
    }
}
