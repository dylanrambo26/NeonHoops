using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStars : MonoBehaviour
{
    [SerializeField] private float speed;
    private PoweredUpManager poweredUpManagerScript;

    private void Start()
    {
        poweredUpManagerScript = GameObject.Find("GameManager").GetComponent<PoweredUpManager>();
    }

    void FixedUpdate()
    {
        
        transform.Translate(Vector3.back * poweredUpManagerScript.starsSpeed);
    }
    
}
