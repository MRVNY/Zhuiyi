using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarterAssets;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static GameObject WritingPanel;
    public static GameObject DialogPanel;
    public static GameObject MenuPanel;
    public static GameObject GamePanel;
    public static GameObject Grimoire;
    public static string currentUI;

    public GameObject Player;
    public static FirstPersonController controller;
    public static PlayerInput playerInput;
    public static StarterAssetsInputs starterAssetsInputs;

    private Vector2 lastPenPos;
    
    public static bool damage = false;
    public static Task damaging;
    public static bool dead = false;
    public TextMeshProUGUI Life;
    
    public static Joycon JC;

    private void Start()
    {
        WritingPanel = transform.GetChild(1).gameObject;
        DialogPanel = transform.GetChild(2).gameObject;
        GamePanel = transform.GetChild(3).gameObject;
        //MenuPanel = transform.GetChild(2);
        Grimoire = WritingPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        Grimoire.SetActive(false);

        if (Player == null)
        {
            Player = GameObject.Find("PlayerCapsule");
        }
        starterAssetsInputs = Player.GetComponent<StarterAssetsInputs>();
        controller = Player.GetComponent<FirstPersonController>();
        playerInput = Player.GetComponent<PlayerInput>();
        
        if(JoyconManager.Instance!=null && JoyconManager.Instance.j.Count > 0)
            JC = JoyconManager.Instance.j[0];

        dead = false;
        toggleUI("Dialog");
    }

    public static void Pause()
    {
        Time.timeScale = Global.GD.slomoSpeed;
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

    public async static Task Damage()
    {
        damage = false;
        Image View = GamePanel.GetComponent<Image>();
        View.color = new Color(1, 0, 0, 0.5f);
        await Task.Delay(1000);
        if(!damage) View.color = new Color(0, 0, 0, 0);
        
        damaging = null;
    }

    private void Update()
    {
        if (damage && damaging == null)
        {
            damaging = Damage();
            if (Global.GD.mode != "Intro")
            {
                Life.text = Life.text.Substring(0, Life.text.Length - 1);
                if (Life.text.Length == 0)
                {
                    dead = true;
                    Global.GD.convoNode = "death";
                    toggleUI("Dialog");
                }
            }
        }

    
        
        if (JC != null)
        {
            float[] stick = JC.GetStick();
            starterAssetsInputs.move = new Vector2(stick[0], stick[1]);
            
            if (JC.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                if (currentUI == "Game") toggleUI("Writing");
                else if (currentUI == "Writing") toggleUI("Game");
            }

            if (JC.GetButtonDown(Joycon.Button.DPAD_UP))
                ToggleHelp(false);
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.E) || (JC!=null && JC.GetButtonDown(Joycon.Button.SHOULDER_2)))
            {
                if (currentUI == "Game") toggleUI("Writing");
                else if (currentUI == "Writing") toggleUI("Game");
            }

            if (Input.GetKeyDown(KeyCode.Q) || (JC!=null && JC.GetButtonDown(Joycon.Button.DPAD_UP)))
                ToggleHelp(false);

            // Wacom view panning
            if (Input.GetMouseButtonDown(0))
                lastPenPos = Input.mousePosition;
        }

        // Wacom view panning
        else if (Input.GetMouseButton(0))
        {
            Vector2 diff = (Vector2)Input.mousePosition - lastPenPos;
            starterAssetsInputs.look = new Vector2(diff.x, -diff.y) * 0.2f;
            //Quaternion.Euler(Camera.main.transform.rotation.eulerAngles + new Vector3(-diff.y, diff.x, 0));
            lastPenPos = Input.mousePosition;
        }

        else if (Input.GetMouseButtonUp(0))
            starterAssetsInputs.look = Vector2.zero;
    }

    private static void ToggleHelp(bool onlyLowMastery)
    {   
        foreach (Animator anim in Grimoire.GetComponentsInChildren<Animator>(true))
        {
            GameObject characterGif = anim.gameObject; 
            
            if (characterGif.name == "Grimoire")
            {
                continue;
            }

            if (onlyLowMastery){
                if (Global.GD.kt.GetMasteryOf(characterGif.name) < 0.3)
                {   
                    characterGif.SetActive(true);
                } else {
                    characterGif.SetActive(false);
                }
            } else {
                characterGif.SetActive(true);
            }
            
        }
    }

    public static void toggleUI(string panel)
    {
        currentUI = panel;
        WritingPanel.SetActive(false);
        DialogPanel.SetActive(false);
        GamePanel.SetActive(false);
        
        switch (panel)
        {
            case "Writing":
                Cursor.lockState = CursorLockMode.None;
                Grimoire.SetActive(true);
                ToggleHelp(true);
                WritingPanel.SetActive(true);
                Pause();
                break;
            case "Dialog":
                DialogPanel.SetActive(true);
                Pause();
                Dialog.Instance.ClearDialog();
                Dialog.Instance.setDialog();
                break;
            case "Menu": 
                //MenuPanel.SetActive(true);
                //Pause();
                break;
            case "Game":
                GamePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
                if (dead) toMenu();
                break;
        }
    }
    
    public static void toMenu()
    {
        Global.Save();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
    }
        
}
