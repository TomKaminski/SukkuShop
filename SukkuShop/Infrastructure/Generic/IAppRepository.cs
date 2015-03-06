using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using DapperExtensions;

namespace SukkuShop.Infrastructure.Generic
{
    public interface IAppRepository : IDisposable
    {
        SqlConnection Con { get; set; }
        IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> filter = null) where T : class;
        T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class;
    }

    public class AppRepository : IAppRepository
    {
        public AppRepository()
        {
            Con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }


        public void Dispose()
        {
            Con.Close();
        }

        public SqlConnection Con { get; set; }
        public IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> filter = null) where T : class
        {
            var query = Con.GetList<T>().AsQueryable();
            if (filter != null)
                query = query.Where(filter);
            return query.ToList();
        }

        public T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var query = Con.GetList<T>().AsQueryable();
            return predicate != null ? query.FirstOrDefault(predicate) : null;
        }
    }
}
