using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lobby : TrainingEnv
{
    private List<Room> rooms;
    // Start is called before the first frame update
    void Start()
    {
        rooms = GetComponentsInChildren<Room>().ToList(); 
        SequenceLegnth = rooms.Count * Global.nb_agent_per_room;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetUpTraining(List<string> Sequence)
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            rooms[i].enemyList = Sequence.GetRange(i * Global.nb_agent_per_room, Global.nb_agent_per_room);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
            OpenEnemyDoor();
    }

    private void OpenEnemyDoor()
    {
        rooms[0].OpenDoor();
    }
}
