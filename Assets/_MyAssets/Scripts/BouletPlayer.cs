using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouletPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;

   
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed); 
        if (transform.position.y > 9f) 
        {
            Destroy(gameObject);
        }
    }
}
