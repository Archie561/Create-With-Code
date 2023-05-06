using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _obstaclePrefabs;
    private PlayerController _playerController;
    private float _spawnPositionX = 25.0f;
    private float _startSpawnDelay = 4.0f;
    private float _spawnPeriod = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //invoke SpawnObstacle method with delay and repeating value
        InvokeRepeating("SpawnObstacle", _startSpawnDelay, _spawnPeriod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        //while game is not over
        if (!_playerController.gameOver)
        {
            //create an obstacle
            int index = Random.Range(0, _obstaclePrefabs.Length);
            Vector3 spawnPosition = new Vector3(_spawnPositionX, _obstaclePrefabs[index].transform.position.y, _obstaclePrefabs[index].transform.position.z);
            Instantiate(_obstaclePrefabs[index], spawnPosition, _obstaclePrefabs[index].transform.rotation);
        }
    }
}
