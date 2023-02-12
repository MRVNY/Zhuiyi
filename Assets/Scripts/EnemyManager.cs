using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using StarterAssets;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject FireEnemy;
    public GameObject WoodEnemy;
    public GameObject BubbleEnemy;
    public GameObject AirEnemy;
    public GameObject HazardArea;

    private Dictionary<string, GameObject> weaknesses;

    public int spawnRadius = 15;
    public string weakness = "huo";
    public int SpawnInterval = 10;
    
    private Timer timer;
    
    private void Start()
    {
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
            weakness = weaknesses.Keys.ToList()[Random.Range(0, weaknesses.Keys.Count-1)];
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + FirstPersonController.Instance.transform.position;
            var enemy = Instantiate(weaknesses[weakness], spawnPos, Quaternion.identity, transform);
        }
    }
    
    
}
