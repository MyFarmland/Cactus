using Cactus.IService.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.SQLiteService.Store
{
    public class CommodityService : ICommodityService
    {
        public bool Insert(Model.Store.Commodity entity)
        {
            throw new NotImplementedException();
        }

        public bool InsertBatch(List<Model.Store.Commodity> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Store.Commodity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ids)
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.Commodity> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.Commodity> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            throw new NotImplementedException();
        }

        public Model.Store.Commodity Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
