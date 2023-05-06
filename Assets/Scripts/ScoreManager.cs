using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _socreAmount = 1;
    private UIScript _userInterface;
    [SerializeField] private AudioSource _passObstacleSound;

    // Start is called before the first frame update
    void Start()
    {
        _userInterface = GameObject.Find("UI").GetComponent<UIScript>();
        _passObstacleSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if obstacle touches score manager
        if(other.CompareTag("Obstacle"))
        {
            //add scoreAmount value to score
            _userInterface.AddScore(_socreAmount);
            //play sound of obstacle pass
            _passObstacleSound.Play();
        }
    }
}
