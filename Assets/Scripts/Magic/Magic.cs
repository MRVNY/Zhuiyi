using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic : MonoBehaviour
{
    protected List<string> dontDestroy = new List<string>(){"Magic", "Player", "Hazard"};
    protected List<string> canDestroy = new List<string>();

    protected void LateUpdate()
    {
        //destroy if too far
        if (transform.parent == null && Vector3.Distance(MagicHand.Instance.transform.position,transform.position)>100)
            Destroy(gameObject);
    }
    
    protected void OnTriggerEnter(Collider other)
    {
        if (transform.parent == null)
        {
            if (!dontDestroy.Contains(other.tag))
            {
                Destroy(gameObject);
            }

            if (canDestroy.Contains(other.tag) && transform.parent == null)
            {
                Global.GD.kt.UpdateKnowledge(gameObject.tag, true);
                Destroy(other.gameObject);
            }
        }
    }
}
