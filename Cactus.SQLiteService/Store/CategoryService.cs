using Cactus.IService.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.SQLiteService.Store
{
    public class CategoryService : ICategoryService
    {
        public List<Model.Store.Category> GetCategoryList(int storeId)
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.Category> GetCategoryList(int storeId, int pid)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Model.Store.Category entity)
        {
            throw new NotImplementedException();
        }

        public bool InsertBatch(List<Model.Store.Category> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Store.Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ids)
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Model.Store.Category> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            throw new NotImplementedException();
        }

        public Model.Store.Category Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
