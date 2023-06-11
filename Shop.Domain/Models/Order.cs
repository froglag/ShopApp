using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderRef { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set;}
        public string City { get; set; }
        public string PostCode { get; set; }

        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
