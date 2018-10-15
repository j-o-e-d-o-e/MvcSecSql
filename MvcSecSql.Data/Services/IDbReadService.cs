using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcSecSql.Data.Services
{
    public interface IDbReadService
    {
        IQueryable<T> Get<T>() where T : class;
        T Get<T>(int id, bool includeRelatedEntities = false) where T : class;
        T Get<T>(string userId, int id) where T : class;
        IEnumerable<T> GetWithIncludes<T>() where T : class;
        SelectList GetSelectList<T>(string valueField, string textField) where T : class;
        (int users, int userGenres, int genres, int bands, int albums, int albumInfos, int videos) Count();
    }
}