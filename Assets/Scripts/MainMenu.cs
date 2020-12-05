using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu = null;
    public GameObject optionsScreen = null;

    public AudioSource backgroundMusic = null;

    float gameVolume = 0.5f;

    public void startGame()
    {
        PlayerPrefs.SetFloat("volume", gameVolume);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void options()
    {
        mainMenu.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void main()
    {
        optionsScreen.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void setGameVolume(float volume)
    {
        gameVolume = volume;
        backgroundMusic.volume = volume;
    }
}
