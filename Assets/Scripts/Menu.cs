using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MenuMenu;
    public GameObject LevelMenu;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var button in MenuMenu.GetComponentsInChildren<Button>())
        {
            if (button.name == "Levels")
                button.onClick.AddListener((() => { ShowLevels(); }));
            else if (button.name == "Reset")
                button.onClick.AddListener((() => { Global.DeleteAllSaveFiles();
                    SceneManager.LoadScene("Menu");
                }));
            else if (button.name == "Quit")
                button.onClick.AddListener((() => { Application.Quit(); }));
        }

        foreach (var button in LevelMenu.GetComponentsInChildren<Button>())
        {
            if (button.name == "Back")
                button.onClick.AddListener((() => { ShowMenu(); }));

            Transform root = GetComponentInChildren<VerticalLayoutGroup>().transform;
            GameObject buttonPrefab = root.GetChild(0).gameObject;
            
            foreach (var level in Global.GD.levelList)
            {
                GameObject NewButton = Instantiate(buttonPrefab, root);
                NewButton.GetComponentInChildren<TextMeshProUGUI>().text = level;
                string delegateLevel = level;
                if(Global.GD.availableLevels.Contains(level))
                    NewButton.GetComponent<Button>().onClick.AddListener((() => { LaunchGame(delegateLevel); }));
                else NewButton.GetComponent<Button>().interactable = false;
            }
            
            Destroy(root.GetChild(0).gameObject);
        }
        
        ShowMenu();
    }

    public void LaunchGame(string level)
    {
        Global.GD.convoNode = level;
        List<string> tmp = level.Split(": ").ToList();
        Global.GD.mode = tmp[0];
        Global.GD.actionSpace = tmp[1].Split(".").ToList();

        switch (Global.GD.mode)
        {
            case "Intro":
                SceneManager.LoadScene("Intro");
                break;
            case "Training":
                SceneManager.LoadScene("Dungeon");
                break;
            case "Infinite":
                Global.GD.actionSpace = Global.MagicList.ToList();
                if (tmp[1] == "OpenSpace") SceneManager.LoadScene("Infinite");
                else SceneManager.LoadScene("Maze");
                break;
        }
    }

    public void ShowLevels()
    {
        MenuMenu.SetActive(false);
        LevelMenu.SetActive(true);
    }
    
    public void ShowMenu()
    {
        MenuMenu.SetActive(true);
        LevelMenu.SetActive(false);
    }

    public void Reset()
    {
        Global.DeleteAllSaveFiles();
    }
}
