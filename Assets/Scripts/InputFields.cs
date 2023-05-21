using UnityEngine;
using UnityEngine.UI;

public class InputFields : MonoBehaviour
{
    private Text IF_gap, IF_iteration;
    private DrawPath DrawPath;

    // Start is called before the first frame update
    void Start()
    {
        IF_gap = GameObject.Find("IF_Gap/Text").GetComponent<Text>();
        IF_iteration = GameObject.Find("IF_Iteration/Text").GetComponent<Text>();

        DrawPath = GameObject.Find("Line Origin").GetComponent<DrawPath>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGap()
    {
        DrawPath.gap = float.Parse(IF_gap.text);
    }

    public void setIteration()
    {
        DrawPath.iterations = int.Parse(IF_iteration.text);
    }

    public void setLineStart()
    {
        DrawPath.lineStart = int.Parse(GameObject.Find("IF_LineStart/Text").GetComponent<Text>().text);
    }

    public void setLineEnd()
    {
        DrawPath.lineEnd = int.Parse(GameObject.Find("IF_LineEnd/Text").GetComponent<Text>().text);
    }
}
