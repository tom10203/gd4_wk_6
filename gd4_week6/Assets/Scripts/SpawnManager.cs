using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject melon;
    public GameObject bomb;
    public Vector3 spawnPoint;

    public float spawnRangeX = 7f;

    public int currentFruit = 0;
    public float spawnDelay = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFruit <= 0)
        {
            spawnDelay -= Time.deltaTime;
        }

        if (spawnDelay <= 0)
        {
            SpawnMultipleFruitAtOnce(5);
            spawnDelay = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(melon, GenerateRandomSpawnPoint(), transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(bomb, GenerateRandomSpawnPoint(), transform.rotation);
        }
    }

    void SpawnMultipleFruitAtOnce(int noOfFruitToSpawn)
    {
        currentFruit += noOfFruitToSpawn;
        for (int i = 0; i < noOfFruitToSpawn; i++)
        {
            Instantiate(melon, GenerateRandomSpawnPoint(), transform.rotation);
        }

        int noOfBombsToSpawn = Random.Range(0, 3);
        SpawnBomb(noOfBombsToSpawn);
    }

    void SpwanMultipleFruitOverTime(int noOfFruitToSpawn)
    {

    }

    void SpawnBomb(int noOfBombToSpawn)
    {
        for (int i = 0;i < noOfBombToSpawn;i++)
        {
            Instantiate(bomb, GenerateRandomSpawnPoint(), transform.rotation);
        }
    }

    Vector3 GenerateRandomSpawnPoint()
    {
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
        spawnPoint = new Vector3(spawnX, -0.5f, transform.position.z - 1.5f);
        return spawnPoint;  
    }
}
