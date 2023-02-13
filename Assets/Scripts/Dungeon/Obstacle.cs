using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    private Collider damage;
    public Collider block;
    
    public string obstacleName = "huo";
    public GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<Collider>();
        block.isTrigger = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.ToLower() == obstacleName)
        {
            block.isTrigger = true;
            obstacle.SetActive(false);
            damage.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponentInChildren<Wei>() != null)
            {
                block.isTrigger = true;
                obstacle.SetActive(false);
                damage.enabled = false;
            }
            else 
                UI.damage = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
