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
        if (collision.gameObject.tag.ToLower() == obstacleName && !passed)
        {
            block.isTrigger = true;
            obstacle.SetActive(false);
            passed = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !passed)
        {
            if (other.GetComponentInChildren<Wei>() != null)
            {
                block.isTrigger = true;
                obstacle.SetActive(false);
                passed = true;
                MagicHand.Instance.Activate("");
            }
            else 
                UI.damage = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && passed)
        {
            MagicHand.Instance.Activate("");
            var tmo = ((Hallway)DungeonManager.Instance.Trainings.Last()).obstacles.Last().gameObject;
            if (Global.GD.mode == "Traning" &&
                ((Hallway)DungeonManager.Instance.Trainings.Last()).obstacles.Last().gameObject == gameObject)
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
