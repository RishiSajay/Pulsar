using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BlackHole : MonoBehaviour
{
    float gravityForce = 5000f;
    static float radius = 5f;
    CircleCollider2D myCollider;

    private Rigidbody2D rb2D;
    float rotateSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.radius = radius;
        rb2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.angularVelocity = rotateSpeed;
    }


    void OnTriggerStay2D(Collider2D collider)
    {
        Rigidbody2D rb2D = collider.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            float forceIntensity = Vector3.Distance(transform.position, collider.transform.position) / radius;
            Vector3 forceDirection = transform.position - collider.transform.position;
            float finalGravitationalForce = gravityForce * forceIntensity * Time.deltaTime;

            if (collider.tag != "basicBullet")
            {
                rb2D.AddForce(forceDirection * finalGravitationalForce);
            }
            else
            {
                rb2D.AddForce(forceDirection * finalGravitationalForce / 2000);
            }
        }
    }

}
