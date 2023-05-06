using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrounLogic : MonoBehaviour
{
    private Vector3 _startPosition;
    private float _reloadPositionX;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        //get backgeound width / 2
        _reloadPositionX = gameObject.GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // if background position is pass half of its width, reload position
        if (transform.position.x <= _startPosition.x - _reloadPositionX)
        {
            transform.position = _startPosition;
        }
    }
}
