using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameObject timeIndicator = null;
    public GameObject cashIndicator = null;

    public GameObject one_star;
    public GameObject two_star;
    public GameObject three_star;

    public GameObject continueBtn;
    public GameObject finishedBtn;

    float time = 0;
    string level;

    // Start is called before the first frame update
    void Start()
    {

        time = PlayerPrefs.GetFloat("CompletionTime");
        
        if(time < 60){
            one_star.SetActive(false);
            two_star.SetActive(false);
            three_star.SetActive(true);
        }else if(time < 120){
            one_star.SetActive(false);
            two_star.SetActive(true);
            three_star.SetActive(false);
        }else{
            one_star.SetActive(true);
            two_star.SetActive(false);
            three_star.SetActive(false);
        }

        
        timeIndicator.GetComponent<Text>().text = time.ToString("F0") + "s";
        cashIndicator.GetComponent<Text>().text = "$" + PlayerPrefs.GetInt("Cash");

        level = PlayerPrefs.GetString("NextLevel");

        if(level == "end"){
            UnityEngine.Debug.Log(level+"--lLEVEL");
            // Game Levels Completed Show Finished Button
            continueBtn.SetActive(false);
            finishedBtn.SetActive(true);
        }else{
            UnityEngine.Debug.Log(level+"--ELSE LEVEL");
            continueBtn.SetActive(true);
            finishedBtn.SetActive(false);
        }
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("NextLevel"));
    }

    public void toStartScreen(){
        SceneManager.LoadScene(0);   
    }

    public void quit()
    {
        Application.Quit();
    }
}
