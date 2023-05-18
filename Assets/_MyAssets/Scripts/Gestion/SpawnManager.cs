using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabEnemy = default;
    [SerializeField] private GameObject _prefabEnemy2 = default;
    [SerializeField] private GameObject _prefabEnemy3 = default;
    [SerializeField] private GameObject _enemyContainer= default;
    [SerializeField] private GameObject _PUPrefabs = default;
   
    private bool _stopSpawn = false;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPURoutine());   
    }

    IEnumerator SpawnPURoutine()
    {
        yield return new WaitForSeconds(3f);
        while (!_stopSpawn)
        {
            Vector3 posSpawn = new Vector3(Random.Range(-4.2f, 4.2f), 7f, 0f);
            GameObject newPU = Instantiate(_PUPrefabs, posSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8,15));
        }
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (!_stopSpawn)
        {    
        if (_uiManager.getScore() < 1000)
        {
            Vector3 posSpawn = new Vector3(Random.Range(-4.2f, 4.2f), 7f, 0f);
            GameObject newEnemy = Instantiate(_prefabEnemy, posSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
        else if (_uiManager.getScore() < 1500 )
        {
             Vector3 posSpawn = new Vector3(Random.Range(-4.2f, 4.2f), 7f, 0f);
            
            GameObject newEnemy2 = Instantiate(_prefabEnemy2, posSpawn, Quaternion.identity);
           
            newEnemy2.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
        else
        {
             Vector3 posSpawn = new Vector3(Random.Range(-4.2f, 4.2f), 7f, 0f);
            
            GameObject newEnemy3 = Instantiate(_prefabEnemy3, posSpawn, Quaternion.identity);
           
            newEnemy3.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
        }
       

    }

    public void FinPartie()
    {
        _stopSpawn = true;
    }
}
