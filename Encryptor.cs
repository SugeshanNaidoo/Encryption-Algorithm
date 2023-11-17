using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
class AesEncryptionExample
{
static void Main(string[] args)
{
try
{
string plainText = "This is an encrypted message.";
byte[] encryptedData;
byte[] decryptedData;
using (Aes aes = Aes.Create())
{
aes.KeySize = 256;
aes.GenerateKey();
// Encrypt the data
encryptedData = EncryptStringToBytes_Aes(plainText, aes.Key, aes.IV);
// Decrypt the data
decryptedData = DecryptStringFromBytes_Aes(encryptedData, aes.Key, aes.IV);
// Store encrypted data in a binary file
File.WriteAllBytes("encryptedMessage.bin", encryptedData);
Console.WriteLine("Original : " + plainText);
Console.WriteLine("Encrypted : " +
Convert.ToBase64String(encryptedData));
Console.WriteLine("Decrypted : " +
Encoding.UTF8.GetString(decryptedData));
}
}
catch (Exception ex)
{
Console.WriteLine("An error occurred : " + ex.Message);
}
}
static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
{
using (Aes aesAlg = Aes.Create())
{
aesAlg.Key = key;
aesAlg.IV = iv;
ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
using (MemoryStream msEncrypt = new MemoryStream())
{
using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
{
using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
{
swEncrypt.Write(plainText);
}
}
return msEncrypt.ToArray();
}
}
}
static byte[] DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
{
using (Aes aesAlg = Aes.Create())
{
aesAlg.Key = key;
aesAlg.IV = iv;
ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
using (MemoryStream msDecrypt = new MemoryStream(cipherText))
{
using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
{
using (StreamReader srDecrypt = new StreamReader(csDecrypt))
{
return Encoding.UTF8.GetBytes(srDecrypt.ReadToEnd());
}
}
}
}
}
}