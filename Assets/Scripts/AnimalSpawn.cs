using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawn : MonoBehaviour
{
    public GameObject[] animals = new GameObject[3];
    float horizontalMapBorder = 25;
    float verticalMapBorder = 25;
    float spawnRange = 12;
    float spawnDelay = 2;
    float spawnPeriod = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", spawnDelay, spawnPeriod);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animals.Length);
        
        Vector3[] spawnPosition =
        {
            new Vector3(Random.Range(-spawnRange, spawnRange), transform.position.y, horizontalMapBorder),
            new Vector3(verticalMapBorder, transform.position.y, Random.Range(-spawnRange, spawnRange)),
            new Vector3(Random.Range(-spawnRange, spawnRange), transform.position.y, -horizontalMapBorder),
            new Vector3(-verticalMapBorder, transform.position.y, Random.Range(-spawnRange, spawnRange))
        };

        Quaternion[] spawnRotation =
        {
            Quaternion.AngleAxis(180, Vector3.up),
            Quaternion.AngleAxis(-90, Vector3.up),
            Quaternion.AngleAxis(0, Vector3.up),
            Quaternion.AngleAxis(90, Vector3.up)
        };

        int positionIndex = Random.Range(0, spawnPosition.Length);

        Instantiate(animals[animalIndex], spawnPosition[positionIndex], spawnRotation[positionIndex]);
    }
}