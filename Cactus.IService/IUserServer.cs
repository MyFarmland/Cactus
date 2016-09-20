
namespace Cactus.IService
{
    public interface IUserServer : IBaseService<Cactus.Model.Sys.User>
    {
        /// <summary>
        /// 验证登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        Cactus.Model.Sys.User CheckLogin(string userName, string pwd);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        bool AlterPwd(int id,string oldPwd,string newPwd);
        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="id"></param>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        bool AlterFace(int id,string picUrl);

    }
}
