using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public static class Global
{
    public static GameData GD;

    public static string[] MagicList = new[] { "huo", "shui", "feng", "gong", "wei" };

    static Global()
    {
        GD = new GameData();
    }
    
    public static void Save()
    {
        Save(Global.GD, "GameData");
    }

    public static void Load()
    {
        Global.GD = Load<GameData>("GameData");
    }
    
    private static string savePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "saves" + Path.DirectorySeparatorChar;

    public static bool SaveExists(string key)
    {
        string path = savePath + key + ".txt";
        return File.Exists(path);
    }
    
    public static void Save<T>(T objectToSave, string key)
    {
        //création du fichier si il n'existe pas
        Directory.CreateDirectory(savePath);
        
        // convertion des paramètres de sauvegarde en fichier binaires
        BinaryFormatter formatter = new BinaryFormatter();

        using(FileStream fileStream = new FileStream(savePath + key + ".txt", FileMode.Create))
        {
            formatter.Serialize(fileStream, objectToSave);
        }
    }
    
    public static T Load<T>(string key)
    {
        if (SaveExists(key))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            T returnValue = default(T);
            using (FileStream fileStream = new FileStream(savePath + key + ".txt", FileMode.Open))
            {
                returnValue = (T)formatter.Deserialize(fileStream);
            }

            return returnValue;
        }
        else return default(T);
    }
    
    public static void DeleteAllSaveFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(savePath);
        dir.Delete(true);
        Directory.CreateDirectory(savePath);
        PlayerPrefs.DeleteAll();
    }
}