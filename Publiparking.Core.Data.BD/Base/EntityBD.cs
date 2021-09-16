using Microsoft.EntityFrameworkCore;
using Publiparking.Core.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD.Base
{
    public class EntityBD<TEntity>
    where TEntity : class
    {

        public static IQueryable<TEntity> GetList(DbParkContext p_dbContext)
        {
            return GetListInternal(p_dbContext);
        }

        public static TEntity GetById(DbParkContext p_dbContext, int p_id)
        {
            return GetEntityById(p_dbContext, p_id);

        }


        public static IQueryable<TEntity> GetListInternal(DbParkContext p_dbContext)
        {
            return p_dbContext.Set<TEntity>().AsNoTracking();
        }
        public static async Task<IList<TEntity>> GetListInternalAsync(DbParkContext p_dbContext)
        {
            return await p_dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public static async Task<TEntity> GetEntityByIdAsync(DbParkContext p_dbContext, int id)
        {

            var entity = await p_dbContext.Set<TEntity>().FindAsync(id);
            p_dbContext.Entry(entity).State = EntityState.Detached;
            return entity;

        }
        public static TEntity GetEntityById(DbParkContext p_dbContext, int id)
        {

            var entity = p_dbContext.Set<TEntity>().Find(id);
            p_dbContext.Entry(entity).State = EntityState.Detached;
            return entity;

        }
        public static async Task CreateAsync(DbParkContext p_dbContext, TEntity entity)
        {
            await p_dbContext.Set<TEntity>().AddAsync(entity);
            await p_dbContext.SaveChangesAsync();
        }
        public static bool Create(DbParkContext p_dbContext, TEntity entity)
        {
            p_dbContext.Set<TEntity>().Add(entity);
            p_dbContext.SaveChanges();
            return true;
        }
        public static async Task UpdateAsync(DbParkContext p_dbContext, int id, TEntity entity)
        {
            p_dbContext.Set<TEntity>().Update(entity);
            await p_dbContext.SaveChangesAsync();
        }
        public static bool Update(DbParkContext p_dbContext, int id, TEntity entity)
        {
            p_dbContext.Set<TEntity>().Update(entity);
            p_dbContext.SaveChanges();
            return true;
        }

        public static async Task DeleteAsync(DbParkContext p_dbContext, int id)
        {
            var entity = await p_dbContext.Set<TEntity>().FindAsync(id);
            p_dbContext.Set<TEntity>().Remove(entity);
            await p_dbContext.SaveChangesAsync();
        }
        public static bool Delete(DbParkContext p_dbContext, int id)
        {
            var entity = p_dbContext.Set<TEntity>().Find(id);
            p_dbContext.Set<TEntity>().Remove(entity);
            p_dbContext.SaveChanges();
            return true;
        }
    }
}
