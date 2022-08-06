using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Load2Player()
    {
        SceneManager.LoadScene("2PlayerScene");
    }

    public void Load3Player()
    {
        SceneManager.LoadScene("3PlayerScene");
    }

    public void Load1player()
    {
        SceneManager.LoadScene("1PlayerScene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

    public void Load2PlayerControls()
    {
        SceneManager.LoadScene("2PlayerControls");
    }

    public void Load3PlayerControls()
    {
        SceneManager.LoadScene("3PlayerControls");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
