using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{

    [SerializeField] private float aimSpeed = 15f;
    [SerializeField] private Transform playerTransform;
    private Camera mainCamera;
    private Vector2 mouseWorldPosition;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPosition - (Vector2)playerTransform.position;
        transform.right = Vector2.MoveTowards(transform.right, direction, aimSpeed);
    }
}
