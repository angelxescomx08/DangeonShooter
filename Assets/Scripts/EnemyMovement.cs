using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Transform playerTransform;
    private bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        Flip();
    }

    private void Follow()
    {
        Vector2 playerDirection = (playerTransform.position - transform.position).normalized;
        transform.Translate(playerDirection * speed * Time.deltaTime);
    }

    private void Flip()
    {
        bool isPlayerRight = playerTransform.position.x > transform.position.x;
        if ((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            isFacingRight = !isFacingRight;
        }
    }
}
