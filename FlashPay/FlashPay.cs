using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using FlashPay.Help;

namespace FlashPay


{
    public class EnData
    {
        public string ver = FlashPayService.ver;

        public string mid { get; set; }
        public string dat { get; set; }
        public string key { get; set; }
        public string chk { get; set; }
    }


    public partial class FlashPayService
    {

        public static string ProductURL = "https://api.fl-pay.com";
        public static string StageURL = "https://demo.flashpayvpn.xyz";
        public static string ver = "1.0.0";


        public string MerchantID { get; set; }

        public string HashKey { get; set; }


        public string HashIv { get; set; }


        public string ServiceURL { get; set; }

        public FlashPayService(string MerchantID, string HashKey, string HashIv, string ServiceURL)
        {
            this.MerchantID = MerchantID;
            this.HashKey = HashKey;
            this.HashIv = HashIv;
            this.ServiceURL = ServiceURL;
        }



         private EnData encodeFormatData(String data)
        {
            string checkData = this.HashKey + data + this.HashIv;
            string checkKeys = SHA256Encoder.Encrypt(checkData);
            string tradeData = AES256.Encrypt(this.HashKey, this.HashIv, data);
            string checkInfo = SHA256Encoder.Encrypt(tradeData).ToUpper();
            var enData = new EnData();
            enData.mid = this.MerchantID;
            enData.dat = tradeData;
            enData.key = checkKeys;
            enData.chk = checkInfo;
            return enData;
        }



        public string decodeFormatData(String data)
        {
            if (data == null || data.Equals(string.Empty))
                throw new Exception("feedback data is null");
            try
            {
                string dat = "", chk = "";//, ver = "";
                String[] result = data.Split("&");
                foreach (String str in result)
                {
                    if (str.IndexOf("dat") >= 0)
                        dat = str.Split("=")[1];
                    if (str.IndexOf("chk") >= 0)
                        chk = str.Split("=")[1];
                    //if (str.indexOf("ver") >= 0)
                    //ver = str.split("=")[1];
                }
                if (!dat.Equals(string.Empty) && !chk.Equals(string.Empty))
                {
                    string dathash = SHA256Encoder.Encrypt(dat);
                    if (!dathash.Equals(chk))
                        throw new Exception("chk does not match");
                    return AES256.Decrypt(this.HashKey, this.HashIv, dat);
                }
                else
                {
                    Logger.WriteLine(data);
                    return data;
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine("error : " + e.Message);
                Logger.WriteLine(data);
                return data;
            }
        }


        protected private string ServerPost(string parameters, string url)
        {
            string szResult = String.Empty;

            byte[] byContent = Encoding.UTF8.GetBytes(parameters);

            WebRequest webRequest = WebRequest.Create(url);
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";
                webRequest.ContentLength = byContent.Length;

                using (System.IO.Stream oStream = webRequest.GetRequestStream())
                {
                    oStream.Write(byContent, 0, byContent.Length); //Push it out there
                    oStream.Close();
                }

                WebResponse webResponse = webRequest.GetResponse();
                {
                    if (null != webResponse)
                    {
                        using (StreamReader oReader = new StreamReader(webResponse.GetResponseStream()))
                        {
                            szResult = oReader.ReadToEnd().Trim();
                        }
                    }

                    webResponse.Close();
                    webResponse = null;
                }

                webRequest = null;
            }

            return szResult;
        }

        protected private void checkModel(Object ob)
        {
            var validationContext = new ValidationContext(ob, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(ob, validationContext, results, true))
            {
                StringBuilder builder = new StringBuilder();
                foreach (ValidationResult r in results)
                {
                    builder.AppendLine(r.ErrorMessage);
                }
                throw new Exception(builder.ToString());
            }
        }



    }
}