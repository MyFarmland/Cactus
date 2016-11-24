using Cactus.IService.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.MSSQLService.Store
{
    public class StaffService : IStaffService
    {
        public bool Insert(Model.Store.Staff entity)
        {
            throw new NotImplementedException();
        }

        public bool InsertBatch(List<Model.Store.Staff> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Store.Staff entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ids)
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.Staff> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.Staff> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            throw new NotImplementedException();
        }

        public Model.Store.Staff Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
