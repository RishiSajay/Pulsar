using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{

    //game manager
    public GameManager2Player gameManager;
    public GameManager3Player gameManager3P;

    //health
    float maxHealth = 100f;
    float currentHealth;
    public HealthBar healthBar;

    //weapon description
    public WeaponDescription weaponDescription;

    //player movement variables
    float forwardThrust = 400.0f;
    float backwardThrust = -200.0f;
    float maxVelForward = 4.5f;
    float maxVelBackward = -3.0f;
    float rotationSpeed = 250.0f;

    /// <summary>
    /// projectile variables
    /// </summary>
    bool hasPowerUp = false;

    //main gun
    public GameObject MainGun;
    List<Rigidbody2D> allBullets = new List<Rigidbody2D>();
    int maxNumBullets = 2;
    public Rigidbody2D bullet;
    float bulletSpeed = 10.0f;
    float bulletDamage = 25.0f;

    //laser gun
    bool laserActive = false;
    public static bool laserIsShot = false;
    bool laserSoundUsed = false;

    //homing missile
    public GameObject HomingMissileGun;
    bool homingMissileActive = false;
    public Rigidbody2D homingMissile;

    //frag grenade
    public GameObject FragGrenadeGun;
    bool fragGrenadeActive = false;
    public Rigidbody2D fragGrenade;
    float fragGrenadeSpeed = 10.0f;

    //bayonet
    bool bayonetActive = false;
    public GameObject bayonet;


    //gravity bomb
    bool gravityBombActive = false;
    public GameObject gravityBomb;
    float gravityBombSpeed = 10.0f;

    //on death
    public GameObject explosionEffect;

    //sounds
    public AudioSource myAudioSource;

    public AudioClip laserShotSound;
    public AudioClip playerDeathSound;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb2D = gameObject.GetComponent<Rigidbody2D>();

        //rotate
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        //move
        if (Input.GetKey(KeyCode.W))
        {
            //rb2D.velocity = new Vector3(0, forwardSpeed, 0);
            rb2D.AddForce(transform.up * forwardThrust * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //rb2D.velocity = new Vector3(0, backwardSpeed, 0);
            rb2D.AddForce(transform.up * backwardThrust * Time.deltaTime);
        }

        //clamp velocity
        var vel = rb2D.velocity;
        float magnitude = rb2D.velocity.magnitude;
        //Debug.Log(magnitude);
        if (vel.magnitude >= maxVelForward)
        {
            rb2D.velocity = vel.normalized * maxVelForward;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //all shooting mechanics
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //shoot button pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //laser active
            if (laserActive == true)
            {
                float laserDuration = 2.1f;

                laserIsShot = true;
                if (laserSoundUsed == false)
                {
                    AudioSource.PlayClipAtPoint(laserShotSound, new Vector3(0f, 0f, -10f), 0.8f);
                    laserSoundUsed = true;
                }
                StartCoroutine(SetFalse(laserDuration));
            }

            //homing missile active
            else if (homingMissileActive == true)
            {
                Rigidbody2D homingMissileClone = Instantiate(homingMissile, HomingMissileGun.transform.position, transform.rotation);
                homingMissileClone.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                homingMissileActive = false;
                hasPowerUp = false;
            }

            //frag grenade active
            else if (fragGrenadeActive == true)
            {
                Rigidbody2D fragGrenadeClone = Instantiate(fragGrenade, FragGrenadeGun.transform.position, transform.rotation);
                fragGrenadeClone.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                fragGrenadeClone.velocity = transform.up * fragGrenadeSpeed;
                fragGrenadeActive = false;
                hasPowerUp = false;
            }

            //bayonet active
            else if (bayonetActive == true)
            {
                GameObject bayonetClone = Instantiate(bayonet, FragGrenadeGun.transform.position, transform.rotation);
                bayonetClone.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                bayonetClone.transform.parent = gameObject.transform;
                StartCoroutine(SetFalse2());
            }

            //gravity bomb active
            else if (gravityBombActive == true)
            {
                GameObject gravityBombClone = Instantiate(gravityBomb, FragGrenadeGun.transform.position, transform.rotation);
                gravityBombClone.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                gravityBombClone.GetComponent<Rigidbody2D>().velocity = transform.up * gravityBombSpeed;
                gravityBombActive = false;
                hasPowerUp = false;
            }

            //shoot basic bullet
            else if (allBullets.Count < maxNumBullets && hasPowerUp == false)
            {
                Rigidbody2D bulletClone = Instantiate(bullet, MainGun.transform.position, transform.rotation);
                bulletClone.velocity = transform.up * bulletSpeed;
                allBullets.Add(bulletClone);
            }
        }

        //reset weapon text
        if (hasPowerUp == false)
        {
            weaponDescription.SetText("Main Gun");
        }

        //remove old basic bullets
        for (int i = 0; i < allBullets.Count; i++)
        {
            if (allBullets[i] == null)
            {
                allBullets.RemoveAt(i);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //on death
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }

    }

    //time for laser
    IEnumerator SetFalse(float duration)
    {
        yield return new WaitForSeconds(duration); //wait 2 seconds
        laserActive = false;
        laserIsShot = false;
        laserSoundUsed = false;
        hasPowerUp = false;
    }

    //fade laser sound
    public static IEnumerator Fade(AudioSource audioSource, float duration, float targetVolume)
    {
        yield return new WaitForSeconds(1.5f);
        //float currentTime = 1.6f;
        float startVolume = audioSource.volume;
        float fadeTime = 0.5f;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
        }


        audioSource.volume = 1.0f;
    }

    //time for bayonet
    IEnumerator SetFalse2()
    {
        yield return new WaitForSeconds(4); //wait 2 seconds
        bayonetActive = false;
        hasPowerUp = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //basicBullets
        if (collision.gameObject.tag == "basicBullet")
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            TakeDamage(bulletDamage);
            Destroy(collision.gameObject);
        }

        //homingMissile
        if (collision.gameObject.tag == "homingMissile")
        {
            TakeDamage(100f);
        }

        //fragGrenade
        if (collision.gameObject.tag == "fragGrenade")
        {
            TakeDamage(100f);
        }

        //fragGrenadeShards
        if (collision.gameObject.tag == "fragGrenadeShard")
        {
            TakeDamage(20f);
            Destroy(collision.gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //laser power up
        if (collider.gameObject.tag == "laserPowerUp" && hasPowerUp == false)
        {
            Destroy(collider.gameObject);
            laserActive = true;
            hasPowerUp = true;
            weaponDescription.SetText("Ion Cannon");
        }

        //missile power up
        if (collider.gameObject.tag == "missilePowerUp" && hasPowerUp == false)
        {
            Destroy(collider.gameObject);
            homingMissileActive = true;
            hasPowerUp = true;
            weaponDescription.SetText("Cerberus");
        }

        //frag power up
        if (collider.gameObject.tag == "fragPowerUp" && hasPowerUp == false)
        {
            Destroy(collider.gameObject);
            fragGrenadeActive = true;
            hasPowerUp = true;
            weaponDescription.SetText("Pulse Grenade");
        }

        //bayonet power up
        if (collider.gameObject.tag == "bayonetPowerUp" && hasPowerUp == false)
        {
            Destroy(collider.gameObject);
            bayonetActive = true;
            hasPowerUp = true;
            weaponDescription.SetText("God's Right Hand");
        }


        //gravity bomb power up
        if (collider.gameObject.tag == "gravityPowerUp" && hasPowerUp == false)
        {
            Destroy(collider.gameObject);
            gravityBombActive = true;
            hasPowerUp = true;
            weaponDescription.SetText("Gravity Bomb");
        }

        //missile after effect
        if (collider.gameObject.tag == "missileAfterEffect")
        {
            TakeDamage(50f);
        }

        //bayonet
        if (collider.gameObject.tag == "bayonet")
        {
            TakeDamage(100f);
        }

    }

    //static so laser class can access it
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void PlayerDeath()
    {

        //myAudioSource.clip = playerDeathSound;
        AudioSource.PlayClipAtPoint(playerDeathSound, new Vector3(0f, 0f, -10f));

        //useless possibly
        laserActive = false;
        laserIsShot = false;

        GameObject newExplosion = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
        if (gameManager3P == null)
        {
            gameManager.PlayerDied();
        }
        else
        {
            gameManager3P.PlayerDied();
        }
        Destroy(newExplosion, 2);
    }

    public void ResetPlayer()
    {
        this.currentHealth = 100f;
        healthBar.SetHealth(currentHealth);

        hasPowerUp = false;
        laserActive = false;
        laserIsShot = false;
        homingMissileActive = false;
        fragGrenadeActive = false;
        bayonetActive = false;
        gravityBombActive = false;
    }
}
