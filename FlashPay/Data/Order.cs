using System.ComponentModel.DataAnnotations;

namespace FlashPay.Data
{
    public class Order
    {

		public string ver { get; set; } = FlashPayService.ver;

		public int tx_type { get; set; } = 101;

		[Required(ErrorMessage = "mer_id is required.")]
		public string mer_id { get; set; }
		[Required(ErrorMessage = "ord_no is required.")]
		public string ord_no { get; set; }

		[RegularExpression(@"^[0-9]*$", ErrorMessage = "install_period is not Int.")]
		[Required(ErrorMessage = "pay_type is required.")]
		public int  pay_type { get; set; }
		[Required(ErrorMessage = "amt is required.")]
		public double amt { get; set; }

		public string cur { get; set; } = "NTD";   //目前只限制台幣
		[Required(ErrorMessage = "order_desc is required.")]
		public string order_desc { get; set; }

		[RegularExpression(@"^[0-9]*$", ErrorMessage = "install_period is not Int.")]
		[Required(ErrorMessage = "install_period is required.")]
		public int install_period { get; set; }

		[RegularExpression(@"09\d{2}(\d{6}|-\d{3}-\d{3})", ErrorMessage = "phone format error.")]
		[Required(ErrorMessage = "phone is required.")]
		public string phone { get; set; }

		public string use_redeem { get; set; } = "0";

		[RegularExpression(@"^(?:http|https|ftp)://[a-zA-Z0-9\.\-]+(?:\:\d{1,5})?(?:[A-Za-z0-9\.\;\:\@\&\=\+\-\$\,\?/_]|%u[0-9A-Fa-f]{4}|%[0-9A-Fa-f]{2})*$", ErrorMessage = "return_url is not correct URL.")]
		[Required(ErrorMessage = "return_url is required.")]
		public string return_url { get; set; }

		[RegularExpression(@"^(?:http|https|ftp)://[a-zA-Z0-9\.\-]+(?:\:\d{1,5})?(?:[A-Za-z0-9\.\;\:\@\&\=\+\-\$\,\?/_]|%u[0-9A-Fa-f]{4}|%[0-9A-Fa-f]{2})*$", ErrorMessage = "client_url is not correct URL.")]
		[Required(ErrorMessage = "client_url is required.")]
		public string client_url { get; set; }

		[Required(ErrorMessage = "ord_time is required.")]
		[RegularExpression(@"\d{4,4}-\d{2,2}-\d{2,2} \d{2,2}:\d{2,2}:\d{2,2}", ErrorMessage = "ord_time format error.")]
		public string ord_time { get; set; }
		public string sto_id { get; set; } = string.Empty;


	}

	public class Orderitem
	{
		[Required(ErrorMessage = "name is required.")]
		public string name { get; set; }
		[Required(ErrorMessage = "price is required.")]
		public double price { get; set; }
		[Required(ErrorMessage = "unit is required.")]
		public string unit { get; set; }
		[Required(ErrorMessage = "quantity is required.")]
		public int quantity { get; set; }

		public string toStr()
		{
			return this.name + this.price.ToString() + " X " + this.unit + this.quantity.ToString();
		}

	}
}
