using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using StarterAssets;
using UnityEngine;

public class EnemyManager : SpawnManager
{
    public GameObject FireEnemy;
    public GameObject WoodEnemy;
    public GameObject BubbleEnemy;
    public GameObject AirEnemy;
    public GameObject HazardArea;
        
    private Timer timer;
    
    public static EnemyManager Instance;
    
    private void Start()
    {
        base.Start();
        Instance = this;
        weaknesses = new Dictionary<string, GameObject>()
        {
            { "huo", WoodEnemy },
            { "shui", FireEnemy },
            { "gong", BubbleEnemy },
            { "feng", AirEnemy },
            { "wei", HazardArea }
        };
        
        StartCoroutine(Spawn());
    }
    
    private IEnumerator Spawn()
    {
        while (true)
        {
            string weakness = GetSpawnSequence(1)[0];
            yield return new WaitForSeconds(Global.GD.SpawnInterval);
            int radius = Global.GD.spawnRadius;
            Vector3 spawnPos = new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius)) 
                               + FirstPersonController.Instance.transform.position;
            var enemy = Instantiate(weaknesses[weakness], spawnPos, Quaternion.identity, transform);
        }
    }
}
