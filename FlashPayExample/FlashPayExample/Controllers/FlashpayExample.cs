using Microsoft.AspNetCore.Mvc;
using FlashPay;
using FlashPay.Data;
using FlashPay.Enum;
using FlashPay.Help;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlashpayExample : ControllerBase
    {
        private readonly ILogger<FlashpayExample> _logger;

        private  FlashPayService flashpay = new FlashPayService("HT00000002", "U9kmXqjovFsBb1BEuEyPiO03uHGIE1Jr", "V4msmb1cSCBUSLWr", FlashPayService.StageURL);

        public FlashpayExample(ILogger<FlashpayExample> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/Order")]
        public ContentResult GetOrder()
        {
            return base.Content(flashpay.queryOrder("66666662"), "application/json");
        }

        [HttpGet]
        [Route("/MultiOrder")]
        public ContentResult GetMultiOrder()
        {
            return base.Content(flashpay.queryMultiOrder(DateTime.Parse("2022-06-08"), DateTime.Parse("2022-06-20")), "application/json");

        }

        [HttpPost]
        [Route("/CreateOrder")]
        public ContentResult CreateOrder()//Order order)
        {
             Order order = new Order();
             order.amt = 57.00;
             order.phone = "0928564055";
             order.mer_id = "HT00000002";
             order.client_url = "https://fl-pay.com";
             order.install_period = (int)FlashPay.Enum.PaymentMethodsItem.Credit12;
             order.order_desc = "123456";
             order.ord_no = "88888888";
             order.pay_type = (int)FlashPay.Enum.PaymentMethods.Credit;
             order.return_url = "https://fl-pay.com";
             order.ord_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
             var validationContext = new ValidationContext(order, null, null);
             var results = new List<ValidationResult>();
            return base.Content(flashpay.checkout(order), "text/html");
        }


        [HttpPost]
        [Route("/DoTrade/{order}/{price:double}")]
        public ContentResult DoTrade(string order, double price)
        {
            // return base.Content(flashpay.doTrade("66666662", 57), "application/json");
            return base.Content(flashpay.doTrade(order, price), "application/json");
        }


        [HttpGet]
        [Route("/FeedBack")]
        public string FeedBack()
        {
            string feedback = "ver=1.0.0&dat=0b7862b1417a075c7b9596d3c65232ea5918f7824ba3617d24368d6ff09bfcc523f9d2930b7c40ea110f968f61b5fce338ceeaeaafac31ea339370c79efd9827d10aae08a19c5b1c25da41af4b08b65e5c00f75330ef4f294ed171af41bca171ace69c6fc11832bed13f94e573ddc22890d89e70159e2c71ecf097d004701444fe36b0fe2a067a1b9ac635d9bb20bdb0b2df14e08c884cd0c051e95e8bfcca19ff0e29b430a3d9378c8ccc3337a71808458e4ceff9422aaa7ab3329a2ed14a38a690b96c782e4b9360a81b4f7310d526a9dda6bc7e7f442a138de64ded4179f9194381cddbd60bc1b39b50838761d7255871632e2da3dc0dce77712b07a05c92703cad3587efd081aed39d9661fefad03d94ab6b8f3fd2161712195ead332cc8016d104ef2075719ccd17dbcefabae7a3293b6b5b5a2b98bae1351ac2199b36308a64360ca1494e938eefd3bfb56d86f2dd54b12199bb26adc458bc976022556a932fb9dd62a6fddddb437555e3cf6aa55a3ce5e2601ebff151af36aa6983efbb3bc5be7fd606b30f31b83c6ed92f63716b266e883467e80616de92cfe32b285a2a5e23c446f62bc67f2eef2e1c1d2190e929b5e1a3be26a4640ad2732df8c7a9a116cca3ce19c071f9fa4987a27a48f474ff47244eea8cfd012a5a33f731832a9416ab9446355118d43b060b7428de1&chk=F10B427E7DF3174DEB43C3EDC876E56F129F3B5165F4EBA9C373D62F5A423B67";
            return flashpay.decodeFormatData(feedback);
        }

    }
}