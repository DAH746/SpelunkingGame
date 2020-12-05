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

    void Update() {
        timer += Time.deltaTime;
        timeIndicator.GetComponent<Text>().text = "Time: " + timer.ToString("F2") + " seconds";
    }
}
