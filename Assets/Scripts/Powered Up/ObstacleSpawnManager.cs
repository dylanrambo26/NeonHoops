using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnManager : MonoBehaviour
{
    public GameObject[] asteroids;

    private float startDelay = 3;

    private float spawnInterval = 2;

    private float spawnRangeX = 10.58f;
    private PoweredUpManager poweredUpManagerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        poweredUpManagerScript = GameObject.Find("GameManager").GetComponent<PoweredUpManager>();
        StartCoroutine("SpawnObstacles");
        //InvokeRepeating("SpawnRandomObstacle", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void SpawnRandomObstacle()
    {
        int asteroidIndex = Random.Range(0, asteroids.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,-spawnRangeX + 5), 12.18f, -4.503f);
        Instantiate(asteroids[asteroidIndex], spawnPos, asteroids[asteroidIndex].transform.rotation);
    }*/

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(startDelay);
        while (poweredUpManagerScript.isGameActive)
        {
            int asteroidIndex = Random.Range(0, asteroids.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,-spawnRangeX + 5), 12.18f, -4.503f);
            Instantiate(asteroids[asteroidIndex], spawnPos, asteroids[asteroidIndex].transform.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
