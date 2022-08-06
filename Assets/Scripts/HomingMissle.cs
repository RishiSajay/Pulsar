using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissle : MonoBehaviour
{
    GameObject player1;
    GameObject player2;
    GameObject player3;

    private Transform target;
    private Rigidbody2D rb2D;
    float speedBefore = 6f;
    float speedAfter = 8f;
    float rotateSpeed = 400f;
    bool noTarget = true;

    public GameObject explosionEffect;

    public GameObject missileAfterEffect;

    public AudioSource myAudioSource;
    public AudioClip missileShotSound;
    public AudioClip missileExplosionSound;

    // Start is called before the first frame update
    void Start()
    {
        //have to get closest target later on
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player3 = GameObject.FindGameObjectWithTag("Player3");

        //target = player1.transform;
        StartCoroutine(FindClosestTarget());
        rb2D = GetComponent<Rigidbody2D>();

        AudioSource.PlayClipAtPoint(missileShotSound, new Vector3(0f, 0f, -10f), 0.8f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (noTarget == true)
        {
            rb2D.velocity = transform.up * speedBefore;
        }
        else
        {
            if (target != null)
            {
                Vector2 direction = (Vector2)target.position - rb2D.position;
                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.up).z;

                rb2D.angularVelocity = -rotateAmount * rotateSpeed;
                rb2D.velocity = transform.up * speedAfter;
            }
        }
    }

    IEnumerator FindClosestTarget()
    {
        yield return new WaitForSeconds(0.75f); //wait 0.75 seconds

        //find all distances
        float distPlayer1;
        float distPlayer2;
        float distPlayer3;
        var myList = new List<KeyValuePair<string, float>>();
        if (this.player1 != null)
        {
            distPlayer1 = Vector2.Distance(player1.transform.position, gameObject.transform.position);
            myList.Add(new KeyValuePair<string, float>("player1", distPlayer1));
        }
        if (this.player2 != null)
        {
            distPlayer2 = Vector2.Distance(player2.transform.position, gameObject.transform.position);
            myList.Add(new KeyValuePair<string, float>("player2", distPlayer2));
        }
        if (this.player3 != null)
        {
            distPlayer3 = Vector2.Distance(player3.transform.position, gameObject.transform.position);
            myList.Add(new KeyValuePair<string, float>("player3", distPlayer3));
        }

        //find closest target
        if (myList.Count == 0)
        {
            Debug.Log("no target");
            this.noTarget = true;
        }
        else
        {
            
            string closestPlayer = FindClosest(myList);
            if (closestPlayer.Equals("player1"))
            {
                this.target = this.player1.transform;
                //Debug.Log("target: player1");
            }
            else if (closestPlayer.Equals("player2"))
            {
                this.target = this.player2.transform;
                //Debug.Log("target: player2");
            }
            else if (closestPlayer.Equals("player3"))
            {
                this.target = this.player3.transform;
                //Debug.Log("target: player3");
            }
            this.noTarget = false;
        }

        /*
        if (this.player1 == null && this.player2 == null && this.player3 == null)
        {
            this.noTarget = true;
        }

        else if(this.player1 == null)
        {
            this.target = this.player2.transform;
            this.noTarget = false;
        }

        else if(this.player2 == null)
        {
            this.target = this.player1.transform;
            this.noTarget = false;
        }
        else
        {
            float distPlayer1 = Vector2.Distance(player1.transform.position, gameObject.transform.position);
            float distPlayer2 = Vector2.Distance(player2.transform.position, gameObject.transform.position);
            //Debug.Log("distance to player 1: " + distPlayer1);
            //Debug.Log("distance to player 2: " + distPlayer2);

            if (distPlayer1 < distPlayer2)
            {
                target = player1.transform;
                //Debug.Log("target: player1");
            }
            else
            {
                target = player2.transform;
                //Debug.Log("target: player2");
            }
            this.noTarget = false;
        }*/
    }

    string FindClosest(List<KeyValuePair<string, float>> list)
    {
        string closest = list[0].Key;
        float smallestDistance = list[0].Value;
        foreach (var val in list)
        {
            if (val.Value < smallestDistance)
            {
                smallestDistance = val.Value;
                closest = val.Key;
            }
        }
        return closest;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "fragGrenadeShard")
        {
            //myAudioSource.clip = missileExplosionSound;
            AudioSource.PlayClipAtPoint(missileExplosionSound, new Vector3(0f, 0f, -10f), 0.6f);

            GameObject newExplosion = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(newExplosion, 2);

            GameObject afterExplosion = (GameObject)Instantiate(missileAfterEffect, transform.position, transform.rotation);
            Destroy(afterExplosion, 0.1f);

        }
    }
}
