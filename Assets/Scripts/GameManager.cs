using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPlayerDead { get; private set; } = false;
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Die()
    {
        isPlayerDead = true;

        StopEnemiesSpawn();

        StopEnemies();

        StartCoroutine(WaitAndRestart(0.5f));
    }

    private IEnumerator WaitAndRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    private static void StopEnemiesSpawn()
    {
        Spawner spawner = FindObjectOfType<Spawner>();
        if (spawner != null)
        {
            spawner.StopAllCoroutines();
        }
    }

    private static void StopEnemies()
    {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();

        foreach (EnemyMovement enemy in enemies)
        {
            enemy.StopMovement();
        }
    }
}
