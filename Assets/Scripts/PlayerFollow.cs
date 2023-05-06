using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 viewType1 = new Vector3(0, 5, -8);
    [SerializeField] private Vector3 viewType2 = new Vector3(0, 2.2f, 2.8f);
    private bool isViewType1 = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isViewType1)
        {
            transform.position = player.transform.position + viewType2;
        }
        else
        {
            transform.position = player.transform.position + viewType1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isViewType1 = !isViewType1;
        }
    }
}
