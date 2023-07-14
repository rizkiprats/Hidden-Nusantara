using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

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
        if (File.Exists(fullPath))
        {
            try
            {
                //memuat serialisasi data dari file save
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameDataSave>(dataToLoad);
            }
            catch(Exception e)
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
            //Membuat direkrori untuk file save game yang akan ditulis jika belum terbuat
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialisasi data kedalam bentuk file json
            string dataToStore = JsonUtility.ToJson(data, true);

            //proses menulis file kedalam json
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file" + fullPath + "\n" + e);
        }

    }

}
