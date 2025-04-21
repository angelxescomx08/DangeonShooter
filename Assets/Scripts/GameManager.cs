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

    public void Die()
    {
        isPlayerDead = true;

        lives--;

        UpdateLivesText();

        StopEnemiesSpawn();

        StopEnemies();

        StartCoroutine(WaitAndRestart(0.5f));
    }

    private IEnumerator WaitAndRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isPlayerDead = false;
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
