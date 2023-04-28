using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     [SerializeField] private float _speed = 5f;
     private Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
         MouvementsEnemy();
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
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
           
            Destroy(gameObject);
        }
    }
}
