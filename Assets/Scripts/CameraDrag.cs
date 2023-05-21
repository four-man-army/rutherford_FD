using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private float dragSpeed = 500;
    private Vector3 dragOrigin;
    private Vector3 camOrigin;


    void Start()
    {
        camOrigin = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            transform.position = camOrigin;
            return;
        }

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-(pos.x * dragSpeed), -(pos.y * dragSpeed), 0);

        transform.position = move;
    }


}

/*
public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            // Move object, taking into account original offset.
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        // Record the difference between the objects centre, and the clicked point on the camera plane.
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        // Stop dragging.
        dragging = false;
    }
}*/