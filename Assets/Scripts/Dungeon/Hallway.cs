using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hallway : TrainingEnv
{
    public Collider arc;
    public GameObject door;
    
    public List<Obstacle> obstacles;
    
    public GameObject HuoObstacle;
    public GameObject ShuiObstacle;
    public GameObject GongObstacle;
    public GameObject FengObstacle;
    public GameObject WeiObstacle;
    
    // Start is called before the first frame update
    void Awake()
    {
        obstacles = GetComponentsInChildren<Obstacle>().ToList();
        SequenceLegnth = obstacles.Count;
        weaknessDict = new Dictionary<string, GameObject>()
        {
            {"huo", HuoObstacle},
            {"shui", ShuiObstacle},
            {"gong", GongObstacle},
            {"feng", FengObstacle},
            {"wei", WeiObstacle}
        };
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
            DungeonManager.Instance.NextTraining();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetUpTraining(List<string> Sequence, int index)
    {
        if(index>0) OpenDoor();
        for(int i = 0; i < Sequence.Count; i++)
        {
            obstacles[i].obstacleName = Sequence[i];
            GameObject toInstantiate = weaknessDict[Sequence[i]];
            obstacles[i].obstacle = Instantiate(toInstantiate,obstacles[i].transform.position,obstacles[i].transform.rotation,obstacles[i].transform);
            obstacles[i].obstacle.SetActive(true);
        }
    }

    void OpenDoor()
    {
        arc.isTrigger = true;
        door.SetActive(false);
    }
}
