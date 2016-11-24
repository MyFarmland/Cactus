using Cactus.Common;
using Cactus.IService.Store;
using Dapper.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cactus.MSSQLService.Store
{
    public class StoreInfoService : IStoreInfoService
    {

        public bool Insert(Model.Store.StoreInfo entity)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                int i = conn.Execute("INSERT INTO store_info(StoreName,StoreLogoPath,StoreDes,StoreSwitch,CreateTime,AlterTime)" +
                    "VALUES(@StoreName,@StoreLogoPath,@StoreDes,@StoreSwitch,@CreateTime,@LastTime)", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertBatch(List<Model.Store.StoreInfo> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Store.StoreInfo entity)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                conn.Execute("UPDATE store_info SET StoreName=@StoreName,StoreLogoPath=@StoreLogoPath,StoreDes=@StoreDes,StoreSwitch=@StoreSwitch,CreateTime=@CreateTime,LastTime=@LastTime WHERE Store_Id =@Store_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                conn.Execute(string.Format("DELETE FROM store_info WHERE Store_Id in ({0})", ids));
            }
        }

        public List<Model.Store.StoreInfo> GetAll()
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                return conn.Query<Model.Store.StoreInfo>("select * from store_info").ToList();
            }
        }

        public List<Model.Store.StoreInfo> ToPagedList(int pageIndex, int pageSize, string keySelector, out int count)
        {
            /*
     * firstIndex:起始索引
     * pageSize:每页显示的数量
     * orderColumn:排序的字段名
     * sql:可以是简单的单表查询语句，也可以是复杂的多表联合查询语句
 */
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                string sql = "store_info";
                string sql01 = "select count(Store_Id) from store_info";
                count = conn.Query<int>(sql01).SingleOrDefault();
                
                string query = "select top " + pageSize + " o.* from (select row_number() over(order by " + keySelector + ") as rownumber,* from(" + sql + ") as oo) as o where rownumber>" + (pageIndex - 1) * pageSize;
                return conn.Query<Model.Store.StoreInfo>(query).ToList();
            }
        }

        public Model.Store.StoreInfo Find(int id)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                string query = "select a.* from store_info as a WHERE a.Store_Id = @id";
                return conn.Query<Model.Store.StoreInfo>(query, new { id = id }).SingleOrDefault();
            }
        }
        public bool setStoreSwitch(int storeId, bool isSwitch) {
            using (IDbConnection conn = SqlString.GetSqlConnection())
            {
                int i = conn.Execute("UPDATE store_info SET StoreSwitch=@StoreSwitch,LastTime=@LastTime WHERE Store_Id =@Store_Id",
                    new { StoreSwitch = isSwitch, Store_Id = storeId, AlterTime=DateTime.Now });
                if (i > 0) { return true; } else { return false; }
            }
        }
    }
}
