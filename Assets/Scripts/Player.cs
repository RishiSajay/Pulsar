using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //player movement variables
    float forwardThrust = 400.0f;
    float backwardThrust = -200.0f;
    float maxVelForward = 800.0f;
    float maxVelBackward = -300.0f;

    float rotationSpeed = 200.0f;

    /// <summary>
    /// projectile variables
    /// </summary>
    
    //main gun
    public GameObject MainGun;
    List<Rigidbody2D> allBullets = new List<Rigidbody2D>();
    int countOfBullets = 0;
    public Rigidbody2D bullet;
    float bulletSpeed = 10.0f;

    //laser gun
    public static bool laserActive = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb2D = gameObject.GetComponent<Rigidbody2D>();

        //rotate
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        //move
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //rb2D.velocity = new Vector3(0, forwardSpeed, 0);
            rb2D.AddForce(transform.up * forwardThrust * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //rb2D.velocity = new Vector3(0, backwardSpeed, 0);
            rb2D.AddForce(transform.up * backwardThrust * Time.deltaTime);
        }

        //clamp velocity
        var vel = rb2D.velocity;
        float magnitude = rb2D.velocity.sqrMagnitude;
        if (vel.sqrMagnitude >= maxVelForward)
        {
            rb2D.velocity = vel.normalized * maxVelForward;
        }

        //shoot basic bullet
        if (Input.GetKeyDown(KeyCode.Space) && (allBullets.Count < 5))
        {
            Rigidbody2D bulletClone = Instantiate(bullet, MainGun.transform.position, transform.rotation);
            bulletClone.velocity = transform.up * bulletSpeed;
            allBullets.Add(bulletClone);

        }
        //remove old bullets
        for (int i = 0; i < allBullets.Count; i++)
        {
            if (allBullets[i] == null)
            {
                allBullets.RemoveAt(i);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            //hit wall, have oppsite movement to bounce off, or bouncy material
            transform.Translate(0, 0, 0);
        }

        //power ups
        if (collision.gameObject.tag == "power")
        {
            Destroy(collision.gameObject);
        }

        //basicBullets
        if (collision.gameObject.tag == "basicBullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
