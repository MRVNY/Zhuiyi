using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : SpawnManager
{
    public static DungeonManager Instance;
    
    public List<TrainingEnv> Trainings;
    private int index = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        index = -1;
        Instance = this;
        
        Trainings = GetComponentsInChildren<TrainingEnv>().ToList();
        NextTraining();
    }
    
    public void NextTraining()
    {
        if(index >= 0)
            Trainings[index].training = false;
        
        index++;
        if(Trainings.Count > index)
        {
            List<string> Sequence = GetSpawnSequence(Trainings[index].SequenceLegnth);
            Trainings[index].SetUpTraining(Sequence, index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
