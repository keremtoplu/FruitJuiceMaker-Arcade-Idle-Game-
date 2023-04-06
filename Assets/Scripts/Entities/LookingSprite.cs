using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingSprite : MonoBehaviour
{
    private Transform mainCamera; 
    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(mainCamera);
    }
}
