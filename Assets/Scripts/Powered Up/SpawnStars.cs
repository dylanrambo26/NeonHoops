using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnStars : MonoBehaviour
{
    [SerializeField] private GameObject star;
    private PoweredUpManager poweredUpManagerScript;

    private float spawnRangeX = 180;

    private float spawnRangeY = 8;

    private float spawnRangeZ = 115f;
    private float startDelay = 1;

    public float spawnInterval = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        poweredUpManagerScript = GameObject.Find("GameManager").GetComponent<PoweredUpManager>();
        InvokeRepeating("randomSpawnStars", startDelay, spawnInterval);    
    }

    // Update is called once per frame

    void randomSpawnStars()
    {
        spawnInterval = poweredUpManagerScript.starsSpawnRate;
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,spawnRangeX-10), Random.Range(-spawnRangeY,spawnRangeY + 72), spawnRangeZ);
        Instantiate(star, spawnPos, star.transform.rotation);
    }

    
}
