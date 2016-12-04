
namespace Cactus.IService.CMS
{
    public interface ICommentService : IBaseService<Cactus.Model.CMS.Comment>
    {
        bool isVoteFavour(int id);
        bool isVoteOppose(int id);
    }
}
