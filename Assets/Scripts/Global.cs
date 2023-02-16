using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public static class Global
{
    public static Dictionary<string, int> charDict; // Dictionary of characters and their number of strokes
    public static Hashtable KCDict; // Dictionary of knowledge components (characters) and their probabilities
    public static QLearning qLearning;
    public static GameData GD;

    public static string[] MagicList = new[] { "Huo", "Shui", "Feng", "Gong", "Wei" };

    static Global()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
        charDict = new Dictionary<string, int>{  {"Wei", 3}, 
                                                        {"Gong", 3},
                                                        {"Huo", 4},
                                                        {"Shui", 4},
                                                        {"Feng", 4}};

        KCDict = new Hashtable();
        foreach (string character in charDict.Keys)
        {
            KCDict[character] = new Hashtable(){{"prior", 0.0f}, {"learn", 0.2f}};
            // Note: we currently consider each character to be a knowledge component in itself. 
            // If we decide we want to differentiate the recognition and the writing of a character, this will have to change.
        }

        Load();
        if(GD==null) GD = new GameData(KCDict);
    }
    
    public static void Save()
    {
        Save(GD, "GameData");
    }

    public static void Load()
    {
        //DeleteAllSaveFiles();
        GD = Load<GameData>("GameData");
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