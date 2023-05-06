using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMove : MonoBehaviour
{
    [SerializeField] private float speed = 40;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
