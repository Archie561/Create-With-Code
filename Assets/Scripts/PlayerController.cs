using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _powerupIndicator;

    [SerializeField]
    private GameObject _rocketPrefab;

    private Rigidbody _playerRigidbody;
    private GameObject _focalPoint;
    private Vector3 _powerupIndicatorOffset = new Vector3(0, -0.5f, 0);

    private float _speed = 20.0f;
    private float _jumpForce = 250.0f;
    private float _enemyType2PushBackForce = 15.0f;
    private float _powerupPushBackForce = 15.0f;
    private float _powerupDuration = 7.0f;
    private float _mapBounds = -5.0f;
    private string _activePowerup;

    public static bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("FocalPoint");
        _activePowerup = "None";
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        _playerRigidbody.AddForce(_focalPoint.transform.forward * _speed * verticalInput);

        _powerupIndicator.transform.position = transform.position + _powerupIndicatorOffset;

        if (Input.GetKeyDown(KeyCode.Space) && _activePowerup == "PowerupRocket")
        {
            SpawnRockets();
        }

        if (Input.GetKeyDown(KeyCode.Space) && _activePowerup == "PowerupSmash")
        {
            StartCoroutine(SmashJump());
        }

        if (transform.position.y < _mapBounds)
        {
            Destroy(gameObject);
            _powerupIndicator.SetActive(false);
            gameOver = true;
        }
    }

    IEnumerator PowerupCountdownRoutine(string powerupType)
    {
        _activePowerup = powerupType;
        _powerupIndicator.SetActive(true);

        yield return new WaitForSeconds(_powerupDuration);

        _activePowerup = "None";
        _powerupIndicator.SetActive(false);
    }
    IEnumerator SmashJump()
    {
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        _playerRigidbody.AddForce(Vector3.down * 2 * _jumpForce, ForceMode.Impulse);
        MakeExplosionVave();
    }   

    void SpawnRockets()
    {
        EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();
        
        for (int i = 0; i < enemies.Length; i++)
        {
            Instantiate(_rocketPrefab, transform.position, transform.rotation).GetComponent<RocketFollow>().FollowTarget(enemies[i].gameObject);
        }
    }

    void MakeExplosionVave()
    {
        EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.GetComponent<Rigidbody>().AddForce((enemies[i].gameObject.transform.position - transform.position) * _powerupPushBackForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("Powerup"))
        {
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine(other.tag));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.StartsWith("Enemy") && _activePowerup == "Powerup") //Bouncy powerup
        {
            Vector3 moveDirection = collision.transform.position - transform.position;
            collision.rigidbody.AddForce(moveDirection * _powerupPushBackForce, ForceMode.Impulse);
        }

        else if (collision.gameObject.CompareTag("EnemyType2"))
        {
            Vector3 moveDirection = transform.position - collision.transform.position;
            _playerRigidbody.AddForce(moveDirection * _enemyType2PushBackForce, ForceMode.Impulse);
        }
    }
}
