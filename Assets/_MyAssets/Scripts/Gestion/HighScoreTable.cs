using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    
    private Transform _entryContainer;
    private Transform _entryTemplate;
    private List<Transform> _highScoreEntryTransformList;
    [SerializeField] private GameObject _txtRestart = default;
    [SerializeField] private GameObject _txtMenu = default;
    [SerializeField] private Button _button = default;
    [SerializeField] private Text _text = default;
    [SerializeField] private GameObject _saisieNom = default;
    [SerializeField] private GameObject _txtErreur = default;
  

    private HighScores highScores;

    private void Awake()
    {
        PlayerPrefs.DeleteKey("highScoreTable"); // Sert si l'on d�sire effacer les scores
        GenererTableHighScore();
        //V�rifie si on est sur la sc�ne de fin afin de g�rer l'action du bouton de sauvegarde
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //Si le score obtenu est plus grand que le score en position 10 on affiche l'�cran d'ajout
            if (highScores._highScoreEntryList.Count >= 1)
            {
                if (PlayerPrefs.GetInt("Score") > highScores._highScoreEntryList[0].score)
                {
                    _saisieNom.SetActive(true);
                    _txtMenu.SetActive(false);
                    _txtRestart.SetActive(false);
                     Button btn = _button.GetComponent<Button>();
                    btn.onClick.AddListener(EnregistrerNom);
               
                }
            }
            else
            {
                _saisieNom.SetActive(false);
                _txtMenu.SetActive(true);
                _txtRestart.SetActive(true);
                Button btn = _button.GetComponent<Button>();
                btn.onClick.AddListener(EnregistrerNom);
            }
        }
    }

    private void GenererTableHighScore()
    {
        _entryContainer = transform.Find("HighScoreEntryContainer");
        _entryTemplate = _entryContainer.Find("HighScoreEntryTemplate");

        _entryTemplate.gameObject.SetActive(false);

        
        //AddHighScoreEntry(14500, "Dave");  //Teste avec un ajout manuel
        //AddHighScoreEntry(3400, "Alex");
        //AddHighScoreEntry(700, "Jos�e");
        //AddHighScoreEntry(5500, "Maxime");
        //AddHighScoreEntry(7800, "David");
        //AddHighScoreEntry(1800, "Shany");
        //AddHighScoreEntry(100, "Fran�ois");
        //AddHighScoreEntry(0, "Fabrice");
        //AddHighScoreEntry(100, "Jonathan");
        //AddHighScoreEntry(100, "Line");



        // R�cup�re la liste des highscores dans une liste � partir du PlayerPrefs
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highScores == null)
        {
            AddHighScoreEntry(100, "Stan");
        }

        // trier(ordonner) la liste des highscores
        for (int i = 0; i < highScores._highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScores._highScoreEntryList.Count; j++)
            {
                if (highScores._highScoreEntryList[j].score > highScores._highScoreEntryList[i].score)
                {
                    //Swap
                    HighScoreEntry tmp = highScores._highScoreEntryList[i];
                    highScores._highScoreEntryList[i] = highScores._highScoreEntryList[j];
                    highScores._highScoreEntryList[j] = tmp;
                }
            }
        }

        
        _highScoreEntryTransformList = new List<Transform>();
        // Utilise la fonction pour ajouter chaque entr�e de la liste dans ma table � afficher
        int compteur = 1;
        foreach (HighScoreEntry highScoreEntry in highScores._highScoreEntryList)
        {
            if (compteur <= 1)
            {
                CreateHighScoreEntryTransform(highScoreEntry, _entryContainer, _highScoreEntryTransformList);
            }
            compteur++;
        }

        

    }
    private void EnregistrerNom()
    {

        bool valide = false;
        string saisie = _text.text;
        foreach( char c in saisie) {
            if (c != ' ') {
                valide = true;
            }
        }
        
        if (!string.IsNullOrEmpty(saisie) && valide)
        {
            AddHighScoreEntry(PlayerPrefs.GetInt("Score"), saisie);
            _saisieNom.SetActive(false);
            _txtMenu.SetActive(true);
            _txtRestart.SetActive(true);
            _txtErreur.SetActive(false);
            foreach(Transform child in _entryContainer.transform)
            {
                if (child.name != "HighScoreEntryTemplate")
                {
                    Destroy(child.gameObject);
                }
            }
            GenererTableHighScore();
        }
        else
        {
            _txtErreur.SetActive(true);
        }
        
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 50f;
        Transform entryTransform = Instantiate(_entryTemplate, container);
        RectTransform entryRectTranform = entryTransform.GetComponent<RectTransform>();
        entryRectTranform.anchoredPosition = new Vector2(0f, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default:
                rankString = rank + "TH"; break;
        }
        entryTransform.Find("TxtPos").GetComponent<Text>().text = rankString;

        int score = highScoreEntry.score;
        entryTransform.Find("TxtScore").GetComponent<Text>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("TxtName").GetComponent<Text>().text = name;

        if (rank == 1)
        {
            entryTransform.Find("background").GetComponent<Image>().color = new Color32(255, 210, 3, 71);
        }
        else if (rank == 2)
        {
            entryTransform.Find("background").GetComponent<Image>().color = new Color32(203, 201, 193, 71);
        }
        else if (rank == 3)
        {
            entryTransform.Find("background").GetComponent<Image>().color = new Color32(176, 114, 26, 71);
        }
        else
        {
            entryTransform.Find("background").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        



        transformList.Add(entryTransform);
    }

    public void AddHighScoreEntry(int p_score, string p_name)
    {
        //Creer un nouvel objet HighScore Entry � partir du score et nom recu
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = p_score, name = p_name };

        //Charger les HighScores sauvegarder dans le playerprefs
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if(highScores == null)  // Si jamais la table est vide on cr�er une nouvelle liste
        {
            highScores = new HighScores()
            {
                _highScoreEntryList = new List<HighScoreEntry>()
            };
        }

        //Ajouter la nouvelle entr�e aux HighScores
        highScores._highScoreEntryList.Add(highScoreEntry);

        //Sauvegarder les nouveaux HighScores dans le playerperfs
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();

    }

    //Classe qui contient la liste des highScores
    private class HighScores
    {
        public List<HighScoreEntry> _highScoreEntryList;
        
    }
    
    /*
     * Classe qui repr�sente une entr�e HighScore
     */
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }

    public void Annuler()
    {
        _saisieNom.SetActive(false);
       
        _txtMenu.SetActive(true);
        _txtRestart.SetActive(true);
        _txtErreur.SetActive(false);
    }
}
