using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;


[Serializable]
public class GameData {
	// public string path = Application.streamingAssetsPath + Path.PathSeparator + "Levels" +Path.PathSeparator;
	public List<string> levelList;
	public List<string> availableLevels = new List<string>();
	public List<string> actionSpace;

	//Difficulty var
	public  int enemySpeed = 1;
	public  float slomoSpeed = 0.1f;
    
	//Only for dungeon mode
	public  int nb_agent_per_room = 5;
    
	//Only for infinite mode
	public  int spawnRadius = 15;
	public  int SpawnInterval = 10;
    
	public  string mode = "dungeon";
	
	public string gameLanguage = "en";
	public string convoNode = "";
    // public List<string> playedConvoNodes = new List<string>();

    public BayesianKnowledgeTracer kt;

    public GameData(Hashtable KCDict)
    {
    	this.kt = new BayesianKnowledgeTracer(KCDict);
        levelList = new List<string>()
        {
	        "Intro: Huo",
	        "Intro: Shui",
	        "Training: Huo.Shui",
	        "Intro: Feng",
	        "Intro: Gong",
	        "Training: Huo.Shui.Feng.Gong",
	        "Intro: Wei",
	        "Training: Huo.Shui.Feng.Gong.Wei",
	        "Infinite: OpenSpace",
	        "Infinite: Maze"
        };
        availableLevels.Add(levelList[0]);
    }
}
