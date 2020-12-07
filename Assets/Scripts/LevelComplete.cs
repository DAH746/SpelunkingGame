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
    public GameObject playAgainBtn;

    public GameObject multiplerDisplay;

    float time = 0;
    string level;

    // Start is called before the first frame update
    void Start()
    {

        time = PlayerPrefs.GetFloat("CompletionTime");
        
        float bonus = 0f;
        float difficulty_bonus = 0f;
        string difficulty_multiplier = "0%";
        int newCash = 0;

        if(time < 60){
            one_star.SetActive(false);
            two_star.SetActive(false);
            three_star.SetActive(true);

            // Bonus
            bonus = PlayerPrefs.GetInt("Cash") * 0.2f;
            UnityEngine.Debug.Log(bonus+"--LESSTHAN60BONUS");
        }else if(time < 120){
            one_star.SetActive(false);
            two_star.SetActive(true);
            three_star.SetActive(false);
            // Bonus
            bonus = PlayerPrefs.GetInt("Cash") * 0.1f;
            UnityEngine.Debug.Log(bonus+"--LESSTHAN120BONUS");
            
        }else{
            one_star.SetActive(true);
            two_star.SetActive(false);
            three_star.SetActive(false);
        }

       // Add difficulty mulitpler is on normal or hard difficulty..
       if(MainMenu.difficulty == 1){
            // Normal
            difficulty_bonus = PlayerPrefs.GetInt("Cash") * 0.1f;
            UnityEngine.Debug.Log(difficulty_bonus+"--DIFFICULTYBONUS-NORMAL");
            difficulty_multiplier = "10%";
        }else if(MainMenu.difficulty == 2){
            // Hard
            UnityEngine.Debug.Log(difficulty_bonus+"--DIFFICULTYBONUS-HARD");
            difficulty_bonus = PlayerPrefs.GetInt("Cash") * 0.5f;
            difficulty_multiplier = "50%";
        }

        newCash = Int32.Parse(CashScript.cashValue.ToString()) + Convert.ToInt32(bonus) + Convert.ToInt32(difficulty_bonus);
        CashScript.cashValue = newCash;
        
        timeIndicator.GetComponent<Text>().text = time.ToString("F0") + "s";
        multiplerDisplay.GetComponent<Text>().text = "Difficulty Multiplier: "+ difficulty_multiplier + (difficulty_bonus >= 0 ? " = $"+difficulty_bonus.ToString("F0")+" Bonus" : "");
        cashIndicator.GetComponent<Text>().text = "$" + PlayerPrefs.GetInt("Cash") + (bonus >= 0 ? " (+$"+bonus.ToString("F0")+" Time Bonus)" : "");

        level = PlayerPrefs.GetString("NextLevel");

        if(level == "end"){
            UnityEngine.Debug.Log(level+"--lLEVEL");
            // Game Levels Completed Show Finished Button
            continueBtn.SetActive(false);
            finishedBtn.SetActive(true);
            playAgainBtn.SetActive(true);
        }
        else{
            UnityEngine.Debug.Log(level+"--ELSE LEVEL");
            continueBtn.SetActive(true);
            finishedBtn.SetActive(false);
            playAgainBtn.SetActive(false);
        }
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("NextLevel"));
    }

    public void toStartScreen(){
        CashScript.cashValue = 0;
        SceneManager.LoadScene(0);   
    }

    public void toFirstLevel()
    {
        CashScript.cashValue = 0;
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }
}
