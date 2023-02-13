using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;


[Serializable]
public class GameData {
	public string path = Application.streamingAssetsPath + Path.PathSeparator + "Levels" +Path.PathSeparator;
	public string mode = "OLD"; //OLD / Homemade
	public int ending = 0;
	public string player = "Student";

	public static List<string> CompStudent = new List<string>();
	public Hashtable levelNameList = new Hashtable();
	public Hashtable compCounter = new Hashtable();
	public int[] levelScore; //levelToLoadScore[0] = best score (3 stars) ; levelToLoadScore[1] = medium score (2 stars)

	//public List<Level> homemadeList;

	public Hashtable actionBlockLimit = new Hashtable(); //Is block available in library?
	public Hashtable score = new Hashtable();

	public string scoreKey = "score";
	public int totalStep;
	public int totalActionBlocUsed;
	public int totalExecute;
	public int totalCoin;
	public int difficulty = 1;

	public float gameSpeed_default = 1f;
	public float gameSpeed_current = 1f;
	public bool dragDropEnabled = true;

	public string gameLanguage = "en";
	public string convoNode = "2.Message";
    // public List<string> playedConvoNodes = new List<string>();

    public BayesianKnowledgeTracer kt;

    public GameData(Dictionary<string, Dictionary<string, float>> KCDict)
    {
    	this.kt = new BayesianKnowledgeTracer(KCDict);
    }
}
