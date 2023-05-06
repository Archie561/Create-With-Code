using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _playerAnimator;
    private Rigidbody _playerRigidbody;
    private MoveLeft _moveLeftScript;

    [SerializeField] private AudioSource _hitObstacleSound;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private ParticleSystem _dirtParticle;

    private float _jumpForce = 800.0f;
    private float _gravityMultilpier = 2f;
    private float _speed;
    private float _preStartSpeed;
    private float _animationSpeed;
    private float _preStartAnimationSpeed;
    private float _startGamePosition = 2.5f;

    private bool _isOnGround = true;
    private bool _isSecondJumpUsed;
    public bool gameOver;
    public bool isGameStarted;

    // Start is called before the first frame update
    void Start()
    {
        _hitObstacleSound = gameObject.GetComponent<AudioSource>();
        _playerAnimator = gameObject.GetComponent<Animator>();
        _playerRigidbody = gameObject.GetComponent<Rigidbody>();
        _moveLeftScript = GameObject.Find("Background").GetComponent<MoveLeft>();

        _speed = _moveLeftScript.speed;
        _preStartSpeed = _speed / 5f;
        _animationSpeed = _playerAnimator.speed;
        _preStartAnimationSpeed = _animationSpeed / 1.5f;

        //changing gravity force
        Physics.gravity *= _gravityMultilpier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
        {
            _dirtParticle.Stop();
            _playerAnimator.speed = _preStartAnimationSpeed;
            transform.Translate(Vector3.right * _preStartSpeed * Time.deltaTime, Space.World);
            if (transform.position.x >= _startGamePosition)
            {
                transform.position = new Vector3(_startGamePosition, transform.position.y, transform.position.z);
                _playerAnimator.speed = _animationSpeed;
                _dirtParticle.Play();
                isGameStarted = true;
            }
        }
        else
        {
            //if condition is true, play jump animation, add force for a jump and stop dirt particle animation
            if ((Input.GetKeyDown(KeyCode.Space)) && (_isOnGround || !_isSecondJumpUsed) && !gameOver)
            {
                //hardcoded
                _playerAnimator.speed = _animationSpeed;
                _moveLeftScript.speed = _speed;

                _playerRigidbody.velocity = Vector3.zero;
                _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _dirtParticle.Stop();

                if (_isOnGround)
                {
                    _isOnGround = false;
                    _playerAnimator.SetTrigger("Jump_trig");
                }
                else
                {
                    _isSecondJumpUsed = true;
                    _playerAnimator.Play("Running_Jump", 3, 1.0f);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && _isOnGround && !gameOver)
            {
                float speedBoost = 10.0f;
                float animationSpeedBoost = 0.5f;

                _moveLeftScript.speed += speedBoost;
                _playerAnimator.speed += animationSpeedBoost;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _playerAnimator.speed = _animationSpeed;
                _moveLeftScript.speed = _speed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if player touches the ground, isOnGround = true, if !gameover, start dirtParticle animation
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
            _isSecondJumpUsed = false;
            if (!gameOver)
            {
                _dirtParticle.Play();
            }
        }
        //if player hits obstacle
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //gameOver = true, show Game Over message
            gameOver = true;
            GameObject.Find("UI").GetComponent<UIScript>().ShowGameOverMessage();

            //Stop background music
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            //play hit SFX
            _hitObstacleSound.Play();

            //play explosion animation
            _explosionParticle.Play();
            //stop dirt particle animation
            _dirtParticle.Stop();

            //play death animation
            _playerAnimator.SetBool("Death_b", true);
            _playerAnimator.SetInteger("DeathType_int", 1);
        }
    }
}
