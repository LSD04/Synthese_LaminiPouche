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
            LaserJoueur();
        }
        else
        {
            LaserEnnemi();
        }
    }

    private void LaserJoueur()
    {
         transform.Translate(Vector3.up * Time.deltaTime * _speed); 
        if (transform.position.y > 9f) 
        {
            Destroy(gameObject);
        }
    }

    private void LaserEnnemi()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed); 
        if (transform.position.y < -9f) 
        {
            Destroy(gameObject);
        }
    }
}
