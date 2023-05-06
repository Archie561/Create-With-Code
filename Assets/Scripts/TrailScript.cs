using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _mouseTrail;

    private Camera _camera;
    private GameManager _gameManager;

    private float _distanceFromCamera = 5.0f;

    void Start()
    {
        _mouseTrail.SetActive(false);
        _camera = GetComponent<Camera>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _gameManager.isGameActive && !_gameManager.isGamePaused)
        {
            _mouseTrail.SetActive(true);
            _mouseTrail.transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distanceFromCamera));
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _mouseTrail.SetActive(false);
        }
    }
}
