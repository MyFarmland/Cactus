using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.IService.Store
{
    public interface ICategoryService : IBaseService<Cactus.Model.Store.Category>
    {
        List<Cactus.Model.Store.Category> GetCategoryList(int storeId);
        List<Cactus.Model.Store.Category> GetCategoryList(int storeId,int pid);
    }
}
