using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    private GenPath GenPath;
    private renderInfo renderInfo;
    private GameObject[] oldLines;

    public float gap = 0.5f;
    public int iterations = 500;
    public int lineStart = -10;
    public int lineEnd = 10;

    // Start is called before the first frame update
    void Start()
    {
        GenPath = gameObject.AddComponent<GenPath>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void draw()
    {
        oldLines = GameObject.FindGameObjectsWithTag("line");
        if (oldLines != null)
        {
            foreach (var line in oldLines)
            {
                Destroy(line);
            }
        }

        for (int i = lineStart; i < lineEnd+1; i++)
        {
            GenPath.calc(i * 0.5f, gap, iterations);
        }
    }
}
