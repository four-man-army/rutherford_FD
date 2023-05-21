using TMPro;
using UnityEngine;

public class renderInfo : MonoBehaviour
{
    TextMeshProUGUI itLabel;
    DrawPath dp;

    // Start is called before the first frame update
    void Start()
    {
        
        itLabel = GameObject.Find("Label_General/Label_Iteration").GetComponent<TextMeshProUGUI>();
        updateGeneral();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateGeneral()
    {
        dp = gameObject.AddComponent<DrawPath>();
        itLabel.text = "Iteration (Steps): " + dp.iterations.ToString();
    }
}
