using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System;
using System.IO;

//Code was found here https://gist.github.com/t-kashima/5714358.

public static class AES
{
    private static string KEY = "This1KeyIsTheBestKey2EvermadeYo3";
    private static string IV = "ThisIv23KeyIsAlsoOneOfTheBestest";

    public static string encrypt(string clearText)
    {
        return EncryptRJ256(KEY, IV, clearText);
    }

    public static string decrypt(string encryptText)
    {
        return DecryptRJ256(KEY, IV, encryptText);
    }

    private static string EncryptRJ256(string pkey, string piv, string clearText)
    {
        RijndaelManaged myRijndael = new RijndaelManaged();
        myRijndael.Padding = PaddingMode.Zeros;
        myRijndael.Mode = CipherMode.CBC;
        myRijndael.KeySize = 256;
        myRijndael.BlockSize = 256;

        byte[] key = System.Text.Encoding.UTF8.GetBytes(pkey);
        byte[] IV = System.Text.Encoding.UTF8.GetBytes(piv);

        ICryptoTransform encryptor = myRijndael.CreateEncryptor(key, IV);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

        byte[] encryptByte = System.Text.Encoding.UTF8.GetBytes(clearText);

        cs.Write(encryptByte, 0, encryptByte.Length);
        cs.FlushFinalBlock();

        byte[] encrypted = ms.ToArray();

        return Convert.ToBase64String(encrypted);
    }

    private static string DecryptRJ256(string pkey, string piv, string encryptedText)
    {
        RijndaelManaged myRijndael = new RijndaelManaged();
        myRijndael.Padding = PaddingMode.Zeros;
        myRijndael.Mode = CipherMode.CBC;
        myRijndael.KeySize = 256;
        myRijndael.BlockSize = 256;

        byte[] key = System.Text.Encoding.UTF8.GetBytes(pkey);
        byte[] iv = System.Text.Encoding.UTF8.GetBytes(piv);

        ICryptoTransform decryptor = myRijndael.CreateDecryptor(key, iv);
        byte[] encryptByte = Convert.FromBase64String(encryptedText);
        byte[] encrypted = new byte[encryptByte.Length];

        MemoryStream ms = new MemoryStream(encryptByte);
        CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        cs.Read(encrypted, 0, encrypted.Length);

        return (System.Text.Encoding.UTF8.GetString(encrypted));
    }
}
