using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayAudio : MonoBehaviour
{
    public static GameplayAudio instance;

    public bool playing;
    public AudioSource myAudioSource;
    public AudioClip[] myClips;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        StartCoroutine(LoopMusic());
    }

    IEnumerator LoopMusic()
    {
        yield return null;
        while (playing)
        {
            for (int i = 0; i < myClips.Length; i++)
            {
                myAudioSource.clip = myClips[i];
                myAudioSource.Play();

                while(myAudioSource.isPlaying)
                {
                    yield return null;
                }
            }
        }
    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "1PlayerScene" && scene.name != "2PlayerScene" && scene.name != "3PlayerScene")
        {
            Destroy(this.gameObject);
        }
    }
}
