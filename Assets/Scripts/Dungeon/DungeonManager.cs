using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : SpawnManager
{
    public static DungeonManager Instance;

    public List<TrainingEnv> Trainings;
    public TrainingEnv CurrentTraining;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Instance = this;
        
        Trainings = GetComponentsInChildren<TrainingEnv>().ToList();
        
        CurrentTraining = Trainings[0];
        List<string> Sequence = GetSpawnSequence(CurrentTraining.SequenceLegnth);
        CurrentTraining.SetUpTraining(Sequence);
    }
    
    public void NextTraining()
    {
        // if (CurrentTraining.TryGetComponent<Hallway>(out Hallway hallway))
        // {
        //     hallway.OpenDoor();
        // }
        //
        // TrainingSequence.RemoveAt(0);
        // CurrentTraining = TrainingSequence[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
