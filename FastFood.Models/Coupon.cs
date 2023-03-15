using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public double Discount { get; set; }
        public double MinimumAmount { get; set; }
        public byte[] ?CouponPicture { get; set; }
        public bool IsActive { get; set; }
    }

    public enum CouponType { 
        Percent = 0,
        Currency = 1
    }

}
