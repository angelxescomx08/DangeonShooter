using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private int maxHealth = 5;
    SpriteRenderer spriteRenderer;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Arrow>())
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GameManager.instance.DecreaseEnemiesLeft();
        }
        else
        {
            healthbar.UpdateHealthbar(maxHealth, health);
            // Start blinking
            StartCoroutine(Blink(0.1f));
        }
    }

    private IEnumerator Blink(float blinkTime)
    {   
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(blinkTime);
        spriteRenderer.color = Color.white;
    }
}
