using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private float _speed = 10f;
   [SerializeField] private float _fireRate = 0.5f;
   [SerializeField] private GameObject _bouletPrefab = default;

    private float _canFire = -1f;
    void Start()
    {
        
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
           
            
                Instantiate(_bouletPrefab, transform.position + new Vector3(0f, 1.2f, 0f), Quaternion.identity);
            
           
        }
    }

     private void MouvementsJoueur()
    {
        float posHorizontal = Input.GetAxis("Horizontal");
        float posVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(posHorizontal, posVertical, 0);
        transform.Translate(direction * Time.deltaTime * _speed);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.2f, 4.2f), Mathf.Clamp(transform.position.y, -4.1f, -1.40f), 0f);

    }
}
