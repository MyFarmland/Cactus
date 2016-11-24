using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.IService.CMS
{
    public interface ICommentService : IBaseService<Cactus.Model.CMS.Comment>
    {
        bool isVoteFavour(int id);
        bool isVoteOppose(int id);
    }
}
