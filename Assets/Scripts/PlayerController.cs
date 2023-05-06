using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Global variables
    [SerializeField] private float speed = 200000.0f;
    private readonly float turnSpeed = 40.0f;
    private float rotateInput = 0;
    private float moveInput = 0;
    [SerializeField] Vector3 _centerOfMass;
    [SerializeField] TextMeshProUGUI _speedText;
    [SerializeField] TextMeshProUGUI _RPMText;
    [SerializeField] List<WheelCollider> _wheels;

    private Rigidbody _playerRidigbody;

    private void Start()
    {
        _playerRidigbody = GetComponent<Rigidbody>();
        _playerRidigbody.centerOfMass = _centerOfMass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move the vehicle
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * MoveInput());
        //Rotate the vehicle
        if (isOnGround())
        {
            moveInput = Input.GetAxis("Vertical");
            rotateInput = Input.GetAxis("Horizontal");

            _playerRidigbody.AddRelativeForce(Vector3.forward * speed * moveInput);
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * rotateInput);

            int speedText = (int)(_playerRidigbody.velocity.magnitude * 3.6f);
            _speedText.SetText("Speed: " + speedText + " km/h");
            _RPMText.SetText("RPM: " + (speedText % 30) * 40);
        }
    }

    bool isOnGround()
    {
        foreach (WheelCollider wheel in _wheels)
            if (!wheel.isGrounded) return false;
        return true;
    }
}
