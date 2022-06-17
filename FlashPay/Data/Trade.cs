using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashPay.Data
{
    public class Trade
    {
        public int  tx_type { get; set; } = 8;

        [Required(ErrorMessage = "mer_id is required.")]
        public string mer_id { get; set; }
        [Required(ErrorMessage = "ord_no is required.")]
        public string ord_no { get; set; }

        [Required(ErrorMessage = "amt is required.")]
        public double amt { get; set; }
    }
}
