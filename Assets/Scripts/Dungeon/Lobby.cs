using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Lobby : TrainingEnv
{
    private List<Room> rooms;
    // Start is called before the first frame update
    public GameObject FireEnemy;
    public GameObject WoodEnemy;
    public GameObject BubbleEnemy;
    public GameObject AirEnemy;

    public Transform EnemyRoot;

    private int roomRadius = 2;
    
    public string weakness = "huo";
    void Start()
    {
        rooms = GetComponentsInChildren<Room>().ToList(); 
        SequenceLength = rooms.Count * Global.GD.nb_agent_per_room;
        
        weaknessDict = new Dictionary<string, GameObject>()
        {
            { "huo", WoodEnemy },
            { "shui", FireEnemy },
            { "gong", BubbleEnemy },
            { "feng", AirEnemy },
            { "wei", BubbleEnemy }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (training && EnemyRoot.childCount == 0)
        {
            if(rooms.Count == 0)
                DungeonManager.Instance.NextTraining();
            else
            {
                rooms.RemoveAt(0);
                if(rooms.Count == 0)
                    DungeonManager.Instance.NextTraining();
                else
                    OpenEnemyDoor();
            }
        }
    }

    public override void SetUpTraining(List<string> Sequence, int index)
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            rooms[i].enemyList = Sequence.GetRange(i * Global.GD.nb_agent_per_room, Global.GD.nb_agent_per_room);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OpenEnemyDoor();
            training = true;
        }
    }

    private void OpenEnemyDoor()
    {
        foreach (var enemy in rooms[0].enemyList)
        {
            GameObject toInstantiate = weaknessDict[enemy];
            Vector3 spawnPos = rooms[0].spawnPoint.position + new Vector3(UnityEngine.Random.Range(-roomRadius, roomRadius), 0, UnityEngine.Random.Range(-roomRadius, roomRadius));
            GameObject enemyInstance = Instantiate(toInstantiate, spawnPos, Quaternion.identity, EnemyRoot);
            enemyInstance.SetActive(true);
        }
        rooms[0].OpenDoor();
    }
    
    
}
