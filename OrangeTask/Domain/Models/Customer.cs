using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Service { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime ContractExpiryDate { get; set; }
    }
}
