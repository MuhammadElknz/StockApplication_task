using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTimeOffset CreatedAt { get; set; } = DateTime.Now;

        public string? CreatedBy { get; set; }

        public DateTimeOffset LastModifiedAt { get; set; } = DateTime.Now;

        public string? LastModifiedBy { get; set; }
    }
}
