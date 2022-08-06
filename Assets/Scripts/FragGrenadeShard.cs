using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenadeShard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float time = Random.Range(1.5f, 3f);
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
