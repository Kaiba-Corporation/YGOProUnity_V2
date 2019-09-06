using System;
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
}
