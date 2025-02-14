using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoweredUpManager : MonoBehaviour
{
    public bool forceFieldActivated;

    public bool freezeHoopActivated;

    public bool moneyBallActivated;
    public bool isGameActive;

    private PowerUpSpawnManager powerUpSpawnManagerScript;
    private MoveBasketball moveBasketballScript;
    private MoveStars moveStarsScript;
    private ChangeColor[] changeColorInstances;
    [SerializeField] private Material blackRedMaterial;
    [SerializeField] private Material courtWhite;
    [SerializeField] private Material courtGreen;
    [SerializeField] private Material courtOrange;
    [SerializeField] private Material courtCyan;
    private Material courtOriginal;
    
    private int livesRemaining = 3;
    private const int MAX_LIVES = 5;
    private int score;
    public float starsSpeed = 0.5f;
    public float starsSpawnRate = 0.5f;
    public float moneyBallRate = 5;
    public float freezeHoopRate = 10;
    public float forceFieldRate = 5;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public Transform heartContainer;
    public GameObject heart;
    public List<GameObject> hearts = new List<GameObject>();

    public GameObject forceField;

    public GameObject obstacleSpawnManager;

    public GameObject court;

    private Renderer courtRenderer;
    // Start is called before the first frame update
    void Start()
    {
        forceFieldActivated = false;

        freezeHoopActivated = false;

        moneyBallActivated = false;

        isGameActive = true;
        changeColorInstances = FindObjectsOfType<ChangeColor>();
        powerUpSpawnManagerScript = GameObject.Find("Power Up Spawn Manager").GetComponent<PowerUpSpawnManager>();
        moveBasketballScript = GameObject.Find("Basketball").GetComponent<MoveBasketball>();
        scoreText.text = "Score: " + score;
        courtRenderer = court.GetComponent<Renderer>();
        courtOriginal = courtRenderer.material;
        InitializeHearts();
    }

    private void Update()
    {
        if (livesRemaining == 0)
        {
            GameOver();
        }
    }

    void InitializeHearts()
    {
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }
        hearts.Clear();
        
        Debug.Log(livesRemaining);
        for (int i = 0; i < livesRemaining; i++)
        {
            GameObject life = Instantiate(heart, heartContainer);
            RectTransform rectTransform = life.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(i * 63f, 0);
            Debug.Log(rectTransform.anchoredPosition);
            hearts.Add(life);
        }
        
    }

    void freezeColors(Color newColor)
    {
        foreach (ChangeColor instance in changeColorInstances)
        {
            instance.StopCoroutine("ChangeColorsOverTime");
            instance.objectRenderer.material.color = newColor;
        }
    }

    void unFreezeColors()
    {
        foreach (ChangeColor instance in changeColorInstances)
        {
            instance.StartCoroutine("ChangeColorsOverTime");
        }
    }

    public IEnumerator FreezeHoopCountdown()
    {
        powerUpSpawnManagerScript.spawnReady = false;
        freezeColors(Color.green);
        changeCourtMaterial(courtGreen);
        yield return new WaitForSeconds(freezeHoopRate);
        unFreezeColors();
        changeCourtMaterial(courtOriginal);
        freezeHoopActivated = false;
        powerUpSpawnManagerScript.spawnReady = true;
    }
    public IEnumerator ForceFieldCountdown()
    {
        powerUpSpawnManagerScript.spawnReady = false;
        forceField.SetActive(true);
        freezeColors(Color.cyan);
        yield return new WaitForSeconds(forceFieldRate);
        unFreezeColors();
        forceFieldActivated = false;
        forceField.SetActive(false);
        powerUpSpawnManagerScript.spawnReady = true;
    }

    public IEnumerator MoneyBallCountdown()
    {
        freezeColors(new Color(1,.5f,0));
        yield return new WaitForSeconds(moneyBallRate);
        unFreezeColors();
        moveBasketballScript.ballRenderer.material = blackRedMaterial;
        moneyBallActivated = false;
    }
    public void SubtractLife()
    {
        if (forceFieldActivated) return;
        livesRemaining--;
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(i < livesRemaining);
        }
        livesText.text = "Lives: " + livesRemaining;
    }

    public void AddLife()
    {
        if (livesRemaining < MAX_LIVES)
        {
            livesRemaining++;
            for (int i = 0; i < hearts.Count; i++)
            {
                hearts[i].SetActive(i < livesRemaining);
            }
            livesText.text = "Lives: " + livesRemaining;
        }
    }
    public void AddScore()
    {
        if (moneyBallActivated)
        {
            if (starsSpawnRate > 0.1f)
            {
                starsSpeed += 0.1f;
                starsSpawnRate -= 0.2f;
            }
            score += 2;
            scoreText.text = "Score: " + score;
        }
        else
        {
            if (starsSpawnRate > 0.1f)
            {
                starsSpeed += 0.05f;
                starsSpawnRate -= 0.1f;
            }
            score++;
            scoreText.text = "Score: " + score;
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        freezeColors(Color.white);
        changeCourtMaterial(courtWhite);
    }

    void changeCourtMaterial(Material newMaterial)
    {
        courtRenderer.material = newMaterial;
    }
}
