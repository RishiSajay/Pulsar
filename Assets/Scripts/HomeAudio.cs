using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeAudio : MonoBehaviour
{
    /*
    public AudioClip[] homeScreenMusic;
    bool isPlaying = false;
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("homeAudio");
        if (musicObj.Length > 1)
        {
            for (int i = 0; i < musicObj.Length; i++)
            {
                
            }

        }
        AudioSource audioSource = musicObj[0].GetComponent<AudioSource>();
        audioSource.clip = homeScreenMusic[Random.Range(0, 1)];
        audioSource.Play();
        isPlaying = true;

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "1PlayerScene" || scene.name == "2PlayerScene" || scene.name == "3PlayerScene")
        {
            Destroy(this.gameObject);
        }
    }*/

    public AudioClip[] homeScreenMusic;
    private AudioSource _audioSource;
    private GameObject[] other;
    private bool NotFirst = false;
    private void Awake()
    {
        other = GameObject.FindGameObjectsWithTag("homeAudio");

        foreach (GameObject oneOther in other)
        {
            if (oneOther.scene.buildIndex == -1)
            {
                NotFirst = true;
            }
        }

        if (NotFirst == true)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.clip = homeScreenMusic[Random.Range(0, homeScreenMusic.Length)];
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "1PlayerScene" || scene.name == "2PlayerScene" || scene.name == "3PlayerScene")
        {
            Destroy(this.gameObject);
        }
    }
}
