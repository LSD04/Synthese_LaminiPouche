using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouletEnemy : MonoBehaviour
{
   [SerializeField] private float _speed = 10f;

   
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed); 
        if (transform.position.y < -9f) 
        {
            Destroy(gameObject);
        }
    }
}
