using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    public Sprite[] asteroids;

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    public float speed = 250.0f;
    public float maxLifeTime = 30.0f;

    private float minSpinSpeed = 2f;
    private float maxSpinSpeed = 8f;
    private float minThrust = 1f;
    private float maxThrust = 5f;
    private float spinSpeed;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = asteroids[Random.Range(0, asteroids.Length)];

        //random rotation, not angular velocity
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360);
        this.transform.localScale = Vector3.one * this.size;
        rb2d.mass = this.size*5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb2d.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifeTime);
    }
}
