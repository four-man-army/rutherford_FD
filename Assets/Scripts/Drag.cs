using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    

    private void Update()
    {
        if (dragging)
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
            transform.position = currentPosition;
        }
    }

    private void OnMouseDown()
    {
        dragging = true;
        Debug.Log("Started Dragging");
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    }

    private void OnMouseUp()
    {
        dragging = false;
        Debug.Log("Stopped Dragging");
    }
}
