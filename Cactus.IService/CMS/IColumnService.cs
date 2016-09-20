using System;
using System.Collections.Generic;

namespace Cactus.IService.CMS
{
    public interface IColumnService : IBaseService<Cactus.Model.CMS.Column>
	{
        /// <summary>
        /// 判断栏目名是否正在使用
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        bool IsUseColumnName(string columnName, int ignoreId);

        List<Model.CMS.Column> FindByPid(int pid);
	}
}

