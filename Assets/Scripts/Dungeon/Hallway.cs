using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hallway : TrainingEnv
{
    public Collider arc;
    public GameObject door;
    
    private List<Obstacle> obstacles;
    
    public GameObject HuoObstacle;
    public GameObject ShuiObstacle;
    public GameObject GongObstacle;
    public GameObject FengObstacle;
    public GameObject WeiObstacle;
    
    public Dictionary<string,GameObject> ObstacleDict;

    // Start is called before the first frame update
    void Awake()
    {
        obstacles = GetComponentsInChildren<Obstacle>().ToList();
        ObstacleDict = new Dictionary<string, GameObject>()
        {
            {"huo", HuoObstacle},
            {"shui", ShuiObstacle},
            {"gong", GongObstacle},
            {"feng", FengObstacle},
            {"wei", WeiObstacle}
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetUpTraining(List<string> Sequence)
    {
        for(int i = 0; i < Sequence.Count; i++)
        {
            obstacles[i].obstacleName = Sequence[i];
            GameObject toInstantiate = ObstacleDict[Sequence[i]];
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
