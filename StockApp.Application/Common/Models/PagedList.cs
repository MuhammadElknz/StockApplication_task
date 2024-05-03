using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Common.Models
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public PaginationHeader pagination { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            pagination = new PaginationHeader(CurrentPage, PageSize, TotalCount, TotalPages);

            AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();

            var items = new List<T>();

            if (pageNumber == -1)
            {
                items = await source.ToListAsync();

            }
            else
            {
                items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            }



            return new PagedList<T>(items, count, pageNumber, pageSize);
        }


    }
}
