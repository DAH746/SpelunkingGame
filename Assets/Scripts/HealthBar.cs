using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
	public GameObject healthIndicator = null;
	private int maxHeatlh;
    
    public void SetHealth(int health){
    	slider.value = health;
    	healthIndicator.GetComponent<Text>().text = health.ToString() + "/" + maxHeatlh;
    }

    public void SetMaxHealth(int health){
    	this.maxHeatlh = health;
    	slider.maxValue = health;
    	slider.value = health;
    	healthIndicator.GetComponent<Text>().text = health.ToString() + "/" + health.ToString();

    }
}
