using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu = null;
    public GameObject optionsScreen = null;

    public AudioSource backgroundMusic = null;
    public Slider volumeSlider = null;

    float gameVolume = 0.3f;

    public static int difficulty = 1; // 0 = Easy, 1 = Normal, 2 = Hard..

    void Start(){
        gameVolume = PlayerPrefs.GetFloat("volume");
        volumeSlider.value = gameVolume;
    }

    public void startGame()
    {
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
        PlayerPrefs.SetFloat("volume", gameVolume);
    }

    public static void updateDifficulty(int value){
        difficulty = value;
    }
}