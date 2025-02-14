using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float currentTime = 60;
    public float startTime = 3;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public float speed;
    private bool isAlreadyCalled;
    private bool isClassicMode;
    private AudioSource playerAudio;
    private AudioSource gameMusic;
    public AudioClip startTimerSound;
    public AudioClip startAirHorn;
    public AudioClip gameOverBuzzer;
    public AudioClip gameMusicClip;
    public AudioClip gameOverMusicClip;
    private AudioSource gameOverMusic;
    private AudioSource titleMusic;

    [SerializeField] private GameObject titleCamera;
    [SerializeField] private GameObject mainCamera;
    private AudioListener titleCameraListener;
    private AudioListener mainCameraListener;
    private ScoreCounter scoreCounterScript;
    private MoveHoop moveHoopScript;
    private ShootBasketball moveBasketballScript;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private TextMeshProUGUI startCountdownText;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GameObject.Find("Audio").GetComponent<AudioSource>();
        titleMusic = GameObject.Find("Title Music").GetComponent<AudioSource>();
        gameMusic = GameObject.Find("Game Music").GetComponent<AudioSource>();
        gameOverMusic = GameObject.Find("Game Over Music").GetComponent<AudioSource>();
        isAlreadyCalled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timerText.text = time.ToString(@"m\:ss\.ff");
            if (currentTime is < 10 and > 5)
            {
                timerText.color = new Color(255, 188, 0, 255);
            }

            if (currentTime is < 5 and > 0)
            {
                timerText.color = new Color(255, 0, 0, 255);
            }
        }
        else if (currentTime < 0 && !isAlreadyCalled)
        {
            GameOver();
            isAlreadyCalled = true;
        }
    }

    IEnumerator StartCountdown()
    {
        isGameActive = false;
        int i = 3;
        while (i > 0)
        {
            playerAudio.PlayOneShot(startTimerSound, 1.0f);
            startCountdownText.text = i + "";
            yield return new WaitForSeconds(1);
            i--;
        }
        startCountdownText.color = Color.green;
        startCountdownText.text = "GO!";
        
        playerAudio.PlayOneShot(startAirHorn, 1.0f);
        yield return new WaitForSeconds(0.5f);
        startCountdownText.gameObject.SetActive(false);
        Debug.Log("Finished");
        isGameActive = true;
    }

    public void GameOver()
    {
        playerAudio.PlayOneShot(gameOverBuzzer, 1.0f);
        gameMusic.Stop();
        gameOverMusic.PlayOneShot(gameOverMusicClip, 0.5f);
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
        scoreCounterScript.DisplayFinalScore(!isGameActive);
    }

    public void StartClassicGame(int difficulty)
    {
        isClassicMode = true;
        SwitchMusicTrack(isClassicMode);
        startCountdownText.gameObject.SetActive(true);
        StartCoroutine(StartCountdown());
        StopCoroutine(StartCountdown());
        scoreCounterScript = GameObject.Find("Basketball").GetComponent<ScoreCounter>();
        moveHoopScript = GameObject.Find("Hoop 2").GetComponent<MoveHoop>();
        mainCameraListener = mainCamera.GetComponent<AudioListener>();
        titleCameraListener = titleCamera.GetComponent<AudioListener>();


        switch (difficulty)
        {
            case 1:
                moveHoopScript.speed = 0.05f;
                break;
            case 2:
                moveHoopScript.speed = 0.10f;
                break;
            case 3:
                moveHoopScript.speed = 0.15f;
                break;
        }

        titleScreen.SetActive(false);
        ChangeCamera();
        scoreCounterScript.scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
    }

    public void StartPoweredUpGame()
    {
        isClassicMode = false;
        SceneManager.LoadScene(1);
        Debug.Log("Loaded Scene");
    }

    private void ChangeCamera()
    {
        titleCameraListener.enabled = false;
        mainCameraListener.enabled = true;
        titleCamera.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SwitchMusicTrack(bool isClassic)
    {
        titleMusic.Stop();
        if (isClassic)
        {
            gameMusic.PlayOneShot(gameMusicClip, 0.75f);
        }
    }

}
