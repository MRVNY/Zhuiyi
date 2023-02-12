using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shui : Magic
{
    void Start()
    {
        dontDestroy.Add("Shui");
        canDestroy.Add("Huo");
    }
}
