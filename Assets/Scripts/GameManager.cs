using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private int lives = 3;
    public bool isPlayerDead { get; private set; } = false;
    public static GameManager instance { get; private set; }

    private int enemiesLeft = 0;
    private bool allWavesSpawned = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void IncreaseEnemiesLeft()
    {
        enemiesLeft++;
    }

    public void DecreaseEnemiesLeft()
    {
        enemiesLeft--;
        if (enemiesLeft <= 0 && allWavesSpawned)
        {
            //pasar de nivel
            LoadNextScene();
        }
    }

    public void SetAllwavesSpawned()
    {
        allWavesSpawned = true;
    }

    private void LoadNextScene()
    {
        Reset();
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    public void Die()
    {
        isPlayerDead = true;

        lives--;

        UpdateLivesText();

        StopEnemiesSpawn();

        StopEnemies();

        StartCoroutine(WaitAndRestart(0.5f));
    }

    private void Reset()
    {
        enemiesLeft = 0;
        allWavesSpawned = false;
        isPlayerDead = false;
    }

    private IEnumerator WaitAndRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Reset();
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
