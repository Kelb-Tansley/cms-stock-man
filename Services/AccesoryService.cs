using CMS.Systems.StockManagement.Entities.StockRoot;
using CMS.Systems.StockManagement.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Systems.StockManagement.Services
{
    public class AccesoryService : IAccesoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccesoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Interface Methods
        public async Task<IEnumerable<Accessory>> GetAllAccessoriesAsync()
        {
            var accessories = await _unitOfWork.AccessoryRepository.GetAsync(vs => !vs.IsDeleted);

            return accessories;
        }
        #endregion

    }
}
