using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class genLine : MonoBehaviour
{
    GameObject line;
    LineRenderer lr;
    GameObject au;
    Vector3[] pos;

    private void Start()
    {
        /*line.transform.parent = transform;
        line.AddComponent<LineRenderer>();
        lr = line.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.black;
        lr.endColor = Color.black;
        lr.startWidth = 2f;
        lr.endWidth = 2f;
        lr.positionCount = 100;
        line.transform.parent.position = new Vector2(-2000, 0);
        Debug.Log("Start done");*/
    }
    private void Update()
    {
    }

    void newLine(){
        lr.SetPosition(0, Input.mousePosition);
        float distance_line = Vector2.Distance(line.transform.position, au.transform.position);
        float distance_lr = Vector2.Distance(lr.GetPosition(0) + line.transform.position, au.transform.position);
        Debug.Log("Distance line: " + distance_line);
        Debug.Log("Distance lr: " + distance_lr);
    }

    public void getLine()
    {
        
        lr = GameObject.Find("Line Origin/Line").GetComponent<LineRenderer>();
        for(int i = 0; i < lr.positionCount; i++)
        {
            pos[i] = lr.GetPosition(i);
        }
        Debug.Log(pos.Length);
    }
}
