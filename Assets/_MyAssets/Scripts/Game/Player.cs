using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Player : MonoBehaviour
{
   [SerializeField] private float _speed = 10f;
   [SerializeField] private float _fireRate = 0.5f;
   [SerializeField] private int _viesJoueur = 3;
    [SerializeField] private AudioClip _laserSound = default;
    [SerializeField] private AudioClip _endSound = default;
   [SerializeField] private GameObject _bouletPrefab = default;
   [SerializeField] private GameObject _followerPrefab1 = default;
   [SerializeField] private GameObject _followerPrefab2 = default;

    private float _canFire = -1f;
    private Animator _anim;
    private bool _isTripleActive = false;

    public int maxHeatlh = 3;
    public int currentHealth ;
    public HealthBar healthBar;

    void Start()
    {
         _anim = GetComponent<Animator>();
         currentHealth = maxHeatlh;
         healthBar.SetMaxHeatlh(maxHeatlh);
    }

    // Update is called once per frame
    void Update()
    {
        MouvementsJoueur();
         Fire();
    }

     private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            AudioSource.PlayClipAtPoint(_laserSound, Camera.main.transform.position, 0.3f);
           
            Instantiate(_bouletPrefab, transform.position + new Vector3(0f, 1.2f, 0f), Quaternion.identity);
              
        }
    }

     private void MouvementsJoueur()
    {
        float posHorizontal = Input.GetAxis("Horizontal");
        float posVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(posHorizontal, posVertical, 0);
        transform.Translate(direction * Time.deltaTime * _speed);

        
        if (posHorizontal < 0f)
        {
            _anim.SetBool("TurnLeft", true);
            _anim.SetBool("TurnRight", false);
        }
        else if (posHorizontal > 0f)
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", true);
        }
        else
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", false);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.2f, 4.2f), Mathf.Clamp(transform.position.y, -4.1f, -1.40f), 0f);

    }

    public void Degats()
    {
       // _viesJoueur--;
       
       // UIManager uiManager = FindObjectOfType<UIManager>();
        //uiManager.DeleteImage(_viesJoueur);
      
        if(currentHealth >= 1)
        {
           currentHealth--;
        healthBar.SetHealth(currentHealth);
        }
        else
        {
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
             AudioSource.PlayClipAtPoint(_endSound, Camera.main.transform.position, 0.8f);
            Destroy(this.gameObject);
        }


    }

     IEnumerator FinPartie()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(2);
        }

    public int  Sante()
    {
        return currentHealth;
    }
    
  

    public void PowerUp()
    {
        if (!_isTripleActive)
        {
        _followerPrefab1.SetActive(true);
        _followerPrefab2.SetActive(true);
        }
        _isTripleActive = true;
        StartCoroutine(Triple());
       
    }
     IEnumerator Triple()
    {
        yield return new WaitForSeconds(5f);
        _isTripleActive = false;
         _followerPrefab1.SetActive(false);
        _followerPrefab2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BouletEnnemi" )
        {
            Degats();
        }
    }

}
