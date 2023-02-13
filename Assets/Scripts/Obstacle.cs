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
    
    public string puzzle = "huo";
    private Dictionary<string, GameObject> puzzleDict;

    public GameObject HuoPuzzle;
    public GameObject ShuiPuzzle;
    public GameObject GongPuzzle;
    public GameObject FengPuzzle;
    public GameObject WeiPuzzle;
    
    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<Collider>();
        block.isTrigger = false;
        
        puzzleDict = new Dictionary<string, GameObject>()
        {
            {"huo", HuoPuzzle},
            {"shui", ShuiPuzzle},
            {"gong", GongPuzzle},
            {"feng", FengPuzzle},
            {"wei", WeiPuzzle}
        };

        //puzzle = puzzleDict.Keys.ToList()[Random.Range(0, puzzleDict.Keys.Count)];
        //puzzle = "wei";
        
        SetPuzzle();
    }

    private void SetPuzzle()
    {
        // if(puzzle == "wei")
        //     block.isTrigger = true;
        // else
        //     block.isTrigger = false;
        puzzleDict[puzzle].SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.ToLower() == puzzle)
        {
            block.isTrigger = true;
            puzzleDict[puzzle].SetActive(false);
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
                puzzleDict[puzzle].SetActive(false);
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
