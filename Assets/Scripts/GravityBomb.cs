using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBomb : MonoBehaviour
{

    public GameObject blackhole;
    public GameObject explosionEffect;

    private Rigidbody2D rb2D;

    float rotateSpeed = 200f;

    public AudioSource myAudioSource;
    public AudioClip gravityShotSound;
    public AudioClip gravityBombExplosionSound;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(BombNotHit());
        AudioSource.PlayClipAtPoint(gravityShotSound, new Vector3(0f, 0f, -10f));
    }

    void FixedUpdate()
    {
        rb2D.angularVelocity = rotateSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3")
        {
            //myAudioSource.clip = gravityBombExplosionSound;
            BlowUp();
        }
    }

    void BlowUp()
    {
        AudioSource.PlayClipAtPoint(gravityBombExplosionSound, new Vector3(0f, 0f, -10f));

        GameObject newExplosion = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(newExplosion, 2);
        CreateBlackHole();
    }

    void CreateBlackHole()
    {
        GameObject myBlackHole = (GameObject)Instantiate(blackhole, transform.position, transform.rotation);
        myBlackHole.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    IEnumerator BombNotHit()
    {
        yield return new WaitForSeconds(2); //wait 5 seconds
        BlowUp();
    }
}
