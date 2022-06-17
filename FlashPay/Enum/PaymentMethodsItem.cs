using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashPay.Enum
{
    public enum PaymentMethodsItem
    {
     /// <summary>
     /// 信用卡一次付清。
     /// </summary>
     CreditAll = 0,
     /// <summary>
     /// 信用卡三次付清。
     /// </summary>
     Credit3 = 3,
        /// <summary>
        /// 信用卡六次付清。
        /// </summary>
        Credit6 = 6,
        /// <summary>
        /// 信用卡十二次付清。
        /// </summary>
        Credit12 = 12,
        /// <summary>
        /// 信用卡十八次付清。
        /// </summary>
        Credit18 = 18,
        /// <summary>
        /// 信用卡二十四次付清。
        /// </summary>
        Credit24 = 24,
        /// <summary>
        /// 銀聯卡一次付清。
        /// </summary>
        UnionAll = 0,
    }
}
