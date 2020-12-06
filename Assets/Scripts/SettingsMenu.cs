using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
	public AudioSource backgroundMusic = null;
	public GameObject settingsMenu = null;
   
    public void setGameVolume(float volume)
    {
        float gameVolume = volume;
        backgroundMusic.volume = volume;
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
