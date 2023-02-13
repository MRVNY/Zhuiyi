using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHand : MonoBehaviour
{
    public static MagicHand Instance;
    private GameObject ActiveMagic;
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
        //Activate("wei");
    }

    public void Activate(string magicName)
    {   
        ActiveMagic = null;
        Huo.SetActive(false);
        Shui.SetActive(false);
        Feng.SetActive(false);
        Gong.transform.parent.gameObject.SetActive(false);
        //Wei.SetActive(false);
        
        switch (magicName)
        {
            case "huo":
                ActiveMagic = Huo;
                break;
            case "shui":
                ActiveMagic = Shui;
                break;
            case "feng":
                ActiveMagic = Feng;
                break;
            case "gong":
                ActiveMagic = Gong;
                Gong.transform.parent.gameObject.SetActive(true);
                break;
            case "wei":
                ActiveMagic = Wei;
                break;
        }
        
        ActiveMagic?.SetActive(true);
    }

    public void Attack()
    {
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
        if (Time.timeScale==1 && Input.GetKeyDown(KeyCode.Space) && ActiveMagic != null)
        {
            if (ActiveMagic == Huo || ActiveMagic == Shui || ActiveMagic == Feng)
                Attack();
        }
    }
}
