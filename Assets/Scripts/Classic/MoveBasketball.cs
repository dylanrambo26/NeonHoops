using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShootBasketball : MonoBehaviour
{
    private Rigidbody ballRb;

    [SerializeField] private float ballForce;

    [SerializeField] private Vector3 ballDirection;
    [SerializeField] private Vector3 ballRotation;

    private GameManager gameManagerScript;
    private bool ballReady;
    private AudioSource playerAudio;
    public AudioClip swoosh;
    
    private Vector3 resetPos = new Vector3(-7.69f, 0, -10.63f);

    private Quaternion resetRotation;
    // Start is called before the first frame update
    void Start()
    {
        ballReady = true;
        ballRb = GetComponent<Rigidbody>();
        ballRb.constraints = RigidbodyConstraints.FreezePositionY;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAudio = GameObject.Find("Audio").GetComponent<AudioSource>();
        resetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        ShootBall();
    }

    void ShootBall()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ballReady && gameManagerScript.isGameActive)
        {
            ballRb.constraints = RigidbodyConstraints.None;
            ballRb.AddForce(ballDirection * ballForce, ForceMode.Impulse);
            ballRb.AddTorque(ballRotation);
            ballReady = false;
            playerAudio.PlayOneShot(swoosh, 1.0f);
        }
    }

    void ResetBall()
    {
        if (ballReady)
        {
            transform.position = resetPos;
            transform.rotation = resetRotation;
            ballRb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ballReady = true;
            ResetBall();
        }
    }
    
}
