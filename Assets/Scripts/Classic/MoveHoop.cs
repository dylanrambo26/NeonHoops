using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHoop : MonoBehaviour
{

    public float speed;

    private bool movingLeft;

    private bool movingRight;

    //private bool isClassicMode;
    // Start is called before the first frame update
    void Start()
    {
        movingLeft = true;
        movingRight = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingLeft)
        {
            transform.Translate(Vector3.left * speed);
        }
        else if (movingRight)
        {
            transform.Translate(Vector3.right * speed);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Left Bound"))
        {
            movingLeft = false;
            movingRight = true;
        }
        else if(other.gameObject.CompareTag("Right Bound"))
        {
            movingLeft = true;
            movingRight = false;
        }
    }
}
