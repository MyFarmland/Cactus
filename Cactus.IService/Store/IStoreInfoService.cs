using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.IService.Store
{
    public interface IStoreInfoService : IBaseService<Cactus.Model.Store.StoreInfo>
    {
        bool setStoreSwitch(int storeId,bool isSwitch);
    }
}
