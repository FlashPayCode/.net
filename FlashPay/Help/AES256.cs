using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FlashPay.Help
{


   

    public static class AES256
    {



        private static string HexStringFromBytes(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();
        }

        private static byte[] hex2bin(string hexContent)
        {
            hexContent = hexContent.Replace(" ", "");
            byte[] buffer = new byte[hexContent.Length / 2];
            for (int i = 0; i < hexContent.Length; i += 2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(hexContent.Substring(i, 2), 16);
            }
            return buffer;
        }


        internal static string Encrypt(string HashKey,string HashIv,string originalString)
        {
            byte[] cipherData;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(HashKey);
                aes.IV = Encoding.UTF8.GetBytes(HashIv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cipher = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, cipher, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(originalString);
                        }
                    }

                    cipherData = ms.ToArray();
                }
            }
            return HexStringFromBytes(cipherData);
        }

        internal static string Decrypt(string HashKey, string HashIv, string EncodeString)
        {
            string plainText;
            byte[] combinedData = hex2bin(EncodeString);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(HashKey);
                aes.IV = Encoding.UTF8.GetBytes(HashIv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(combinedData))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            plainText = sr.ReadToEnd();
                        }
                    }

                    return plainText;
                }
            }
        }
    }


}
