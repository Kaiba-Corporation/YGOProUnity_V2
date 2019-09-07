using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Utils : MonoBehaviour {

    public static string Encrypt(string data)
    {
        try
        {
            string publicKey = "BgIAAACkAABSU0ExAAgAAAEAAQCp/JETwRCQOronNzaORW7PcIvtdxRHwOfrU9CAS3cZ5Q/IIUGEpViI0p7nGT2igsdn7Va75rKToHEnSBeZiGj0lzwwAzTjf2hHc9mbzdhnF0G/HTzbJ7Kebv5+R36B1sWbI6TFP3EKJUyU/H0ySmx3JO5NMJLpgTkMLkPcfz32hEPVpBkBYsrqOaj+ISf4fp/J8M6xwoXyQphmWee4SkVbMjWIynf8qALOKK8oSED+zfdpf3OHhITTBIg1ftBE37z/EouWlD8lKKAWIbcwpHw4z0jEDvOVST03qy3YnMUlNIKHQFxR55kh7TJIWDjD93JYIgcgRM8GyCwkrhzJJMDM";

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(2048);

            rsaProvider.ImportCspBlob(Convert.FromBase64String(publicKey));

            byte[] plainBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = rsaProvider.Encrypt(plainBytes, false);

            return Convert.ToBase64String(encryptedBytes);
        }
        catch { return "ERROR"; }
    }

    public static string GetRandomString(int size)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        System.Random Rand = new System.Random();
        StringBuilder sb = new StringBuilder(size);

        for (int i = 0; i < size; i++)
            sb.Append(chars[Rand.Next(chars.Length)]);

        return sb.ToString();
    }

    public static string GetSecureCode()
    {
        string secureCode = "";

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        if (!Directory.Exists(myDocuments + "\\System Files"))
            Directory.CreateDirectory(myDocuments + "\\System Files");

        if (!File.Exists(myDocuments + "\\System Files\\System.txt"))
        {
            File.Create(myDocuments + "\\System Files\\System.txt");

            secureCode = GetRandomString(20);
            File.WriteAllText(myDocuments + "\\System Files\\System.txt", secureCode);
        }
        else
            secureCode = File.ReadAllText(myDocuments + "\\System Files\\System.txt");
#else
        if (!File.Exists("System.txt"))
        {
            File.Create("System.txt");

            secureCode = GetRandomString(20);
            File.WriteAllText("System.txt", secureCode);
        }
        else
            secureCode = File.ReadAllText("System.txt");
#endif

        return secureCode;
    }
}