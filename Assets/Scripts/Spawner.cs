using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesPerWave = 3, waves = 3;
    [SerializeField] private float spawnInterval = 0.7f, timeBetweenWave = 7f;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for(int i = 0; i < waves; i++)
        {
            for (int j = 0; j < enemiesPerWave; j++)
            {
                yield return new WaitForSeconds(spawnInterval);
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
                GameManager.instance.IncreaseEnemiesLeft();
            }
            if (i < waves - 1)
            {
                yield return new WaitForSeconds(timeBetweenWave);
            }
        }
        GameManager.instance.SetAllwavesSpawned();
    }
}
