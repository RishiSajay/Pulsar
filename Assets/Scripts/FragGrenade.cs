using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FragGrenade : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Rigidbody2D fragGrenadeShard;

    float speed = 5f;
    float rotateSpeed = 200f;

    public GameObject shard;

    public GameObject explosionEffect;

    public AudioSource myAudioSource;
    public AudioClip fragShotSound;
    public AudioClip fragBombExplosionSound;


    // Start is called before the first frame update
    void Start()
    {
        fragGrenadeShard = shard.GetComponent<Rigidbody2D>();
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(FragNotHit());

        AudioSource.PlayClipAtPoint(fragShotSound, new Vector3(0f, 0f, -10f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.angularVelocity = rotateSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3")
        {
            //myAudioSource.clip = fragBombExplosionSound;
            BlowUp();
        }
    }

    void BlowUp()
    {
        AudioSource.PlayClipAtPoint(fragBombExplosionSound, new Vector3(0f, 0f, -10f));
        StartCoroutine(ExplodeFrag());

    }

    IEnumerator ExplodeFrag()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject newExplosion = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(newExplosion, 2);
        CreateShardsAroundPoint(20, 5f, 2f);
    }
    void CreateShardsAroundPoint(int num, float shardSpeed, float radius)
    {
        float nextAngle = 2 * Mathf.PI / num;
        float angle = 0;

        for (int i = 0; i < num; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Rigidbody2D newShard = Instantiate(fragGrenadeShard, transform.position, Quaternion.identity);
            newShard.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            //newShard.bodyType = RigidbodyType2D.Kinematic;
            newShard.velocity = new Vector3(x, y, 0f) * shardSpeed;

            angle += nextAngle;
        }
    }

    IEnumerator FragNotHit()
    {
        yield return new WaitForSeconds(2); //wait 5 seconds
        BlowUp();
    }
}
