using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : SpawnManager
{
    public static DungeonManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
