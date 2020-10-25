using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
