using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
     [SerializeField] private GameObject _prefabEnemy = default;
    [SerializeField] private GameObject _enemyContainer= default;
   
    private bool _stopSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());   
    }


    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (!_stopSpawn)
        {
            Vector3 posSpawn = new Vector3(Random.Range(-4.2f, 4.2f), 7f, 0f);
            GameObject newEnemy = Instantiate(_prefabEnemy, posSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
       

    }

    public void FinPartie()
    {
        _stopSpawn = true;
    }
}
