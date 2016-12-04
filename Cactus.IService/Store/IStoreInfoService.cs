
namespace Cactus.IService.Store
{
    public interface IStoreInfoService : IBaseService<Cactus.Model.Store.StoreInfo>
    {
        bool setStoreSwitch(int storeId,bool isSwitch);
    }
}
