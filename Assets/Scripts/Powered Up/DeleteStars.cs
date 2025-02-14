using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteStars : MonoBehaviour
{
    //chat gpt code
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        bool isOffScreen = screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;

        if (isOffScreen)
        {
            Destroy(gameObject);
        }
    }
}
