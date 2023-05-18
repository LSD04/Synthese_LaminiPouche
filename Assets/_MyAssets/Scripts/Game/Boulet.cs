using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulet : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private string _nom = default;

   
    void Update()
    {
        if (_nom == "Player")
        {
            BouletJoueur();
        }
        else if (_nom == "Enemy")
        {
            BouletEnnemi();
        }
        else {
            BouletFollower();
        }
    }

    private void BouletJoueur()
    {
         transform.Translate(Vector3.up * Time.deltaTime * _speed); 
        if (transform.position.y > 9f) 
        {
            Destroy(gameObject);
        }
    }

    private void BouletEnnemi()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed); 
        if (transform.position.y < -9f) 
        {
            Destroy(gameObject);
        }
    }

     private void BouletFollower()
    {
         transform.Translate(Vector3.up * Time.deltaTime * _speed); 
        if (transform.position.y > 9f) 
        {
            Destroy(gameObject);
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _nom != "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Degats();
            Destroy(gameObject);
        }
    }
}
