using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API.Helpers
{
    // To make it work with any type of object or class, give generic type `<T>`
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            PageSize = pageSize;
            TotalCount = count;
            //  To return list of our items
            AddRange(items);
        }

        // Purpose of this method is that we pass a query. We haven't done anything yet to our database,
        // Untill we run methods like `CountAsync()` or `ToListAsync()`
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();  //  Count number of items
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();    // Get items for a page
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
