using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.Model.CMS
{
    public class TagMap
    {
        public int Tag_Id { get; set; }

        public Tag Tag { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
