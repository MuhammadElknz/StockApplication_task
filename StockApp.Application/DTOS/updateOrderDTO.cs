using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOS
{
    public class updateOrderDTO
    {
        [Required]
        public int? Quantity { get; set; }
        public int StockId { get; set; }
        public int Order_id { get; set; }
        public string UserId { get; set; }

    }

    
}
