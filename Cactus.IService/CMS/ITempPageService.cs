
namespace Cactus.IService.CMS
{
    public interface ITempPageService : IBaseService<Cactus.Model.CMS.TempPage>
    {
        bool IsUseTempName(string tempName, int ignoreId);
    }
}
