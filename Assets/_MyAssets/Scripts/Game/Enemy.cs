using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     [SerializeField] private float _speed = 5f;
     [SerializeField] private int _points = 100;
      [SerializeField] private float _fireRate = 2f;
      [SerializeField] private GameObject _bouletEnemyPrefab = default;
     
     private Player _player;
     private float _canFire = 1f;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
         MouvementsEnemy();
          Fire();
    }

     private void Fire()
    {
        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;    
            Instantiate(_bouletEnemyPrefab, transform.position + new Vector3(0f, -1.2f, 0f), Quaternion.identity);
              
        }
    }

     private void MouvementsEnemy()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-4.5f, 4.5f);
            transform.position = new Vector3(randomX, 6f, 0f);
        }
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boulet")
        {
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.AjouterScore(_points);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
           _player.Degats();
            Destroy(gameObject);
        }
    }
}
