using System.Security.Cryptography;
using System.Text;

namespace RapidPay.EncryptionLibrary
{
    /// <summary>
    /// Provides AES encryption and decryption functionality.
    /// </summary>
    public class AesEncryptor
    {
        /// <summary>
        /// Encrypts a plain text using AES encryption with the provided key.
        /// </summary>
        /// <param name="plainText">The plain text to be encrypted.</param>
        /// <param name="key">The encryption key.</param>
        /// <returns>The encrypted data with IV included.</returns>
        public byte[] EncryptAES(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Configure AES settings
                aesAlg.KeySize = 256; // Set key size to 256 bits
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.GenerateIV(); // Generate a random Initialization Vector (IV)

                // Create an encryption transform using the key and IV
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Use CryptoStream to write encrypted data
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(plainText);
                        csEncrypt.Write(data, 0, data.Length);
                    }

                    // Combine IV and ciphertext into a single result
                    byte[] encryptedData = msEncrypt.ToArray();
                    byte[] result = new byte[aesAlg.IV.Length + encryptedData.Length];

                    // Copy IV (Initialization Vector) to the beginning of the result array
                    Array.Copy(aesAlg.IV, result, aesAlg.IV.Length);

                    // Copy encrypted data after IV in the result array
                    Array.Copy(encryptedData, 0, result, aesAlg.IV.Length, encryptedData.Length);

                    return result;
                }
            }
        }

        /// <summary>
        /// Decrypts combined data (IV + ciphertext) using AES decryption with the provided key.
        /// </summary>
        /// <param name="combinedData">The combined data (IV + ciphertext) to be decrypted.</param>
        /// <param name="key">The decryption key.</param>
        /// <returns>The decrypted plain text.</returns>
        public string DecryptAES(byte[] combinedData, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Configure AES settings
                aesAlg.KeySize = 256; // Set key size to 256 bits
                aesAlg.Key = Encoding.UTF8.GetBytes(key);

                // Extract IV from combined data
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                byte[] encryptedData = new byte[combinedData.Length - iv.Length];

                // Copy the first bytes from combinedData to iv
                Array.Copy(combinedData, iv, iv.Length);

                // Copy the remaining bytes after IV in combinedData to encryptedData
                Array.Copy(combinedData, iv.Length, encryptedData, 0, encryptedData.Length);

                aesAlg.IV = iv; // Set the IV for decryption

                // Create a decryption transform using the key and IV
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                {
                    // Use CryptoStream to read and decrypt data
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read decrypted data and return it as plain text
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}