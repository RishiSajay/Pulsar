using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDescription : MonoBehaviour
{
    //public TextMeshProUGUI myText;

    void Start()
    {
        //GetComponent<TMPro.TextMeshProUGUI>().text;
    }

    public void SetText(string weaponDescription)
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = weaponDescription;
    }
}
