using FlashPay.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;



namespace FlashPay
{
    public partial class FlashPayService 
    {

        private string trade = "/trade.php";
        private string querytrade = "/querytrade.php";


        public string checkOut(Order order)
        {
            checkModel(order);
            string jsonString = JsonSerializer.Serialize(order);
             var endata=encodeData(jsonString);
            StringBuilder builder = new StringBuilder();
            builder.Append("<!DOCTYPE html><html><head><meta charset=\"utf - 8\"></head><body>");
            builder.Append("<form id=\"FLASHForm\" action=\"" + this.ServiceURL + trade + "\" method=\"post\">");
            builder.Append("<input type=\"hidden\" name=\"ver\" value=\"" + endata.ver + "\">");
            builder.Append("<input type=\"hidden\" name=\"mid\" value=\"" + endata.mid + "\">");
            builder.Append("<input type=\"hidden\" name=\"dat\" value=\"" + endata.dat + "\">");
            builder.Append("<input type=\"hidden\" name=\"key\" value=\"" + endata.key + "\">");
            builder.Append("<input type=\"hidden\" name=\"chk\" value=\"" + endata.chk + "\">");
            builder.Append("<script language=\"JavaScript\">");
            builder.Append("FLASHForm.submit()");
            builder.Append("</script>");
            builder.Append("</form> </body> </html>");
            return builder.ToString();
        }
        public string queryOrder(string orderNo)
        {
            var queryOrder = new QueryOrder();
            queryOrder.ord_no = orderNo;
            queryOrder.mer_id = this.MerchantID;
            checkModel(queryOrder);
            string jsonString = JsonSerializer.Serialize(queryOrder);
            var endata = encodeData(jsonString);
            return decodeData(serverPost(JsonSerializer.Serialize(endata),this.ServiceURL+ querytrade));
        }

        public string queryMultiOrder(DateTime start, DateTime end)
        {
            TimeSpan Diff_dates = end.Subtract(start);
            if (Diff_dates.TotalDays > 30)
                throw new Exception("Date is greater than 30 days");
            var queryMultiOrder = new QueryMultiOrder();
            queryMultiOrder.mer_id = MerchantID;
            queryMultiOrder.end_date = end.ToString("yyyy-MM-dd");
            queryMultiOrder.start_date = start.ToString("yyyy-MM-dd");
            checkModel(queryMultiOrder);
            string jsonString = JsonSerializer.Serialize(queryMultiOrder);
            var endata = encodeData(jsonString);
            return decodeData(serverPost(JsonSerializer.Serialize(endata), this.ServiceURL + querytrade));
        }
        public string doTrade(string orderNO, double orderPrice)
        {
            var trade = new Trade();
            trade.mer_id =MerchantID;
            trade.amt = orderPrice;
            trade.ord_no = orderNO;
            checkModel(trade);
            string jsonString = JsonSerializer.Serialize(trade);
            var endata = encodeData(jsonString);
            return decodeData(serverPost(JsonSerializer.Serialize(endata), this.ServiceURL + querytrade));
        }

        public string checkoutFeedback(string response)
        {
            return  decodeData(response);
        }


    }
}
