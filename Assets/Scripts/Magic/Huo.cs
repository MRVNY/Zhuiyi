using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Huo : Magic
{
    private void Start()
    {
        dontDestroy.Add("Huo");
        canDestroy.Add("Wood");
    }
}
