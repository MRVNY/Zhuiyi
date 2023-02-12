using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarterAssets;
using UnityEngine;

public class Wei : Magic
{
    private Animator anim;
    private Task playing;

    private void Start()
    {
        dontDestroy.Add("Wei");
    }

    private async void OnEnable()
    {
        anim = GetComponent<Animator>();
        
        PlayAnim("Expand");
        await Task.Delay(TimeSpan.FromSeconds(20));
        playing = PlayAnim("Shrink");
        
        await playing;
        gameObject.SetActive(false);
    }
    
    private Task PlayAnim(string animName)
    {
        anim.Play(animName);
        return Task.Delay(2000);
    }

    private async void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().blocked = true;
            Vector3 horizontalForce = new Vector3(
                transform.position.x - collision.transform.position.x,
                0,
                transform.position.z - collision.transform.position.z);
            
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-horizontalForce * 50, ForceMode.Impulse);
            await Task.Delay(2000);
        }
    }
}
