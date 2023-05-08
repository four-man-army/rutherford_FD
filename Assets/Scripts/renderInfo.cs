using TMPro;
using UnityEngine;

public class renderInfo : MonoBehaviour
{
    TextMeshProUGUI posLabel;
    TextMeshProUGUI distLabel;
    GameObject gold;

    // Start is called before the first frame update
    void Start()
    {
        posLabel = GameObject.Find("Label_Helium/Label_Position").GetComponent<TextMeshProUGUI>();
        distLabel = GameObject.Find("Label_Helium/Label_Distance").GetComponent<TextMeshProUGUI>();
        gold = GameObject.Find("Gold"); 
    }

    // Update is called once per frame
    void Update()
    {
        getPos();
        calcDistance();
    }

    private void calcDistance()
    {
        float distance = Vector2.Distance(transform.position, gold.transform.position);
        distLabel.text = "Distance: " + distance*1e-15 + "m";
    }

    private void getPos()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);
        posLabel.text = "Position: (" + x + " | " + y + ")";
    }
}
