using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager3Player : MonoBehaviour
{
    public Player1 player1;
    public Player2 player2;
    public Player3 player3;
    public GeneratePowerUps powerUpGenerator;
    public GenerateAsteroids asteroidGenerator;

    public GameplayBounds gameplayBounds;

    //player 1 score
    public PlayerScore player1ScoreText;

    //player 2 score
    public PlayerScore player2ScoreText;

    //player 3 score
    public PlayerScore player3ScoreText;

    int player1Score;
    int player2Score;
    int player3Score;
    int numPlayersDead = 0;

    float respawnRate = 3.5f;
    float fieldRadius = 5.0f;

    bool playerAlreadyDead = false;

    int scoreToWin = 20;


    // Start is called before the first frame update
    void Start()
    {
        this.player1Score = 0;
        this.player2Score = 0;
        this.player3Score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerDied()
    {
        this.numPlayersDead++;
        if (this.numPlayersDead > 1)
        {
            //same variable name as 2 player game manager
            //logic: Only start coroutine once. Coroutine starts when second player dies. If third player dies after, coroutine is not started 
            if (this.playerAlreadyDead == false)
            {
                this.playerAlreadyDead = true;
                StartCoroutine(UpdateGame());
            }
        }
    }

    IEnumerator UpdateGame()
    {
        yield return new WaitForSeconds(this.respawnRate); //wait x seconds
        if (this.numPlayersDead >= 3)
        {
            yield return new WaitForSeconds(2.0f); //if all three players die, wait 2 more seconds
        }
        if (player1.gameObject.activeSelf == true)
        {
            this.player1Score++;
        }
        else if (player2.gameObject.activeSelf == true)
        {
            this.player2Score++;
        }
        else if (player3.gameObject.activeSelf == true)
        {
            this.player3Score++;
        }
        ResetMap();
        Respawn();
        UpdateScore();
        RandomizePositions();
        RemoveAllActivePowerUps();
        CheckForWinner();
        this.playerAlreadyDead = false;
        this.numPlayersDead = 0;
    }

    private void Respawn()
    {
        this.player1.gameObject.SetActive(true);
        this.player1.ResetPlayer();
        this.player2.gameObject.SetActive(true);
        this.player2.ResetPlayer();
        this.player3.gameObject.SetActive(true);
        this.player3.ResetPlayer();
    }

    void UpdateScore()
    {
        this.player1ScoreText.SetScore("Blue Team: " + player1Score);
        this.player2ScoreText.SetScore("Red Team: " + player2Score);
        this.player3ScoreText.SetScore("Green Team: " + player3Score);
    }

    void ResetMap()
    {
        powerUpGenerator.ResetPowerUps();
        asteroidGenerator.ResetAsteroids();
    }

    void RandomizePositions()
    {
        //position
        Vector3 position1 = gameplayBounds.GetRandomPosInRect();
        Vector3 position2 = gameplayBounds.GetRandomPosInRect();
        Vector3 position3 = gameplayBounds.GetRandomPosInRect();

        this.player1.gameObject.transform.position = position1;
        this.player2.gameObject.transform.position = position2;
        this.player3.gameObject.transform.position = position3;

        //0 velocity
        this.player1.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        this.player2.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        this.player3.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);

        //rotation
        player1.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360);
        player2.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360);
        player3.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360);
    }

    void RemoveAllActivePowerUps()
    {
        var bayonets = GameObject.FindGameObjectsWithTag("bayonet");
        foreach (var item in bayonets)
        {
            Destroy(item);
        }

        var fragGrenades = GameObject.FindGameObjectsWithTag("fragGrenade");
        foreach (var item in fragGrenades)
        {
            Destroy(item);
        }

        var fragShards = GameObject.FindGameObjectsWithTag("fragGrenadeShard");
        foreach (var item in fragShards)
        {
            Destroy(item);
        }

        var bullet = GameObject.FindGameObjectsWithTag("basicBullet");
        foreach (var item in bullet)
        {
            Destroy(item);
        }

        var missiles = GameObject.FindGameObjectsWithTag("homingMissile");
        foreach (var item in missiles)
        {
            Destroy(item);
        }

        var gravityBombs = GameObject.FindGameObjectsWithTag("gravityBomb");
        foreach (var item in gravityBombs)
        {
            Destroy(item);
        }

        var blackHoles = GameObject.FindGameObjectsWithTag("blackHole");
        foreach (var item in blackHoles)
        {
            Destroy(item);
        }

    }

    IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(2); //wait 2 seconds
    }

    void CheckForWinner()
    {
        if (player1Score >= scoreToWin)
        {
            SceneManager.LoadScene("Player1Wins");
        }
        else if (player2Score >= scoreToWin)
        {
            SceneManager.LoadScene("Player2Wins");
        }
        else if (player3Score >= scoreToWin)
        {
            SceneManager.LoadScene("Player3Wins");
        }
        return;
    }
}
