using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOS
{
    public class CreateOrderDTO
    {
        [Required]
        public int? Quantity { get; set; }
        public int StockId { get; set; }
        public string UserId { get; set; }

    }
}
