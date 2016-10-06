using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.IService.CMS
{
    public interface IStaticPageService : IBaseService<Cactus.Model.CMS.StaticPage>
    {
        bool IsUsePageName(string pageName, int ignoreId);
        void UpdatePath(int id,string path);
    }
}
