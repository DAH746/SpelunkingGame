using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashScript : MonoBehaviour
{
    public static int cashValue = 0;
    Text cash;
    
    void Start()
    {
        cash = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        cash.text = cashValue.ToString();
    }
}
