using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public BadRequestException(string message) :base(message)
        {
            
        }

        public BadRequestException(IDictionary<string, string[]> errors) :base() 
        {
            Errors = errors;
        }


    }
}
