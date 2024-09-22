﻿using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Preisitance.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Preisitance.Repositories._Generic
{
    public class GenaricRepository<T> where T : ModelBase
    {
        private protected readonly ApplictaionDbContext _dbContext;

        public GenaricRepository(ApplictaionDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll(bool withAsNoTracking = true)
        {

            if (withAsNoTracking)
                return _dbContext.Set<T>().Where (X=>!X.IsDeleted).AsNoTracking().ToList();

            return _dbContext.Set<T>().Where(X => !X.IsDeleted).ToList(); ;
        }
        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>();
        }
        public T? Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
            //return _dbContext.Find<T>(id);

            ///     var T = _dbContext.Ts.Local.FirstOrDefault(D => D.Id == id);
            /// if (T == null)
            ///     _dbContext.Ts.FirstOrDefault(D => D.Id == id);
            /// return T;

        }
        public int Add(T entity)
        {
            // 4 ways to add 
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            // Soft Delete 
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
            // Hard deleted 
            /// _dbContext.Set<T>().Remove(entity);
            /// return _dbContext.SaveChanges();
        }
    }
    
}
