using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    private string tutoStage = "init";
    public string toLean = "huo";
    private Hallway hallway;
    
    public GameObject Tuto;
    public TextMeshProUGUI TutoText;

    public GameObject anim;
    // Start is called before the first frame update
    void Start()
    {
        TutoText = Tuto.GetComponentInChildren<TextMeshProUGUI>();
        hallway = GetComponentInChildren<Hallway>();
        hallway.SetUpTraining(new List<string>(){toLean}, 0);
        
        for(int i = 0; i < Tuto.transform.childCount; i++)
        {
            if (Tuto.transform.GetChild(i).name == toLean)
                anim = Tuto.transform.GetChild(i).gameObject;
        }
        
        Tuto.SetActive(true);
        TutoText.text = "Press E to launch canvas";
    }

    private void LateUpdate()
    {
        if (tutoStage == "init" && UI.currentUI == "Writing")
        {
            anim.SetActive(true);
            TutoText.text = "Write the character to load the magic";
            tutoStage = "writing";
        }
        
        if (tutoStage == "writing" && MagicHand.Instance.ActiveMagic != null)
        {
            if (MagicHand.Instance.ActiveMagic.tag.ToLower() == toLean)
            {
                TutoText.text = "Press E again to return to game";
                tutoStage = "loaded";
            }
            else
            {
                TutoText.text = "Try agian by pressing E twice";
            }
        }
        
        if(tutoStage == "loaded" && UI.currentUI == "Game")
        {
            anim.SetActive(false);
            TutoText.text = "Press SPACE to attack";
            tutoStage = "toAttack";
        }
        
        if(tutoStage=="toAttack" && hallway.obstacles[0].passed)
        {
            TutoText.text = "Look with pen, move with WASD"; 
            tutoStage = "attacked";
        }
        
        if(tutoStage=="attacked" && UI.currentUI == "Dialog")
        {
            TutoText.text = "";
            tutoStage = "dialog";
        }
        
        if(tutoStage=="dialog" && UI.currentUI == "Game")
        {
            Time.timeScale = 0;
            //return to menu
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        Global.GD.convoNode = toLean + ".Flashback";
        UI.toggleUI("Dialog");
    }
}
