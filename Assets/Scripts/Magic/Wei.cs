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
    public static Wei Instance;

    private void Start()
    {
        Instance = this;
        dontDestroy.Add("Wei");
    }

    private async void OnEnable()
    {
        anim = GetComponent<Animator>();
        
        PlayAnim("Expand");
        await Task.Delay(TimeSpan.FromSeconds(10));
        playing = PlayAnim("Shrink");
        
        await playing;
        gameObject.SetActive(false);
    }
    
    private async Task PlayAnim(string animName)
    {
        if (anim != null)
        {
            anim.Play(animName);
            await Task.Delay(TimeSpan.FromSeconds(anim.GetCurrentAnimatorStateInfo(0).length));
        }
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
