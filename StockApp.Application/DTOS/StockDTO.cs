using StockApp.Application.DTOS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOS
{
    public class StockDTO : EntityDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
