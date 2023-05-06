using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    float animalRange = 30;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > animalRange || transform.position.x > animalRange)
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
        if (transform.position.z < -animalRange || transform.position.x < -animalRange)
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
    }
}
