using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicBullet : MonoBehaviour
{
    float bulletForce = 1.0f;

    public AudioSource myAudioSource;
    public AudioClip bulletShotSound;
    public AudioClip bulletHitSound;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.5f);

        //myAudioSource.clip = bulletShotSound;
        AudioSource.PlayClipAtPoint(bulletShotSound, new Vector3(0f, 0f, -10f), 0.4f);

    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3")
        {
            AudioSource.PlayClipAtPoint(bulletHitSound, new Vector3(0f, 0f, -10f), 0.4f);
        }
    }

}
