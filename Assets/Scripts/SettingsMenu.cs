using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
	public AudioSource backgroundMusic = null;
    public Slider volumeSlider = null;
	public GameObject settingsMenu = null;
    float gameVolume = 0.3f;

    void Start(){
        if (PlayerPrefs.HasKey("volume"))
        {
            backgroundMusic.volume = PlayerPrefs.GetFloat("volume");
            gameVolume = PlayerPrefs.GetFloat("volume");
        }
        volumeSlider.value = gameVolume;
    }

    public void setGameVolume(float volume)
    {
        float gameVolume = volume;
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat("volume", gameVolume);
    }

    public void resumeGame(){
    	settingsMenu.SetActive(false);
    }

    public void leaveGame()
    {
    	settingsMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
