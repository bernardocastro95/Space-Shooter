using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawn = false;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _asteroidPrefab;
    [SerializeField]
    private GameObject _asteroidContainer;


    // Start is called before the first frame update
    void Start()
    {
        Coroutines();
    }

    public void Coroutines()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnAsteroidRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawn == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawn == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    IEnumerator SpawnAsteroidRoutine()
    {
        while (_stopSpawn == false)
        {
            Vector3 posToSpwan = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, posToSpwan, Quaternion.identity);
            newAsteroid.transform.parent = _asteroidContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
        
    }
    

    public void onPlayerDeath()
    {
        _stopSpawn = true;
    }
}
