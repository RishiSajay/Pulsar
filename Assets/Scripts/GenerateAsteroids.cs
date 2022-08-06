using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAsteroids : MonoBehaviour
{
    public float spawnRate = 4.0f;
    public int spawnAmount = 5;
    public Asteroid asteroidPrefab;

    public float radius = 12.0f;

    public float trajectoryVariance = 15.0f;

    List<GameObject> asteroids = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Generate();
        InvokeRepeating(nameof(Generate), this.spawnRate, this.spawnRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Generate()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            //spawn direction is random point on circle
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.radius;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward); 

            Asteroid newAsteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            newAsteroid.size = Random.Range(newAsteroid.minSize, newAsteroid.maxSize);

            newAsteroid.SetTrajectory(rotation * -spawnDirection);

            asteroids.Add(newAsteroid.gameObject);
        }
    }

    public void ResetAsteroids()
    {
        for (int i = 0; i < asteroids.Count; i++)
        {
            if (asteroids[i] != null)
            {
                Destroy(asteroids[i]);
            }
        }
        asteroids.Clear();
    }
    
}


