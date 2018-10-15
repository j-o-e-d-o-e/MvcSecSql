using System.Threading.Tasks;
using MvcSecSql.Data.Data;

namespace MvcSecSql.Data.Services
{
    public class DbWriteService : IDbWriteService
    {
        private readonly VodContext _db;

        public DbWriteService(VodContext db)
        {
            _db = db;
        }

        public async Task<bool> Add<T>(T item) where T : class
        {
            try
            {
                await _db.AddAsync<T>(item);
                return await _db.SaveChangesAsync() >= 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete<T>(T item) where T : class
        {
            try
            {
                _db.Set<T>().Remove(item);
                return await _db.SaveChangesAsync() >= 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update<T>(T item) where T : class
        {
            try
            {
                _db.Set<T>().Update(item);
                return await _db.SaveChangesAsync() >= 0;
            }
            catch
            {
                return false;
            }
        }

        //this method is used with the UserGenre entity where a combined primary key is used
        public async Task<bool> Update<T>(T originalItem, T updatedItem) where T : class
        {
            try
            {
                _db.Set<T>().Remove(originalItem);
                _db.Set<T>().Add(updatedItem);
                return await _db.SaveChangesAsync() >= 0;
            }
            catch
            {
                return false;
            }
        }
    }
}