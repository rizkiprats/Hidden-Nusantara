using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;

public class FileDataHandler  
{
    private string dataDirPath = "";

    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameDataSave Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameDataSave loadedData = null;

        if (File.Exists(fullPath) && PlayerPrefs.HasKey("key"))
        {
            try
            {
                //using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
                //{
                //    using (StreamReader sReader = new StreamReader(fileStream))
                //    {
                //        string dataToLoad = sReader.ReadToEnd();
                //        sReader.Close();
                //        loadedData = JsonUtility.FromJson<GameDataSave>(dataToLoad);
                //    }
                //}

                byte[] savedKey = Convert.FromBase64String(PlayerPrefs.GetString("key"));

                using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
                {
                    Aes Output_AES = Aes.Create();

                    byte[] outputIV = new byte[Output_AES.IV.Length];

                    fileStream.Read(outputIV, 0, outputIV.Length);

                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, Output_AES.CreateDecryptor(savedKey, outputIV), CryptoStreamMode.Read))
                    {
                        using (StreamReader sReader = new StreamReader(cryptoStream))
                        {
                            string dataToLoad = sReader.ReadToEnd();
                            sReader.Close();
                            loadedData = JsonUtility.FromJson<GameDataSave>(dataToLoad);
                        }
                    }
                }

                //byte[] savedKey = System.Convert.FromBase64String(PlayerPrefs.GetString("key"));
                //FileStream fileStream = new FileStream(fullPath, FileMode.Open);
                //Aes Output_AES = Aes.Create();
                //byte[] outputIV = new byte[Output_AES.IV.Length];
                //fileStream.Read(outputIV, 0, outputIV.Length);
                //CryptoStream cryptoStream = new CryptoStream(fileStream, Output_AES.CreateDecryptor(savedKey, outputIV), CryptoStreamMode.Read);
                //StreamReader sReader = new StreamReader(cryptoStream);
                //string dataToLoad = sReader.ReadToEnd();
                //sReader.Close();
                //loadedData = JsonUtility.FromJson<GameDataSave>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file" + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameDataSave data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        
        try
        {
            //Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            //{
            //        using (StreamWriter sWriter = new StreamWriter(fileStream))
            //        {
            //            string dataToStore = JsonUtility.ToJson(data, true);
            //            sWriter.Write(dataToStore);
            //            sWriter.Close();
            //        }
            //    fileStream.Close();
            //}

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            Aes Input_AES = Aes.Create();
            byte[] savedKey = Input_AES.Key;

            string key = Convert.ToBase64String(savedKey);
            PlayerPrefs.SetString("key", key);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                byte[] inputIV = Input_AES.IV;

                fileStream.Write(inputIV, 0, inputIV.Length);

                using (CryptoStream cryptoStream = new CryptoStream(fileStream, Input_AES.CreateEncryptor(Input_AES.Key, Input_AES.IV), CryptoStreamMode.Write))
                {
                    using (StreamWriter sWriter = new StreamWriter(cryptoStream))
                    {
                        string dataToStore = JsonUtility.ToJson(data, true);
                        sWriter.Write(dataToStore);
                        sWriter.Close();
                    }

                    cryptoStream.Close();
                }

                fileStream.Close();
            }

            //Aes iAes = Aes.Create();
            //byte[] savedKey = iAes.Key;
            //string key = Convert.ToBase64String(savedKey);
            //PlayerPrefs.SetString("key", key);
            //FileStream fileStream = new FileStream(fullPath, FileMode.Create);
            //byte[] inputIV = iAes.IV;
            //fileStream.Write(inputIV, 0, inputIV.Length);
            //CryptoStream cryptoStream = new CryptoStream(fileStream, iAes.CreateEncryptor(iAes.Key, iAes.IV), CryptoStreamMode.Write);
            //StreamWriter sWriter = new StreamWriter(cryptoStream);
            //string jsonString = JsonUtility.ToJson(data, true);
            //sWriter.Write(jsonString);
            //sWriter.Close();
            //cryptoStream.Close();
            //fileStream.Close();

        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file" + fullPath + "\n" + e);
        }
    }

    public void Delete()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if (File.Exists(fullPath))
        {
            try
            {
                File.Delete(fullPath);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to Delete data from file" + fullPath + "\n" + e);
            }
        }
    }
}