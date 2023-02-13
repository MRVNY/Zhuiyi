using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrainingEnv : MonoBehaviour
{
    public int SequenceLegnth = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void SetUpTraining(List<string> Sequence);

}
