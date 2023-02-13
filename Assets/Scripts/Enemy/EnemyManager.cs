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
    
    public string weakness = "huo";
    
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
            yield return new WaitForSeconds(SpawnInterval);
            weakness = GetWeakness();
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + FirstPersonController.Instance.transform.position;
            var enemy = Instantiate(weaknesses[weakness], spawnPos, Quaternion.identity, transform);
        }
    }
}
