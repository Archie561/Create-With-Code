using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerController : MonoBehaviour
{
    //Global variables
    [SerializeField] private float speed = 40.0f;
    [SerializeField] private float turnSpeed = 60.0f;
    private float rotateInput = 0;
    private float moveInput = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move the vehicle
        transform.Translate(Vector3.forward * Time.deltaTime * speed * MoveInput());
        //Rotate the vehicle
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * RotateInput());
    }

    float MoveInput()
    {
        //while W button is pressed
        if (Input.GetKey(KeyCode.W))
        {
            //if moveInput is less then 1, then increase it, else: set to 1
            moveInput = moveInput < 1 ? moveInput += 0.01f : 1;
            return moveInput;
        }
        //while S button is pressed
        else if (Input.GetKey(KeyCode.S))
        {
            //if moveInput is greater then -1, then decrease it, else: set to -1
            moveInput = moveInput > -1 ? moveInput -= 0.01f : -1;
            return moveInput;
        }
        //while no button pressed
        else
        {
            //round moveInput to 0
            if (moveInput < 0.1f && moveInput > -0.1f)
            {
                moveInput = 0;
            }
            else
            {
                //if moveInput is less then 0, then increase it, else: decrease it until it's 0
                moveInput = moveInput < 0 ? moveInput += 0.01f : moveInput -= 0.01f;
            }
            return moveInput;
        }
    }

    //same method for rotateInput
    float RotateInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rotateInput = rotateInput < 1 ? rotateInput += 0.1f : 1;
            return rotateInput;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rotateInput = rotateInput > -1 ? rotateInput -= 0.1f : -1;
            return rotateInput;
        }
        else
        {
            if (rotateInput < 0.3f && rotateInput > -0.3f)
            {
                rotateInput = 0;
            }
            else
            {
                rotateInput = rotateInput < 0 ? rotateInput += 0.1f : rotateInput -= 0.1f;
            }
            return rotateInput;
        }
    }
}
