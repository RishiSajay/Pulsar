using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePowerUps : MonoBehaviour
{
    public GameObject missilePowerUp;
    public GameObject laserPowerUp;
    public GameObject fragPowerUp;
    public GameObject bayonetPowerUp;
    public GameObject gravityBombPowerUp;
    int maxPowerUps = 6;
    int numPowerUps = 0;
    List<GameObject> listOfPowerUps = new List<GameObject>();
    List<GameObject> currentPowerUps = new List<GameObject>();

    public GameplayBounds gameplayBounds;
    // Start is called before the first frame update
    void Start()
    {
        listOfPowerUps.Add(missilePowerUp);
        listOfPowerUps.Add(laserPowerUp);
        listOfPowerUps.Add(fragPowerUp);
        listOfPowerUps.Add(bayonetPowerUp);
        listOfPowerUps.Add(gravityBombPowerUp);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < currentPowerUps.Count; i++)
        {
            if (currentPowerUps[i] == null)
            {
                currentPowerUps.RemoveAt(i);
                numPowerUps--;
            }
        }
        if (numPowerUps < maxPowerUps)
        {
            numPowerUps++;
            int time = Random.Range(2, 8);
            StartCoroutine(Generate(time));
        }
    }

    IEnumerator Generate(float time)
    {
        yield return new WaitForSeconds(time); //wait time seconds
        int powerToGenerate = Random.Range(0, 5);
        //float fieldRadius = 6f;
        Vector3 randomSpawnRotation = new Vector3(0, 0, Random.Range(0, 360));
        Vector3 position = gameplayBounds.GetRandomPosInRect();
        GameObject newPowerUp = Instantiate(listOfPowerUps[powerToGenerate], position, Quaternion.Euler(randomSpawnRotation));
        /*while (screenBounds.AmIOutOfBounds(newPowerUp.transform.position))
        {
            Destroy(newPowerUp);
            newPowerUp = Instantiate(listOfPowerUps[powerToGenerate], Random.insideUnitCircle * fieldRadius, Quaternion.Euler(randomSpawnRotation));
        }*/
        currentPowerUps.Add(newPowerUp);
    }

    public void ResetPowerUps()
    {
        for (int i = 0; i < currentPowerUps.Count; i++)
        {
            if (currentPowerUps[i] != null)
            {
                Destroy(currentPowerUps[i]);
            }
        }
        currentPowerUps.Clear();
        numPowerUps = 0;
    }
}
