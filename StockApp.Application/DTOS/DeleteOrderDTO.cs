using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOS
{
    public class DeleteOrderDTO
    {
        public int Order_id { get; set; }
        public string UserId { get; set; }

    }
}
