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
    public bool passed = false;
    
    public string obstacleName = "Huo";
    public GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<Collider>();
        block.isTrigger = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Wei")
        {
            print("HERE");
        }
        //when magic touches the door
        if (collision.gameObject.tag == obstacleName && !passed)
        {
            block.isTrigger = true;
            obstacle?.SetActive(false);
            passed = true;
        }
        
        else if (collision.gameObject.tag == "Player" && !passed)
        {
            if (obstacleName == "Wei" && collision.GetComponentInChildren<Wei>() != null)
            {
                block.isTrigger = true;
                obstacle?.SetActive(false);
                passed = true;
            }
            else UI.damage = true;
        }
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     //when player touches the door
    //     if (other.gameObject.tag == obstacleName && !passed)
    //     {
    //         block.isTrigger = true;
    //         obstacle.SetActive(false);
    //         passed = true;
    //     }
    // }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && passed)
        {
            MagicHand.Instance.Activate("");
            //if(obstacleName=="Wei") Wei.Instance?.gameObject.SetActive(false);
            if (Global.GD.mode == "Training" &&
                ((Hallway)DungeonManager.Instance.Trainings.Last()).obstacles.Last() == this)
            {
                List<string> levelList = Global.GD.levelList;
                int index = levelList.IndexOf(Global.GD.convoNode);
                if(levelList.Count>index+1) Global.GD.availableLevels.Add(levelList[index + 1]);
                UI.ToMenu();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
