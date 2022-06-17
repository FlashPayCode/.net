using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashPay.Data
{
    public class QueryOrder
    {


        [Required(ErrorMessage = "mer_id is required.")]
        public string mer_id { get; set; }

        [Required(ErrorMessage = "ord_no is required.")]
        public string ord_no { get; set; }

        public int tx_type { get; set; } = 107;


    }

    public class QueryMultiOrder
    {
        [Required(ErrorMessage = "mer_id is required.")]
        public string mer_id { get; set; }

        [RegularExpression(@"\d{4,4}-\d{2,2}-\d{2,2}", ErrorMessage = "start format error.")]
        [Required(ErrorMessage = "start is required.")]
        public string start_date { get; set; }

        [RegularExpression(@"\d{4,4}-\d{2,2}-\d{2,2}", ErrorMessage = "end format error.")]
        [Required(ErrorMessage = "end is required.")]
        public string end_date { get; set; }

        public int tx_type { get; set; } = 106;
    }



}
