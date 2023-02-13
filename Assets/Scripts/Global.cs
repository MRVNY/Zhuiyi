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
    public static Dictionary<string, Dictionary<string, float>> KCDict; // Dictionary of knowledge components (characters) and their probabilities
    public static QLearning qLearning;
    public static GameData GD;

    public static string[] MagicList = new[] { "huo", "shui", "feng", "gong", "wei" };
    
    //Difficulty var
    public static int enemySpeed = 1;
    public static float slomoSpeed = 0.1f;
    
    //Only for dungeon mode
    public static int nb_agent_per_room = 5;
    
    //Only for infinite mode
    public static int spawnRadius = 15;
    public static int SpawnInterval = 10;
    
    public static string mode = "dungeon";

    static Global()
    {

        Global.charDict = new Dictionary<string, int>{  {"wei", 3}, 
                                                        {"gong", 3},
                                                        {"huo", 4},
                                                        {"shui", 4},
                                                        {"feng", 4}};

        Global.KCDict = new Dictionary<string, Dictionary<string, float>>();
        foreach (string character in Global.charDict.Keys){
            Global.KCDict[character] = new Dictionary<string, float>{{"prior", 0.2f}, {"learn", 0.2f}};
            // Note: we currently consider each character to be a knowledge component in itself. 
            // If we decide we want to differentiate the recognition and the writing of a character, this will have to change.
        }

        Global.GD = new GameData(KCDict);
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