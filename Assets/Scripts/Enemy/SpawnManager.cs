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
        List<float> p = Softmax(0.8f); // tau value to be fixed with experimentation
        List<string> Sequence = new List<string>();
        for (int i = 0; i < sequenceLength; i++)
        {
            List<float> cumsum = new List<float>();
            float sum = 0.0f;
            for (int j = 0; j < p.Count; j++)
            {
                sum = sum + p[j];
                cumsum.Add(sum);
            }
            float r = Random.Range(0.0f, 1.0f);
            for (int j = 0; j < p.Count; j++)
            {
                if (r < cumsum[j])
                {
                    Sequence.Add(Global.MagicList[j]);
                    break;
                }
            }
        }
        
        return Sequence;
    }

    public List<float> Softmax(float tau){
        //get softmax of mastery values
        List<float> states = Global.GD.kt.GetCurrentState();
        //calculate softmax from states
        List<float> p = new List<float>();
        float sump = 0.0f;
        for (int i = 0; i < states.Count; i++){
            p.Add(Mathf.Exp(-states[i] / tau)); // We take the negative of the mastery value in order to prioritise weak skills
            sump = sump + p[i];
        }
        for (int i = 0; i < states.Count; i++){
            p[i] = p[i] / sump;
        }

        return p;
    }
}
