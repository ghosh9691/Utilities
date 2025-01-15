using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrabalGhosh.Utilities
{
    public static class PagedResultExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query,
            int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double) result.RowCount / pageSize;
            result.PageCount = (int) Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query,
            int page, int pageSize) where T : class
        {
            return await Task.Run(() => GetPaged<T>(query, page, pageSize));
        }

        public static PagedResult<T> GetPaged<T>(this IEnumerable<T> query,
            int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double) result.RowCount / pageSize;
            result.PageCount = (int) Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
        
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IEnumerable<T> query,
            int page, int pageSize) where T : class
        {
            return await Task.Run(() => GetPaged<T>(query, page, pageSize));
        }
    }
}