using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetScore(string score)
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = score;
    }
}
