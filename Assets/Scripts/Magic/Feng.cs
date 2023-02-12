using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feng : Magic
{
    // Start is called before the first frame update
    void Start()
    {
        dontDestroy.Add("Feng");
        canDestroy.Add("Air");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
