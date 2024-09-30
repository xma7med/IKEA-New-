using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Preisitance.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Preisitance.Repositories._Generic
{
    public class GenaricRepository<T> : IGenericRepository<T>  where T : ModelBase 
    {
        private protected readonly ApplictaionDbContext _dbContext;

        public GenaricRepository(ApplictaionDbContext dbContext) // Ask ClrS
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true)
        {

            if (withAsNoTracking)
                return await _dbContext.Set<T>().Where (X=>!X.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync() ;
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
            //return _dbContext.Find<T>(id);

            ///     var T = _dbContext.Ts.Local.    (D => D.Id == id);
            /// if (T == null)
            ///     _dbContext.Ts.FirstOrDefault(D => D.Id == id);
            /// return T;

        }
        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }
        //public IEnumerable<T> GetIEnumerable()
        //{
        //    throw new NotImplementedException();
        //}



        public void Add(T entity)
        {
            // 4 ways to add 
            _dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)=> _dbContext.Set<T>().Update(entity);
        
        public void Delete(T entity)
        {
            // Soft Delete 
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            // Hard deleted 
            /// _dbContext.Set<T>().Remove(entity);
            /// return _dbContext.SaveChanges();
        }

        
    }
    
}
