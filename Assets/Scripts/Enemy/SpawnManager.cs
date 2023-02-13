using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using StarterAssets;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    protected Dictionary<string, GameObject> weaknesses;
    
    public string weakness = "huo";
    
    // Difficulty only for infinite mode
    public int spawnRadius = 15;
    public int SpawnInterval = 10;
    
    // Difficulty
    public int enemySpeed = 1;
    public float slomoSpeed = 0.1f;
    protected void Start()
    {
    }
    
    public string GetWeakness()
    {
        return Global.MagicList[Random.Range(0, Global.MagicList.Length)];
    }


}
