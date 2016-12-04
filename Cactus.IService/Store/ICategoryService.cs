using System.Collections.Generic;

namespace Cactus.IService.Store
{
    public interface ICategoryService : IBaseService<Cactus.Model.Store.Category>
    {
        List<Cactus.Model.Store.Category> GetCategoryList(int storeId);
        List<Cactus.Model.Store.Category> GetCategoryList(int storeId,int pid);
    }
}
