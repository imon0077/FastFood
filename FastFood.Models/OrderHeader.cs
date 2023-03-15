using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser ?ApplicationUser { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public DateTime TimeofPick { get; set; }
        public DateTime DateofPick { get; set; }
        public double SubTotal { get; set; }
        public double OrderTotal { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public string CouponDis { get; set; } = string.Empty;
        public string TransId { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
