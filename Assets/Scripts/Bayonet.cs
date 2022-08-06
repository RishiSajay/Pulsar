using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bayonet : MonoBehaviour
{
    public GameObject fireEffect;

    public AudioSource myAudioSource;
    public AudioClip bayonetSound;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4);
        GameObject newfireEffect = (GameObject)Instantiate(fireEffect, transform.position, Quaternion.identity);
        newfireEffect.transform.parent = this.transform;
        newfireEffect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        Destroy(newfireEffect, 4);

        myAudioSource.clip = bayonetSound;
        myAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Walls" && collider.gameObject.tag != "Player1" && collider.gameObject.tag != "Player2" && collider.gameObject.tag != "Player3" && collider.gameObject.tag != "fragPowerUp"
            && collider.gameObject.tag != "missilePowerUp" && collider.gameObject.tag != "laserPowerUp" && collider.gameObject.tag != "bayonetPowerUp" 
            && collider.gameObject.tag != "gravityPowerUp" && collider.gameObject.tag != "boundaryShip" && collider.gameObject.tag != "blackHole")
        {
            Destroy(collider.gameObject);
        }
    }
}
