using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float destroyTime = 1f;
        Destroy(gameObject, destroyTime);
    }

    
}
