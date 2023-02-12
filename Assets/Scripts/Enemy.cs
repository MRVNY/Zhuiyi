using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarterAssets;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public bool blocked = false;
    public AnimatorController Idle;
    public AnimatorController Walk;
    public AnimatorController Run;
    public AnimatorController Attack;

    void Start()
    {
        if (tag != "Bubble" && tag != "Hazard")
        {
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = Idle;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tag != "Hazard")
        {
            if (!blocked && Vector3.Distance(transform.position, FirstPersonController.Instance.transform.position) <
                20)
            {
                if (tag != "Bubble") animator.runtimeAnimatorController = Walk;
                transform.position = Vector3.MoveTowards(transform.position,
                    FirstPersonController.Instance.transform.position, 0.01f);
                transform.rotation =
                    Quaternion.LookRotation(FirstPersonController.Instance.transform.position - transform.position);
            }
            else
            {
                if (tag != "Bubble") animator.runtimeAnimatorController = Idle;
            }

            if (blocked)
            {
                StartCoroutine(WaitToUnblock());
            }
        }
    }
    
    IEnumerator WaitToUnblock()
    {
        yield return new WaitForSeconds(2);
        blocked = false;
    }

    private async void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && tag != "Hazard")
        {
            UI.damage = true;
        }
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && tag == "Hazard")
        {
            bool checkShield = MagicHand.Instance.Wei.gameObject.activeSelf;
            if (!checkShield)
            {
                UI.damage = true;
            }
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Huo")
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}
