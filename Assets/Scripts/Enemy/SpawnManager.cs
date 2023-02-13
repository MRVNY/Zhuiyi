using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using JetBrains.Annotations;
using StarterAssets;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    protected Dictionary<string, GameObject> weaknesses;
    
    public string weakness = "huo";
    protected void Start()
    {
    }
    
    public string GetWeakness()
    {
        return Global.MagicList[Random.Range(0, Global.MagicList.Length)];
    }

    public List<string> GetSpawnSequence(int SequenceLegnth)
    {
        List<string> randomSquence = new List<string>();
        for (int i = 0; i < SequenceLegnth; i++)
        {
            randomSquence.Add(GetWeakness());
        }
        
        return randomSquence;
    }
}
