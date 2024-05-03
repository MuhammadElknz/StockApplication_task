using StockApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        private readonly List<Order> orders = new List<Order>();
        public IReadOnlyCollection<Order> Orders => orders.AsReadOnly();

        private Stock() { } 

        public Stock(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
     
        public void SetRandomPrice()
        {
            Price = GenerateRandomPrice();
        }

        private static decimal GenerateRandomPrice()
        {
            // Your logic for generating a random price
            Random random = new Random();
            return (decimal)(random.NextDouble() * 100);
        }
    }
}
