using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    private GameObject au;
    private GameObject line;
    private LineRenderer lr;
    private float h_mass = 6.6464731e-27f; // Helium atom mass in kg
    //private float au_mass = 3.2707137e-25f; // Gold atom mass in kg
    private float au_radius = 24.5f; // Gold atom diameter in fm
    //private float h_diameter = 1e-15f; // Helium atom diameter in m
    private float el = 1.602176634e-19f; // Elementary charge in C
    //private float cc = 8.9875517923e9f; // Coulomb constant in Nm^2/C^2
    private float perm = 8.8541878128e-12f; // Permittivity of free space in F/m
    private float vxInit = 3e7f; // Initial x velocity in m/s
    private float xInit = -24.5f*5; // Initial x position of alpha particle in fm
    private float yInit = 0; // Initial x position of alpha particle in femtometer (1e-15 m)
    //private float Time_Sampling = 1e-23f; // Time sampling in s

    public int iterations = 300; // Number of iterations

    // Start is called before the first frame update
    void Start()
    {
        au = GameObject.Find("Gold");
        line = new GameObject("Line");
        line.transform.parent = transform;
        line.AddComponent<LineRenderer>();
        lr = line.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.startWidth = 2f;
        lr.endWidth = 2f;
        lr.positionCount = 100;
        line.transform.position = new Vector2(xInit, yInit);

        nextLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void nextLine()
    {
        float K = 79f * el * 2f * el / (4f * Mathf.PI * perm); // Electical force calculation constant for gold
        float[] aX = new float[iterations];
        float[] aY = new float[iterations];
        aX[0] = 0f;
        aY[0] = 0f;
        float[] vX = new float[iterations];
        float[] vY = new float[iterations];
        vX[0] = vxInit;
        vY[0] = 0f;
        float[] x = new float[iterations];
        float[] y = new float[iterations];
        x[0] = xInit;
        y[0] = yInit;

        for (int i = 1; i<iterations; i++) //Error on distance calculation
        {
            float distance = Vector2.Distance(lr.GetPosition(i - 1) + line.transform.position, au.transform.position); // distance from gold atom in fm
            float distX = (lr.GetPosition(i - 1).x + line.transform.position.x); // x distance from gold atom in fm
            float distY = (lr.GetPosition(i - 1).y + line.transform.position.y); // y distance from gold atom in fm

            if (distance >= au_radius)
            {
                aX[i - 1] = K * distX / (h_mass * Mathf.Pow(distance, 3)); // update x acceleration
                aY[i - 1] = K * distY / (h_mass * Mathf.Pow(distance, 3)); // update y acceleration
            }
            else {
                Debug.Log("Collision at " + i);
                aX[i - 1] = K * distX / (h_mass * Mathf.Pow(au_radius, 3)); // update x acceleration
                aY[i - 1] = K * distY / (h_mass * Mathf.Pow(au_radius, 3)); // update y acceleration
            }

            vX[i] += aX[i - 1] * Time.deltaTime;
            vY[i] += aY[i - 1] * Time.deltaTime;
            x[i] += vX[i - 1] * Time.deltaTime + 0.5f*aX[i - 1] *Mathf.Pow(Time.deltaTime, 2); // update x position
            y[i] += vY[i - 1] * Time.deltaTime + 0.5f*aY[i - 1] *Mathf.Pow(Time.deltaTime, 2); // update y position

            Debug.Log("Distance: " + distX + ", " + distY);
            Debug.Log("Acceleration: " + aX[i - 1] + ", " + aY[i - 1]);
            Debug.Log("Velocity: " + vX[i - 1] + ", " + vY[i - 1]);
            Debug.Log("Position: " + x[i - 1] + ", " + y[i - 1]);
            Debug.Log("Time: " + Time.deltaTime);
            Debug.Log(" ");
        }
        /*for(int i = 0; i < x.Length; i++)
        {
            Debug.Log(i+": "+x[i]);
        }*/
    }
}
