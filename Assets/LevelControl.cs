using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class LevelControl : MonoBehaviour
{

    [SerializeField]
    string nextLevel;

    public float timer = 0;
    public GameObject timeIndicator = null;
    private bool active = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("CompletionTime", timer);
            PlayerPrefs.SetString("NextLevel", nextLevel);
            PlayerPrefs.SetInt("Cash", CashScript.cashValue);
            SceneManager.LoadScene("Level Complete");
        }
    }

    public void Update() {
        if(active){
            timer += Time.deltaTime;
            timeIndicator.GetComponent<Text>().text = timer.ToString("F0") + "s";
        }
    }

    public void Pause(){
        active = false;
    }

    public void Resume(){
        active = true;
    }
}
