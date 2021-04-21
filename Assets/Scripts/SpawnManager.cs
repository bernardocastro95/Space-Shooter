﻿using System.Collections;
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
    private GameObject _tripleShotPowerupPrefab;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotPowerupRoutine());
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
    IEnumerator SpawnTripleShotPowerupRoutine()
    {
        while(_stopSpawn == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newTripleShotPowerup = Instantiate(_tripleShotPowerupPrefab, postToSpawn, Quaternion.identity);
            newTripleShotPowerup.transform.parent = _tripleShotPowerupPrefab.transform;
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawn = true;
    }
}
