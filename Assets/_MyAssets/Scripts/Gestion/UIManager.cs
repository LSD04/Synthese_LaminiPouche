using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class UIManager : MonoBehaviour
{
   [SerializeField] private int _score =  default;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private Image[] _lives = default; 

    void Start()
    {
       _score = 0; 
        UpdateScore();
    }

    public void AjouterScore(int points) {
        _score += points;
        UpdateScore();
    }
    private void UpdateScore() {
        _txtScore.text = "Score : " + _score.ToString();
    }

    public void DeleteImage(int noImage)
    {
        _lives[noImage].gameObject.SetActive(false);
        noImage++;
    }
}
