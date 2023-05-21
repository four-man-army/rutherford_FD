using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFields : MonoBehaviour
{
    private TextMeshProUGUI screenSmsg;
    private DrawPath DrawPath;
    private float time = 0;
    private bool screenshot = false;
    private bool screenshotMsg = true;
    private string screenshotPath = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "\\Pictures\\RFC_Screenshot.png";

    // Start is called before the first frame update
    void Start()
    {
        DrawPath = GameObject.Find("Line Origin").GetComponent<DrawPath>();
        screenSmsg = GameObject.Find("Screenshot").GetComponent<TextMeshProUGUI>();
        screenSmsg.text = "Press F2 to screenshot.";
    }

    // Update is called once per frame
    void Update()
    {
        if (screenshotMsg) { 
            if (Time.time >= 2)
            {
                screenSmsg.text = "";
                screenshotMsg = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F2)) {
            screenSmsg.text = "Screenshot saved under\n" + screenshotPath;
            time = Time.time;
            screenshot = true;
        }

        if (screenshot)
        {
            if (Time.time - time >= 3){
                screenSmsg.text = "";
                screenshot = false;
                ScreenCapture.CaptureScreenshot(screenshotPath);
            }
        }
    }

    public void setGap()
    {
        DrawPath.gap = float.Parse(GameObject.Find("IF_Gap/Text").GetComponent<Text>().text);
    }

    public void setIteration()
    {
        DrawPath.iterations = int.Parse(GameObject.Find("IF_Iteration/Text").GetComponent<Text>().text);
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
