using Cactus.IService.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.SQLiteService.Store
{
    public class CustomerService : ICustomerService
    {
        public bool Insert(Model.Store.Customer entity)
        {
            throw new NotImplementedException();
        }

        public bool InsertBatch(List<Model.Store.Customer> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Store.Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ids)
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.Customer> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            throw new NotImplementedException();
        }

        public Model.Store.Customer Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
