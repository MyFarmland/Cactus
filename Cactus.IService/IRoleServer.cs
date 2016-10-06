
namespace Cactus.IService
{
    public interface IRoleServer : IBaseService<Cactus.Model.Sys.Role>
    {
        bool IsUseName(string rolename, int ignoreId);
    }
}
