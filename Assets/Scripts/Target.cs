using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _destroyParticles;

    private Rigidbody _targetRigidbody;
    private GameManager _gameManager;

    private float _spawnBound = 4.0f;
    private float _minimalForce = 11.0f;
    private float _maximalForce = 14.0f;

    private int _foodPoint = 5;
    private int _bombPoint = -10;

    void Start()
    {
        _targetRigidbody = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        transform.position = GetRandomPosition();
        _targetRigidbody.AddForce(Vector3.up * GetRandomForce(), ForceMode.Impulse);
        _targetRigidbody.AddTorque(GetRandomForce(), GetRandomForce(), GetRandomForce());
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sensor")
        {
            Destroy(gameObject);

            if (CompareTag("Food") && _gameManager.isGameActive)
            {
                _gameManager.UpdateScore(-_foodPoint);
                _gameManager.UpdateLives(-1);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _gameManager.isGameActive && !_gameManager.isGamePaused)
        {
            Destroy(gameObject);
            Instantiate(_destroyParticles, transform.position, transform.rotation);

            if (CompareTag("Bomb"))
            {
                _gameManager.UpdateScore(_bombPoint);
                _gameManager.UpdateLives(-1);
            }
            else if (CompareTag("Food"))
            {
                _gameManager.UpdateScore(_foodPoint);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-_spawnBound, _spawnBound), -_spawnBound);
    }

    float GetRandomForce()
    {
        return Random.Range(_minimalForce, _maximalForce);
    }
}
