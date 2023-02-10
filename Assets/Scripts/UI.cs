using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI : MonoBehaviour
{
    public static GameObject WritingPanel;
    public static GameObject DialogPanel;
    public static GameObject MenuPanel;

    public GameObject Player;
    public static FirstPersonController Controller;
    public static PlayerInput Input;

    private void Start()
    {
        WritingPanel = transform.GetChild(0).gameObject;
        DialogPanel = transform.GetChild(1).gameObject;
        //MenuPanel = transform.GetChild(2);
        
        Controller = Player.GetComponent<FirstPersonController>();
        Input = Player.GetComponent<PlayerInput>();
        
        //toggleUI("Game");
        toggleUI("Dialog");
    }

    public static void Pause()
    {
        Controller.enabled = false;
        Input.enabled = false;
    }
    
    public static void Resume()
    {
        Controller.enabled = true;
        Input.enabled = true;
    }

    public static void toggleUI(string panel)
    {
        WritingPanel.SetActive(false);
        DialogPanel.SetActive(false);
        
        switch (panel)
        {
            case "Writing":
                WritingPanel.SetActive(true);
                Pause();
                break;
            case "Dialog":
                DialogPanel.SetActive(true);
                Pause();
                break;
            case "Menu": 
                //MenuPanel.SetActive(true);
                //Pause();
                break;
            case "Game":
                Resume();
                break;
        }
    }
        
}
