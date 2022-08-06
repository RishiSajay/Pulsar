using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserP2 : MonoBehaviour
{
    private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;

    public Player1 player1;
    public Player2 player2;
    public Player3 player3;

    public GameObject laserEffect;


    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, transform.up))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, transform.up);
            if (hit.transform.gameObject.tag == "Player1")
            {
                player1.TakeDamage(6.0f);
            }
            if (hit.transform.gameObject.tag == "Player2")
            {
                player2.TakeDamage(6.0f);
            }
            if (hit.transform.gameObject.tag == "Player3")
            {
                player3.TakeDamage(6.0f);
            }
            Draw2DRay(laserFirePoint.position, hit.point);
        }

        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.up * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        GameObject newlaserEffect = (GameObject)Instantiate(laserEffect, laserFirePoint.position, Quaternion.identity);
        newlaserEffect.transform.parent = laserFirePoint;
        newlaserEffect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(newlaserEffect, 0.5f);
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool laserIsShot = Player2.laserIsShot;
        if (laserIsShot == true)
        {
            m_lineRenderer.SetVertexCount(2);
            ShootLaser();
        }
        else
        {
            m_lineRenderer.SetVertexCount(0);
        }
    }
}
