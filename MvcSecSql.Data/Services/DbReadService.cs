using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcSecSql.Data.Data;

namespace MvcSecSql.Data.Services
{
    public class DbReadService : IDbReadService
    {
        private readonly VodContext _db;

        public DbReadService(VodContext db)
        {
            _db = db;
        }

        public IQueryable<T> Get<T>() where T : class
        {
            return _db.Set<T>();
        }

        // returns INSTANCE
        public T Get<T>(int id, bool includeRelatedEntities = false) where T : class
        {
            var res = _db.Set<T>().Find(id);

            // ReSharper disable once InvertIf
            if (res != null && includeRelatedEntities)
            {
                var (collections, classes) = GetEntityNames<T>();

                //Eager load tables referenced by the generic type T
                foreach (var entity in collections)
                    _db.Entry(res).Collection(entity).Load();

                foreach (var entity in classes)
                    _db.Entry(res).Reference(entity).Load();
            }

            return res;
        }

        // returns INSTANCE for tables with composite key (only for userGenres)
        public T Get<T>(string userId, int id) where T : class
        {
            return _db.Set<T>().Find(userId, id);
        }

        // returns COLLECTION of T including related entities
        public IEnumerable<T> GetWithIncludes<T>() where T : class
        {
            var dbset = _db.Set<T>();

            var (collections, classes) = GetEntityNames<T>();
            var entities = collections.Union(classes);
            foreach (var entity in entities)
                _db.Set<T>().Include(entity).Load();
            return dbset;
        }

        // returns drop down menu items
        public SelectList GetSelectList<T>(string valueField, string textField) where T : class
        {
            return new SelectList(Get<T>(), valueField, textField);
        }

        // returns count numbers for cards in administration index view
        public (int users, int userGenres, int genres, int bands, int bandmembers, int albums, int albumInfos, int videos)
            Count()
        {
            return (
                users: _db.Users.Count(),
                userGenres: _db.UserGenres.Count(),
                genres: _db.Genres.Count(),
                bands: _db.Bands.Count(),
                bandmembers: _db.BandMembers.Count(),
                albums: _db.Albums.Count(),
                albumInfos: _db.AlbumInfos.Count(),
                videos: _db.Videos.Count());
        }

        //helper method to return the names of all properties that correspond to DbSet properties
        private static (IEnumerable<string> collections, IEnumerable<string> classes) GetEntityNames<T>()
            where T : class
        {
            //fetch property names from VodContext whose name contains DbSet
            var dbsets = typeof(VodContext)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(table => table.PropertyType.Name.Contains("DbSet"))
                .Select(table => table.Name);

            //fetch property names from generic type T divided into collections (DbSet) and single classes
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var collections = properties.Where(property => dbsets.Contains(property.Name))
                .Select(property => property.Name);
            var classes = properties.Where(property => dbsets.Contains(property.Name + "s"))
                .Select(property => property.Name);

            return (collections: collections, classes: classes);
        }
    }
}