using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using JetBrains.Annotations;
using StarterAssets;
using UnityEngine;
using Numpy;

public class SpawnManager : MonoBehaviour
{
    protected Dictionary<string, GameObject> weaknesses;
    
    protected void Start()
    {
    }
    
    public string GetWeakness()
    {
        return Global.MagicList[Random.Range(0, Global.MagicList.Length)];
    }

    public List<string> GetSpawnSequence(int sequenceLength)
    {
        NDarray p = Softmax(0.8f); // tau value to be fixed with experimentation
        List<string> randomSequence = new List<string>();
        for (int i = 0; i < sequenceLength; i++)
        {
            string[] result = np.random.choice(np.array(Global.charDict.Keys.ToArray()), null, true, p).GetData<string>();
            randomSequence.Add(result[0]);
        }
        
        return randomSequence;
    }

    public NDarray Softmax(float tau){

        NDarray masteryValues = np.array(Global.GD.kt.GetCurrentState().Values.ToArray());
        NDarray p = np.zeros(masteryValues.size); // probability distribution over all actions
        float sump = 0.0f;
        for (int i = 0; i < masteryValues.size; i++){
            p[i] = np.exp(-masteryValues[i] / tau); // We take the negative of the mastery value in order to prioritise weak skills
            sump = sump + (float) p[i];
        }
        for (int i = 0; i < masteryValues.size; i++){
            p[i] = p[i] / sump;
        }
        return p;
    }
}
