using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody _enemyRigidbody;

    private float _mapBounds = -5.0f;
    public float _speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _enemyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = (_player.transform.position - transform.position).normalized;
        _enemyRigidbody.AddForce(moveDirection * _speed);

        if (transform.position.y < _mapBounds)
        {
            Destroy(gameObject);
        }
    }
}
