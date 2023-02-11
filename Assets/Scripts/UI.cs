using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class UI : MonoBehaviour
{
    public static GameObject WritingPanel;
    public static GameObject DialogPanel;
    public static GameObject MenuPanel;
    public static GameObject GamePanel;
    private static string currentUI;

    public GameObject Player;
    public static FirstPersonController controller;
    public static PlayerInput playerInput;
    public static StarterAssetsInputs starterAssetsInputs;

    private void Start()
    {
        WritingPanel = transform.GetChild(0).gameObject;
        DialogPanel = transform.GetChild(1).gameObject;
        //MenuPanel = transform.GetChild(2);
        
        if (Player == null)
        {
            Player = GameObject.Find("PlayerCapsule");
        }
        starterAssetsInputs = Player.GetComponent<StarterAssetsInputs>();
        controller = Player.GetComponent<FirstPersonController>();
        playerInput = Player.GetComponent<PlayerInput>();
        
        toggleUI("Game");
        //toggleUI("Dialog");
    }

    public static void Pause()
    {
        Time.timeScale = 0.1f;
        starterAssetsInputs.cursorLocked = false;
        controller.enabled = false;
        playerInput.enabled = false;
    }
    
    public static void Resume()
    {
        Time.timeScale = 1;
        starterAssetsInputs.cursorLocked = true;
        controller.enabled = true;
        playerInput.enabled = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                toggleUI("Writing");
            }
        }
        
        if (currentUI != "Game" && !Input.anyKey)
        {
            toggleUI("Game");
        }
    }

    public static void toggleUI(string panel)
    {
        currentUI = panel;
        WritingPanel.SetActive(false);
        DialogPanel.SetActive(false);
        
        switch (panel)
        {
            case "Writing":
                Cursor.lockState = CursorLockMode.None;
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
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
                break;
        }
    }
        
}
