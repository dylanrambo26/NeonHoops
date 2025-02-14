using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MovePowerup : MonoBehaviour
{
    [SerializeField] private float speed;

    private bool isAsteroid;

    private float rotateX;

    private float rotateY;

    private float rotateZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {/*
        if (isAsteroid)
        {
            transform.Rotate(30 * Time.deltaTime, 30 * Time.deltaTime, 30 * Time.deltaTime);
        }*/
        transform.Translate(Vector3.back * speed);
        if (gameObject.transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }
/*
    private void OnEnable()
    {
        if (gameObject.tag.Equals("Obstacle"))
        {
            isAsteroid = true;
            rotateX = Random.Range(0, 365);
            rotateY = Random.Range(0, 365);
            rotateZ = Random.Range(0, 365);
        }
    }*/
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Power Up Boundary"))
        {
            Debug.Log("Hit boundary");
            Destroy(gameObject);
        }
    }*/
}
