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
    [SerializeField] private GameObject _pausePanel = default;
    private bool _pauseOn = false;

    void Start()
    {
       _score = 0; 
        _pauseOn = false;
        Time.timeScale = 1;
        UpdateScore();
    }


    private void Update() {
        
        // Permet la gestion du panneau de pause (marche/arrêt)
        if ((Input.GetKeyDown(KeyCode.Escape) && !_pauseOn))  {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && _pauseOn)) {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }
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
         if (noImage == 0) {
            PlayerPrefs.SetInt("Score", _score);
            PlayerPrefs.Save();
            StartCoroutine("FinPartie");
            
        }
    }
    IEnumerator FinPartie()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(2);
        }
     public int getScore()
    {
        return _score;
    }
    
    public void ChargerDepart()
    {
        SceneManager.LoadScene(0);
    }

     public void ResumeGame() {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }

    
}
