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
        foreach (var enemy in enemyList)
        {
            
        }
        arc.isTrigger = true;
        door.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
