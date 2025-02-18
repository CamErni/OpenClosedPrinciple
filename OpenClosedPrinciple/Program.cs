using System;
using System.Security.Cryptography;
using System.Text;



public interface IType
{
    string EncryptData(string text);

}
public class AESType : IType
{
    byte[] aeskey ={
        0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
        0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
        0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F
    };
    byte[] aesiv =
    {
        0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
    0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
    };
    public string EncryptData(string text)
    {
        // AES Encryption
        using (Aes aes = Aes.Create())
        {
            aes.Key = aeskey;
            aes.IV = aesiv;


            // You need to implement this method in your AES class
            byte[] encryptedMessage;
            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
                    }
                    encryptedMessage = ms.ToArray();
                    Console.WriteLine($"Plaintext: {text}");
                    return $"Encrypted Data (AES): {Convert.ToBase64String(encryptedMessage)}";
                }
            }

        }

    }
}
public class RSAType : IType
{

    public string EncryptData(string text)
    {
        // RSA Encryption
        using (RSA rsa = RSA.Create())
        {

            // You need to implement this method in your RSA class
            string encryptedData = Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA256));
            Console.WriteLine($"Plaintext: {text}");
            return $"Encrypted Data (RSA): {encryptedData}";
        }

    }
}


class Program
{

    static void Main(string[] args)
    {


        var rsa = new RSAType();
        Console.WriteLine(rsa.EncryptData("Encrypted message for asymmetrical encryption"));
        var aes = new AESType();
        Console.WriteLine(aes.EncryptData("Encrypted message for symmetrical encryption"));

    }
}