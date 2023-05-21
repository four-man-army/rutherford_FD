using UnityEngine;


public class CameraDrag : MonoBehaviour
{
    private float dragSpeed = 1000;
    private Vector3 dragOrigin;
    private Vector3 camOrigin;

    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3f;

    void Start()
    {
        camOrigin = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            Cursor.visible = true;
            transform.position = Vector3.SmoothDamp(transform.position, camOrigin, ref velocity, smoothTime);
            return;
        }

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-(pos.x * dragSpeed), -(pos.y * dragSpeed), 0);

        transform.position = new Vector3(Mathf.Clamp(move.x, -600, 1500), Mathf.Clamp(move.y, -500, 500), 0);
    }

}