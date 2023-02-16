using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;
using PDollarGestureRecognizer;

public class DrawStrokes : MonoBehaviour, IDragHandler, IDropHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject linePrefab;
    
    private GameObject currentLine;
    private UILineRenderer lineRenderer;
    private List<UILineRenderer> lines = new List<UILineRenderer>();
    
    private List<List<Vector2>> Writings = new List<List<Vector2>>();
    private List<Vector2> points = new List<Vector2>();

    private int strokeIndex = 0;
    List<PDollarGestureRecognizer.Point> allPoints = new List<PDollarGestureRecognizer.Point>();

    private Vector2 rectPos;

    private Gesture[] trainingSet = null;   // training set loaded from XML files
    
    private string[] three_strokes = new[] { "Wei", "Gong" };
    private string[] four_strokes = new[] { "Huo", "Shui", "Feng" };
    private void Start()
    {
        trainingSet = LoadTrainingSet();
    }

    private void OnEnable()
    {

        strokeIndex = 0;
        allPoints.Clear();

        Writings.Clear();
        points.Clear();
        
        foreach (var line in GetComponentsInChildren<UILineRenderer>())
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
    }

    public void OnDrag(PointerEventData eventData)
    {
        float x = eventData.position.x - rectPos.x;
        float y = eventData.position.y - rectPos.y;
        points.Add(new Vector2(x, y));
        RefreshLine();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentLine = Instantiate(linePrefab, new Vector3(0,0,0), Quaternion.identity);
        currentLine.transform.SetParent(transform);
        lineRenderer = currentLine.GetComponent<UILineRenderer>();

        float x = eventData.position.x - rectPos.x;
        float y = eventData.position.y - rectPos.y;
        points.Add(new Vector2(x, y));
        allPoints.Add(new PDollarGestureRecognizer.Point(x, y, strokeIndex));
        //Debug.Log("strokeIndex:" + strokeIndex);
        RefreshLine();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (lineRenderer.Points.Length > 2)
        {
            Writings.Add(lineRenderer.Points.ToList());
            lines.Add(lineRenderer);
            foreach (var point in lineRenderer.Points)
            {
                allPoints.Add(new PDollarGestureRecognizer.Point(point.x, point.y, strokeIndex));
            }
            strokeIndex++;
        }
        else
        {
            Destroy(currentLine);
        }
        currentLine = null;
        points.Clear();
        
        Recognize();
    }
    
    private void RefreshLine()
    {
        lineRenderer.Points = points.ToArray();
        lineRenderer.SetAllDirty();
    }

    private void Recognize()
    {
        if (strokeIndex >= 3){
            Gesture candidate = new Gesture(allPoints.ToArray());
            (string,float) output = PointCloudRecognizer.Classify(candidate, trainingSet);
            
            string gestureClass = output.Item1;
            float score = output.Item2;
            
            if ((three_strokes.Contains(gestureClass) && strokeIndex == 3) || 
                (four_strokes.Contains(gestureClass) && strokeIndex == 4))
            {
                GiveFeedback(true);
                Debug.Log(gestureClass + ": " + score);
                MagicHand.Instance.Activate(gestureClass);
            }
        }         

    }

    private void GiveFeedback(bool correct)
    {
        if (correct){
            foreach (var line in lines)
            {
                line.color = Color.green;
            }
            if(UI.JC != null) UI.JC.SetRumble (160, 320, 0.6f, 20);
        }
    }

    /// <summary>
    /// Loads training gesture samples from XML files
    /// </summary>
    /// <returns></returns>
    private Gesture[] LoadTrainingSet()
    {
        List<Gesture> gestures = new List<Gesture>();
        UnityEngine.Object[] xmlFiles = Resources.LoadAll("CharacterGestures", typeof(TextAsset));

        foreach (TextAsset textAsset in xmlFiles)
        {
            gestures.Add(ReadGesture(textAsset));
        }

        return gestures.ToArray();
    }

    /// <summary>
    /// Reads a multistroke gesture from an XML file
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static Gesture ReadGesture(TextAsset xmlFile)
    {
        MemoryStream assetStream = new MemoryStream(xmlFile.bytes);
        XmlReader xmlReader = XmlReader.Create(assetStream);
        List<Point> xmlPoints = new List<Point>();
        int currentStrokeIndex = -1;
        string gestureName = "";
        
        try
        {
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType != XmlNodeType.Element) continue;
                switch (xmlReader.Name)
                {
                    case "Gesture":
                        gestureName = xmlReader["Name"];
                        if (gestureName.Contains("~")) // '~' character is specific to the naming convention of the MMG set
                            gestureName = gestureName.Substring(0, gestureName.LastIndexOf('~'));
                        if (gestureName.Contains("_")) // '_' character is specific to the naming convention of the MMG set
                            gestureName = gestureName.Replace('_', ' ');
                        break;
                    case "Stroke":
                        currentStrokeIndex++;
                        break;
                    case "Point":
                        xmlPoints.Add(new Point(
                            float.Parse(xmlReader["X"]),
                            -float.Parse(xmlReader["Y"]),
                            currentStrokeIndex
                        ));
                        break;
                }
            }
        }
        finally
        {
            if (xmlReader != null)
                xmlReader.Close();
        }

        return new Gesture(xmlPoints.ToArray(), gestureName);
    }
}
