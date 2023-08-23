using System.Security.Cryptography;
using System.Text;

namespace SB.WebApi.Services
{
    public interface IEncryptionValidator
    {
        string CalculateHMAC256(string data, string key);

        bool VerifyAuthenticity(string hash, string privateKey, string data);
    }
    public class EncryptionValidator : IEncryptionValidator
    {
        private string ByteArrayToHexString(byte[] data)
        {
            // Crea un nuevo StringBuilder para almacenar el resultado
            StringBuilder sb = new StringBuilder(data.Length * 2);

            // Convierte cada byte a una cadena hexadecimal y la añade al StringBuilder
            foreach (byte b in data)
            {
                sb.AppendFormat("{0:x2}", b);
            }

            // Devuelve la cadena resultante
            return sb.ToString();
        }

        public string CalculateHMAC256(string key, string data)
        {
            // Convierte la clave y los datos a bytes
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            // Crea una instancia de HMACSHA256 usando la clave
            using HMACSHA256 hmac = new HMACSHA256(keyBytes);
            // Calcula el hash de los datos
            byte[] hash = hmac.ComputeHash(dataBytes);

            // Convierte el hash a una cadena hexadecimal y la devuelve
            return ByteArrayToHexString(hash);
        }
 

        public bool VerifyAuthenticity(string hash, string privateKey, string data)
        {
            string hmacValue = CalculateHMAC256(privateKey, data);
            return hmacValue == hash;
        }
    }
}
