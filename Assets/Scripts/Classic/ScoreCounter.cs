using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI finalScoreText;
    private GameManager gameManagerScript;
    private AudioSource playerAudio;
    public AudioClip pointScored;
    public ParticleSystem scoreParticle;
    
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        score = 0;
        scoreText.SetText("Score: " + score);
        playerAudio = GameObject.Find("Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor") && gameManagerScript.isGameActive)
        {
            score++;
            scoreText.SetText("Score: " + score); 
            playerAudio.PlayOneShot(pointScored, 1.0f);
            scoreParticle.Play();
        }
    }

    public void DisplayFinalScore(bool isGameActive)
    {
        finalScoreText.SetText("Final Score: " + score);
    }
}
