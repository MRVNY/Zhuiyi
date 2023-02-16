using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHand : MonoBehaviour
{
    public static MagicHand Instance;
    public GameObject ActiveMagic;
    private GameObject Ball;
    private Camera cam;

    public GameObject Huo;
    public GameObject Shui;
    public GameObject Feng;
    public GameObject Gong;
    public GameObject Wei;

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        // Huo = GetComponentInChildren<Huo>().gameObject;
        // Shui = GetComponentInChildren<Shui>().gameObject;
        // Feng = GetComponentInChildren<Feng>().gameObject;
        // Gong = GetComponentInChildren<Gong>().gameObject;
        
        cam = Camera.main;
        Wei.SetActive(false);
        Activate("");
        //Activate("Wei");
    }

    public void Activate(string magicName)
    {   
        Global.adaptationManager.UsedMagic();
        ActiveMagic = null;
        Huo.SetActive(false);
        Shui.SetActive(false);
        Feng.SetActive(false);
        Gong.transform.parent.gameObject.SetActive(false);
        //Wei.SetActive(false);
        
        switch (magicName)
        {
            case "Huo":
                ActiveMagic = Huo;
                break;
            case "Shui":
                ActiveMagic = Shui;
                break;
            case "Feng":
                ActiveMagic = Feng;
                break;
            case "Gong":
                ActiveMagic = Gong;
                Gong.transform.parent.gameObject.SetActive(true);
                break;
            case "Wei":
                ActiveMagic = Wei;
                Global.GD.kt.UpdateKnowledge("Wei", true);
                break;
        }
        
        ActiveMagic?.SetActive(true);
    }

    public void Attack()
    {
        if(UI.JC != null) UI.JC.SetRumble (160, 320, 0.6f, 200);
        Ball = Instantiate(ActiveMagic, ActiveMagic.transform.position, ActiveMagic.transform.rotation, null);
        
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));
        Vector3 targetPosition;
        
        if (Physics.Raycast(ray, out hit))
        { 
            targetPosition = hit.point;
        }
        else
        {
            targetPosition = ray.GetPoint(10);
        }

        Vector3 direction = targetPosition - Ball.transform.position;
        Ball.GetComponent<Rigidbody>().AddForce(direction * 50);
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale==1 && ActiveMagic != null 
                              && (Input.GetKeyDown(KeyCode.Space) 
                                  || (UI.JC!=null && (UI.JC.GetButtonDown(Joycon.Button.SHOULDER_1) 
                                                      || UI.JC.GetAccel().magnitude>5))) )
        {
            if (ActiveMagic == Huo || ActiveMagic == Shui || ActiveMagic == Feng)
                Attack();
        }
    }
}
