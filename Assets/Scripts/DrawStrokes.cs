using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

public class DrawStrokes : MonoBehaviour, IDragHandler, IDropHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject linePrefab;
    
    private GameObject currentLine;
    private UILineRenderer lineRenderer;
    private List<UILineRenderer> lines = new List<UILineRenderer>();
    private List<List<Vector2>> Writings = new List<List<Vector2>>();
    private List<Vector2> points = new List<Vector2>();
    private int CurrentLine = 0;
    private Vector2 rectPos;

    private void OnEnable()
    {
        Writings.Clear();
        points.Clear();
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
    }

    public void OnDrag(PointerEventData eventData)
    {
        points.Add(new Vector2(eventData.position.x - rectPos.x, eventData.position.y - rectPos.y));
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
        List<Vector2> tmpList = new List<Vector2>();
        points = new List<Vector2>();
        points.Add(new Vector2(eventData.position.x - rectPos.x, eventData.position.y - rectPos.y));
        // points.Add(new Vector2(eventData.position.x - rectPos.x, eventData.position.y - rectPos.y));
        RefreshLine();
        //CurrentLine = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (lineRenderer.Points.Length > 3)
        {
            Writings.Add(lineRenderer.Points.ToList());
            lines.Add(lineRenderer);
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
        if (Writings.Count == 3)
        {
            
        }
        if (Writings.Count == 4)
        {
            if (Huo.Recognizer(Writings))
            {
                foreach (var line in lines)
                {
                    line.color = Color.green;
                }
                MagicHand.Instance.Activate("Huo");
            }
            if (Shui.Recognizer(Writings)) print("SHUI");
        }
    }
}
