
namespace Cactus.IService.CMS
{
    public interface IStaticPageService : IBaseService<Cactus.Model.CMS.StaticPage>
    {
        bool IsUsePageName(string pageName, int ignoreId);
        void UpdatePath(int id,string path);
    }
}
