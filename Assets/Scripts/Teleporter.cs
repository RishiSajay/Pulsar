using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Teleporter : MonoBehaviour
{
    public GameObject teleportLocation;

    void Start()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Walls" && col.gameObject.tag != "Asteroid")
        {
            col.gameObject.transform.position = teleportLocation.gameObject.transform.position;
        }
            
    }

}
