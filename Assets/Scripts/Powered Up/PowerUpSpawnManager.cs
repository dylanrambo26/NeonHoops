using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    public GameObject[] powerUps;

    private float startDelay = 5;

    private float spawnInterval = 5;

    private float spawnRangeX = 10.58f;

    public bool spawnReady = true;

    private PoweredUpManager poweredUpManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        poweredUpManagerScript = GameObject.Find("GameManager").GetComponent<PoweredUpManager>();
        StartCoroutine("SpawnPowerups");
        //InvokeRepeating("SpawnRandomPowerup", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void SpawnRandomPowerup()
    {
        if (spawnReady)
        {
            int powerUpIndex = Random.Range(0, powerUps.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,-spawnRangeX + 5), 12.32f, -4.503f);
            Instantiate(powerUps[powerUpIndex], spawnPos, powerUps[powerUpIndex].transform.rotation);
        }
        
    }*/
    IEnumerator SpawnPowerups()
    {
        yield return new WaitForSeconds(startDelay);
        while (poweredUpManagerScript.isGameActive)
        {
            if (spawnReady)
            {
                int powerUpIndex = Random.Range(0, powerUps.Length);
                Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,-spawnRangeX + 5), 12.32f, -4.503f);
                Instantiate(powerUps[powerUpIndex], spawnPos, powerUps[powerUpIndex].transform.rotation);
                yield return new WaitForSeconds(spawnInterval);
            }
            
        }
    }
}
