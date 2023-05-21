using UnityEngine;

public class GenPath : MonoBehaviour
{
    private GameObject line;
    private LineRenderer lr;
    private float he_mass = 6.6464731e-27f; // Helium atom mass in kg
    private float au_radius = 2.45e-14f; // Gold atom diameter in m
    private float el = 1.602176634e-19f; // Elementary charge in C
    private float cc = 8.9875517923e9f; // Coulomb constant in Nm^2/C^2
    private float vxInit = 2e7f; // Initial x velocity in m/s
    private float xInit; // Initial x position of alpha particle in fm
    private float yInit; // Initial y position of alpha particle in femtometer (1e-15 m)
    private float Time_Sampling = 1e-22f; // Time sampling in s (0.00001s)
    private int iterations = 500; // Number of iterations (Steps in Time_Sampling)

    public void calc(float y_init, float gap, int iteration)
    {
        iterations = iteration;
        xInit = -au_radius * 15f; // Initial x position of alpha particle in fm
        yInit = au_radius * gap; // Initial y position of alpha particle in femtometer (1e-15 m)
        Color[] color = { Color.red, Color.blue, Color.green, Color.magenta };
        int colorNum = new System.Random().Next(0, 4);
        line = new GameObject("Line");
        line.tag = "line";
        line.transform.parent = transform;
        line.AddComponent<LineRenderer>();
        lr = line.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = color[colorNum];
        lr.endColor = color[colorNum];
        lr.startWidth = 2f;
        lr.endWidth = 2f;
        lr.positionCount = iterations;
        line.transform.position = new Vector2(xInit, yInit);

        float K = 79f * el * 2f * el;            // Electrical force 
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
        y[0] = y_init * yInit;

        for (int i = 1; i<iterations; i++)
        {
            float distance = (Mathf.Sqrt((0 - x[i - 1]) * (0 - x[i - 1]) + (0 - y[i - 1]) * (0 - y[i - 1]))); // distance from gold atom in fm

            if (y[0] == 0)
            {
                if (distance >= au_radius)
                {
                    aX[i - 1] = (cc * (K / (distance * distance))) / he_mass; // update x acceleration
                    aY[i - 1] = 0; // update y acceleration
                }
                else
                {
                    aX[i - 1] = (cc * (K / (au_radius * au_radius))) / he_mass; // update x acceleration
                    aY[i - 1] = 0; // update y acceleration
                }

                vX[i] = (vX[i - 1] - aX[i - 1] * Time_Sampling); // update x velocity
                vY[i] = (vY[i - 1] + aY[i - 1] * Time_Sampling); // update y velocity

                if (y[0] < 0)
                {
                    aY[i - 1] = -aY[i - 1];
                    vY[i - 1] = -vY[i - 1];
                }

                x[i] = 0.5f * aX[i - 1] * Time_Sampling * Time_Sampling + vX[i - 1] * Time_Sampling + x[i - 1]; // update x position
                y[i] = 0.5f * aY[i - 1] * Time_Sampling * Time_Sampling + vY[i - 1] * Time_Sampling + y[i - 1]; // update y position
            }
            else { 
                if (distance >= au_radius)
                {
                    aX[i - 1] = (cc * (K/ (distance * distance))) / he_mass; // update x acceleration
                    aY[i - 1] = (cc * (K / (distance * distance))) / he_mass; // update y acceleration
                }
                else {
                    aX[i - 1] = (cc * (K / (au_radius * au_radius))) / he_mass; // update x acceleration
                    aY[i - 1] = (cc * (K / (au_radius * au_radius))) / he_mass; // update y acceleration
                }

                vX[i] = (vX[i - 1] + aX[i - 1] * Time_Sampling); // update x velocity
                vY[i] = (vY[i - 1] + aY[i - 1] * Time_Sampling); // update y velocity

                if (y[0] < 0)
                {
                    aY[i - 1] = -aY[i - 1];
                    vY[i - 1] = -vY[i - 1];
                }
            
                x[i] = 0.5f * aX[i-1] * Time_Sampling * Time_Sampling + vX[i - 1] * Time_Sampling + x[i - 1]; // update x position
                y[i] = 0.5f * aY[i-1] * Time_Sampling * Time_Sampling + vY[i - 1] * Time_Sampling + y[i - 1]; // update y position
            }
        }

        float[] newX = new float[x.Length];
        float[] newY = new float[y.Length];
        for (int i = 0; i < x.Length; i++)
        {
            newX[i] = x[i] * 1e15f; // convert to world coordinate
            newY[i] = y[i] * 1e15f;// convert to coordinate
        }
        for(int i = 0; i < newX.Length; i++)
        {
            lr.SetPosition(i, new Vector3(newX[i], newY[i], 1)); // set position of lines
        }
    }
    
    public void calc2(float y_init, float gap)
    {
        xInit = -au_radius * 15f; // Initial x position of alpha particle in fm
        yInit = au_radius * gap; // Initial y position of alpha particle in femtometer (1e-15 m)
        Color[] color = { Color.red, Color.blue, Color.green, Color.magenta };
        int colorNum = new System.Random().Next(0, 4);
        line = new GameObject("Line");
        line.tag = "line";
        line.transform.parent = transform;
        line.AddComponent<LineRenderer>();
        lr = line.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = color[colorNum];
        lr.endColor = color[colorNum];
        lr.startWidth = 2f;
        lr.endWidth = 2f;
        lr.positionCount = iterations;
        line.transform.position = new Vector2(xInit, yInit);

        float K = 79f * el * 2f * el;            // Electrical force 
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
        y[0] = y_init * yInit;

        for (int i = 1; i < iterations; i++)
        {
            float distance = (Mathf.Sqrt((0 - x[i - 1]) * (0 - x[i - 1]) + (0 - y[i - 1]) * (0 - y[i - 1]))); // distance from gold atom in fm

            if (y[0] == 0)
            {
                if (distance >= au_radius)
                {
                    aX[i - 1] = (cc * (K / (distance * distance))) / he_mass; // update x acceleration
                    aY[i - 1] = 0; // update y acceleration
                }
                else
                {
                    aX[i - 1] = (cc * (K / (au_radius * au_radius))) / he_mass; // update x acceleration
                    aY[i - 1] = 0; // update y acceleration
                }

                vX[i] = (vX[i - 1] - aX[i - 1] * Time_Sampling); // update x velocity
                vY[i] = (vY[i - 1] + aY[i - 1] * Time_Sampling); // update y velocity

                if (y[0] < 0)
                {
                    aY[i - 1] = -aY[i - 1];
                    vY[i - 1] = -vY[i - 1];
                }

                x[i] = 0.5f * aX[i - 1] * Time_Sampling * Time_Sampling + vX[i - 1] * Time_Sampling + x[i - 1]; // update x position
                y[i] = 0.5f * aY[i - 1] * Time_Sampling * Time_Sampling + vY[i - 1] * Time_Sampling + y[i - 1]; // update y position
            }
            else
            {
                if (distance >= au_radius)
                {
                    aX[i - 1] = (cc * (K / (distance * distance))) / he_mass; // update x acceleration
                    aY[i - 1] = (cc * (K / (distance * distance))) / he_mass; // update y acceleration
                }
                else
                {
                    aX[i - 1] = (cc * (K / (au_radius * au_radius))) / he_mass; // update x acceleration
                    aY[i - 1] = (cc * (K / (au_radius * au_radius))) / he_mass; // update y acceleration
                }

                vX[i] = (vX[i - 1] + aX[i - 1] * Time_Sampling); // update x velocity
                vY[i] = (vY[i - 1] + aY[i - 1] * Time_Sampling); // update y velocity

                if (y[0] < 0)
                {
                    aY[i - 1] = -aY[i - 1];
                    vY[i - 1] = -vY[i - 1];
                }

                x[i] = 0.5f * aX[i - 1] * Time_Sampling * Time_Sampling + vX[i - 1] * Time_Sampling + x[i - 1]; // update x position
                y[i] = 0.5f * aY[i - 1] * Time_Sampling * Time_Sampling + vY[i - 1] * Time_Sampling + y[i - 1]; // update y position
            }
        }

        float[] newX = new float[x.Length];
        float[] newY = new float[y.Length];
        for (int i = 0; i < x.Length; i++)
        {
            newX[i] = x[i] * 1e15f;
            newY[i] = y[i] * 1e15f;
        }
        for (int i = 0; i < newX.Length; i++)
        {
            lr.SetPosition(i, new Vector3(newX[i], newY[i], 1));
        }
    }
}
