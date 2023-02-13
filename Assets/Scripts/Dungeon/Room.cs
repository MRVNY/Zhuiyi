using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Collider arc;
    public GameObject door;
    public Transform spawnPoint;

    public List<string> enemyList;
    
    public void OpenDoor()
    {
        arc.isTrigger = true;
        door.SetActive(false);
    }
}
