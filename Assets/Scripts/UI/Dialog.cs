using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private string node;
    private JObject convoTree;

    private TextMeshProUGUI TextBox;
    private List<string> toWrite = new List<string>();
    private string written = "";
    
    private Task writing;
    public Button skipButton;
    bool skipped = false;
    
    public static Dialog Instance;

    private void Awake()
    {
        Instance = this;
        
        if (Global.GD.convoNode != null)
        {
            convoTree = JObject.Parse(File.ReadAllText(Application.streamingAssetsPath+Path.DirectorySeparatorChar+"Dialog.json"));
            node = Global.GD.convoNode;
            
            TextBox = GetComponentInChildren<TextMeshProUGUI>();
            skipButton = GetComponent<Button>();
            skipButton.onClick.AddListener(() => { Next(); });
        }
    }

    void Start()
    {
        
    }
    
    public void ClearDialog()
    {
        toWrite.Clear();
        written = "";
        TextBox.text = "";
    }
    
    public void setDialog()
    {
        node = Global.GD.convoNode;
        JToken jNode = convoTree[node];
        if (jNode == null)
        {
            UI.toggleUI("Game");
            return;
        }

        // set text
        if (toWrite.Count == 0)
        {
            string text = jNode[Global.GD.gameLanguage].ToString();
            //if (text.Contains("{name}")) text = text.Replace("{name}", Global.GD.player);
            toWrite = text.Split('\n').ToList();
        }

        writing = TypeWriter(toWrite[0]);
    }

    async Task TypeWriter(string toType)
    {
        skipped = false;
        skipButton.enabled = true;
        string story = toType;
        TextBox.text = written;
        foreach (char c in story) 
        {
            if(skipped) break;
            TextBox.text += c;
            await Task.Delay(10);
        }
        written += story + "\n\n";
        skipped = true;
    }
    
    public async void Next()
    {
        if (skipped && toWrite.Count <= 1)
        {
            UI.toggleUI("Game");
            toWrite.Clear();
        }

        else if (skipped)
        {
            toWrite.RemoveAt(0);
            skipButton.enabled = false;
            if (toWrite.Count == 0)
            {
                UI.toggleUI("Game");
            }
            else setDialog();
        }
		
        else if (writing != null)
        {
            skipped = true;
            await writing;
            TextBox.text = written;
			
            if (toWrite.Count==1) skipButton.enabled = true;
        }
    }
}
