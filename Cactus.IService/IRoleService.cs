
namespace Cactus.IService
{
    public interface IRoleService : IBaseService<Cactus.Model.Sys.Role>
    {
        bool IsUseName(string rolename, int ignoreId);
    }
}
