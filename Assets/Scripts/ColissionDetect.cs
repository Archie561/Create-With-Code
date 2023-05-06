using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColissionDetect : MonoBehaviour
{
    GameManager gameManager;
    HungerBar hungerBar;
    public float satietyValue = 25;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hungerBar = gameObject.GetComponentInChildren<HungerBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.AddLives(-1);
        }
        else
        {
            other.gameObject.SetActive(false);
            if (hungerBar.ChangeHunger(satietyValue))
            {
                gameManager.AddScores(5);
                Destroy(gameObject);
            }
        }
    }
}
