using Cactus.IService.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.SQLiteService.Store
{
    public class OrderDetailService : IOrderDetailService
    {
        public bool Insert(Model.Store.OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public bool InsertBatch(List<Model.Store.OrderDetail> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Store.OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ids)
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.OrderDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.OrderDetail> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            throw new NotImplementedException();
        }

        public Model.Store.OrderDetail Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
