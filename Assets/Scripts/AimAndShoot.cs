using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{

    [SerializeField] private float aimSpeed = 15f;
    [SerializeField] private Transform playerTransform, shootPosition;
    [SerializeField] private GameObject arrowPrefab;
    private Camera mainCamera;
    private Vector2 mouseWorldPosition, direction;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
        Aim();
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject arrow = Instantiate(arrowPrefab,shootPosition.position,shootPosition.rotation);
            arrow.GetComponent<Arrow>().Launch(direction);
        }
    }

    private void Aim()
    {
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = (mouseWorldPosition - (Vector2)playerTransform.position).normalized;
        transform.right = Vector2.MoveTowards(transform.right, direction, aimSpeed);
    }
}
