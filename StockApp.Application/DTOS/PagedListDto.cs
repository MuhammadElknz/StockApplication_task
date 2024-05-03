using StockApp.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOS
{
    public class PagedListDto<T>
    {
        
    
        public PaginationHeader Pagination { get; set; }
        
        public List<T> Items { get; set; } = new List<T>();
    }
}
