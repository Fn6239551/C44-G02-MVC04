using IKEA.DAL.Presistance.Data.Contexts;
using IKEA.DAL.Presistance.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Classess
{
    public class GenericRepository<TEntity>(ApplicationDbContext dbContext)  : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Set<TEntity>().ToList();
            }
            return _dbContext.Set<TEntity>().AsNoTracking().ToList();

        }

        public TEntity? GetById(int id/*,ApplicationDbContext dbContext*/)
        => dbContext.Set<TEntity>().Find(id);
        //Add
        public int Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            return dbContext.SaveChanges();
        }
        //Delete
        public int Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            return dbContext.SaveChanges();
        }

        //Update
        public int Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            return dbContext.SaveChanges();
        }
    }
}
