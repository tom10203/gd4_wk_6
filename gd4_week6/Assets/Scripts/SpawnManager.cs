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

    [SerializeField] int noOfFruitOverTimeMin;
    [SerializeField] int noOfFruitOverTimeMax;
    [SerializeField] float fruitOverTimeDelayMin;
    [SerializeField] float fruitOverTimeDelayMax;
    float fruitOverTimeDelay;

    public bool reduceLives = false;
    public float reduceLivesTimer;
    float reduceLivesBuffer = 1.5f;

    UIManager uiManager;
    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        reduceLivesTimer = Time.time;
    }
    void Update()
    {

        if (reduceLives)
        {
            reduceLives = false;
            reduceLivesTimer = Time.time + reduceLivesBuffer;
            uiManager.UpdateLives(-1);
        }

        if (currentFruit <= 0)
        {
            spawnDelay -= Time.deltaTime;
        }

        if (spawnDelay <= 0) // start spawning fruit - either at once or over time
        {
            int random = Random.Range(0, 2);
            int noOfFruitToSpawn;
            if (random == 0)
            {
                noOfFruitToSpawn = Random.Range(3, 6);
                SpawnMultipleFruitAtOnce(noOfFruitToSpawn);
            }
            else
            {
                noOfFruitToSpawn = Random.Range(noOfFruitOverTimeMin, noOfFruitOverTimeMax);
                StartCoroutine(SpwanMultipleFruitOverTime(noOfFruitToSpawn));
            }
            currentFruit += noOfFruitToSpawn;
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
        
        for (int i = 0; i < noOfFruitToSpawn; i++)
        {
            Instantiate(melon, GenerateRandomSpawnPoint(), transform.rotation);
        }

        int noOfBombsToSpawn = Random.Range(0, 3);
        SpawnBomb(noOfBombsToSpawn);
    }

    IEnumerator SpwanMultipleFruitOverTime(int noOfFruitToSpawn)
    {
        int fruitSpawned = 0;
        while (fruitSpawned < noOfFruitToSpawn)
        {
            int noToSpawnThisFrame = Random.Range(0, 4);
            fruitSpawned += noToSpawnThisFrame;
            for (int i = 0;i < noToSpawnThisFrame; i++)
            {
                Instantiate(melon, GenerateRandomSpawnPoint(), transform.rotation);
            }
            float timeDelay = Random.Range(fruitOverTimeDelayMin, fruitOverTimeDelayMax);
            yield return new WaitForSeconds(timeDelay);
        }
        
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
