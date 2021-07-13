using CMS.Systems.StockManagement.Entities.StockRoot;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Systems.StockManagement.Interfaces
{
    public interface IAccesoryService
    {
        Task<IEnumerable<Accessory>> GetAllAccessoriesAsync();
    }
}
