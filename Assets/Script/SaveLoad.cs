using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad<T>
{
    private readonly string ext = ".txt";//Ȯ����

    private FileStream file;

    public void Save(T tempData, string filename, string path)
    {
        string data = JsonUtility.ToJson(tempData);
        string FilePath = path + "/" + filename + ext;

        if (!Directory.Exists(path + "/"))
        {
            Directory.CreateDirectory(path + "/");
        }
        if (!File.Exists(FilePath))
        {
            file = File.Create(FilePath);
            file.Close();
        }
        File.WriteAllText(FilePath, data);
    }

    public bool Load(string filename, string path, out T deserialized)
    {
        string FilePath = path + "/" + filename + ext;
        string json = "";

        if (File.Exists(FilePath))
        {
            //file = File.Open(path + "/" + filename + ext, FileMode.Open);
            json = File.ReadAllText(FilePath);
        }
        else
        {
            deserialized = default;
            return false;
        }

        deserialized = JsonUtility.FromJson<T>(json);
        return true;

    }
}