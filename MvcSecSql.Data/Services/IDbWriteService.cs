using System.Threading.Tasks;

namespace MvcSecSql.Data.Services
{
    public interface IDbWriteService
    {
        Task<bool> Add<T>(T item) where T : class;
        Task<bool> Delete<T>(T item) where T : class;
        Task<bool> Update<T>(T item) where T : class;
        Task<bool> Update<T>(T originalItem, T updatedItem) where T : class;
    }
}
