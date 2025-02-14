using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOneSpawnManager : MonoBehaviour
{
    public GameObject plusOneLife;

    private float startDelay = 30;

    private float spawnInterval = 20;

    private float spawnRangeX = 10.58f;
    
    // Start is called before the first frame update
    private PoweredUpManager poweredUpManagerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        poweredUpManagerScript = GameObject.Find("GameManager").GetComponent<PoweredUpManager>();
        StartCoroutine("SpawnPlusOnes");
    }
    IEnumerator SpawnPlusOnes()
    {
        yield return new WaitForSeconds(startDelay);
        while (poweredUpManagerScript.isGameActive)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,-spawnRangeX + 5), 12.32f, -4.503f);
            Instantiate(plusOneLife, spawnPos, plusOneLife.transform.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
