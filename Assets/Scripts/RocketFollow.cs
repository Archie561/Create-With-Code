using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFollow : MonoBehaviour
{
    private GameObject _target;
    private Rigidbody _rocketRigidbody;

    private float _pushBack = 10.0f;
    private float _speed = 3.0f;
    private float _lifetime = 2.0f;

    void Start()
    {
        _rocketRigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, _lifetime);
    }

    void Update()
    {
        _rocketRigidbody.AddForce((_target.transform.position - transform.position) * _speed);
    }

    public void FollowTarget(GameObject target)
    {
        _target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.StartsWith("Enemy"))
        {
            collision.rigidbody.AddForce(transform.forward * _pushBack * 100);
            Destroy(gameObject);
        }
    }
}
