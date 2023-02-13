using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    private List<Room> rooms;
    // Start is called before the first frame update
    void Start()
    {
        rooms = GetComponentsInChildren<Room>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
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
