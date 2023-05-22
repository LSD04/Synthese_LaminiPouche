using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class HealthBar : MonoBehaviour
{

 private UIManager _uiManager;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    
    // Start is called before the first frame update
    
     private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
    }

    public void SetMaxHeatlh ( int health)
    {
        slider.maxValue = health;
        slider.value=health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color= gradient.Evaluate(slider.normalizedValue);

        if(slider.value == 0)
        {
            PlayerPrefs.SetInt("Score",_uiManager.getScore());
            PlayerPrefs.Save();
           SceneManager.LoadScene(2);
        }
    }

 
}
