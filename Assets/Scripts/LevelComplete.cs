using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameObject timeIndicator = null;
    public GameObject cashIndicator = null;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        time = PlayerPrefs.GetFloat("CompletionTime");
        timeIndicator.GetComponent<Text>().text = "Time: " + time.ToString("F2") + " seconds";
        cashIndicator.GetComponent<Text>().text = "Cash: $" + PlayerPrefs.GetInt("Cash");
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("NextLevel"));
    }

    public void quit()
    {
        Application.Quit();
    }
}
