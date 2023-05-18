using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
   [SerializeField] private float _fireRate = 10f;
   [SerializeField] private GameObject _bouletFollowerPrefab = default;
   private float _canFire = -1f;
   //private bool _isTripleActive = false;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
         if (Time.time > _canFire)
        {
        _canFire = Time.time + _fireRate;
        Instantiate(_bouletFollowerPrefab, transform.position + new Vector3(0f, 1.2f, 0f), Quaternion.identity);
        }
    }

    
}
