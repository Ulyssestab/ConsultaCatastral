using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using log4net;
using ServiciosMunicipio.Controllers;

namespace ServiciosMunicipio.Utilerias
{
    public class EncriptadorSHA1
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));
        public string getSha1(string cadena)
        {
            try
            {
                SHA1Managed sha1 = new SHA1Managed();
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(cadena));

                return BitConverter.ToString(hash).Replace("-", "");
            }
            catch (Exception error)
            {
                log.Error(error);
                return "";
            }
        }
        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encryptedPassword = Convert.ToBase64String(b);
            return encryptedPassword;
        }
        public string DecryptString(string encrString)
        {
            byte[] b = Convert.FromBase64String(encrString);
            string decryptedPassword = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return decryptedPassword;
        }
        public string EncryptData(string Message)
        {
            byte[] Results;
            string passphrase = "password";

            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        public string DecryptData(string Message)
        {
            byte[] Results;
            string passphrase = "password";

            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }


    }


}