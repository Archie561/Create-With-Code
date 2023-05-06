using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefabs;
    [SerializeField]
    private GameObject[] _powerupPrefabs;
    [SerializeField]
    private GameObject _bossPrefab;

    private float _spawnBoundsXZ = 7.0f;
    private int _enemyAmount = 1;
    private int _enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(_enemyAmount);
        SpawnRandomPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyCount = FindObjectsOfType<EnemyController>().Length;

        if (_enemyCount == 0 && !PlayerController.gameOver)
        {
            _enemyAmount++;
            if (_enemyAmount % 5 == 0)
            {
                SpawnBoss();
                StartCoroutine(SpawnMinion());
            }
            else
            {
                SpawnEnemyWave(_enemyAmount);
            }
            SpawnRandomPowerup();
        }
    }

    IEnumerator SpawnMinion()
    {
        while (GameObject.FindGameObjectWithTag("EnemyBoss"))
        {
            SpawnEnemyWave(1);
            yield return new WaitForSeconds(5);
        }
    }

    void SpawnEnemyWave(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, _enemyPrefabs.Length);
            Instantiate(_enemyPrefabs[index], GenerateSpawnPosition(_enemyPrefabs[index].transform.position.y), _enemyPrefabs[index].transform.rotation);
        }
    }

    void SpawnBoss()
    {
        Instantiate(_bossPrefab, GenerateSpawnPosition(_bossPrefab.transform.position.y), _bossPrefab.transform.rotation);
    }

    void SpawnRandomPowerup()
    {
        int index = Random.Range(0, _powerupPrefabs.Length);
        Instantiate(_powerupPrefabs[index], GenerateSpawnPosition(_powerupPrefabs[index].transform.position.y), _powerupPrefabs[index].transform.rotation);
    }

    private Vector3 GenerateSpawnPosition(float positionY)
    {
        float postionX = Random.Range(-_spawnBoundsXZ, _spawnBoundsXZ);
        float postionZ = Random.Range(-_spawnBoundsXZ, _spawnBoundsXZ);

        return new Vector3(postionX, positionY, postionZ);
    }
}
