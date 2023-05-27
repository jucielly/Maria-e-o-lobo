using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    
    [SerializeField]
    private int _lives = 3;
    private bool _isDead;
    

    //ui
    public GameObject  gameOverUi;
    public GameObject finishGameUi;
    
    //movement
    [SerializeField]
    private float _speed = 4f;
    bool facingRight = true;


    //sprites
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    //fxs
    public AudioSource jumpSound;
    public AudioSource shootingSound;
    public AudioSource damageSound;
    public AudioSource cherryColectSound;


    //jump
    public LayerMask groundLayer;
    public Transform feetPosition;
    public float jumpTimeCounter;
    private Rigidbody2D _rigidBody;
    private bool _isJumping;
    private bool _isGrounded;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _groundCheckCircle = 0.4f;
    [SerializeField]
    private float _jumpTime = 0.35f;
    


    //shooting
    public GameObject shootingPrefab;
    public Transform launchOffset;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector2(-2.45f, -1.95f);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        caulculateMovement();
        Jump();
        Fire();
    }
    void caulculateMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float newInputX = inputX < 0 ? inputX * -1 : inputX;
        Vector2 direction = new Vector2(newInputX, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

       if(inputX > 0 && !facingRight)
        {
            //print("direita");
            Flip();
          
        }
        if(inputX < 0 && facingRight)
        {
            //print("esquerda");
            Flip();
            
            
        }

        _animator.SetFloat("startMove", Mathf.Abs(newInputX));
       
    }


    void Flip()
    {
        //Vector3 currentScale = gameObject.transform.localScale;
        //currentScale.x *= -1;
        //gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
       
    }

    void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPosition.position, _groundCheckCircle, groundLayer);

        if(_isGrounded && Input.GetButtonDown("Jump")){
            _isJumping = true;
            jumpTimeCounter = _jumpTime;
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            jumpSound.Play();
        }

        if (Input.GetButtonDown("Jump") && _isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                jumpSound.Play();
                _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }


            if (Input.GetButtonUp("Jump"))
            {
                _isJumping = false;
            }

        }
    }


    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shootingSound.Play();

            Instantiate(shootingPrefab, launchOffset.position, launchOffset.rotation);
        }
        
    }

    public void GameOver()
    {
        gameOverUi.SetActive(true);
        Destroy(this.gameObject);
    }
    public void Damage()
    {
        _lives = _lives - 1;
        HudScript.lives--;
        damageSound.Play();

        if (_lives < 1 && !_isDead )
        {
            _isDead = true;
            GameOver();
        }
    }

    public void LifeRecovery()
    {
        HudScript.lives++;
        _lives = _lives + 1;
        cherryColectSound.Play();
    }
    public void FinishGame()
    {
        finishGameUi.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "endGame")
        {
            FinishGame();
            Destroy(this.gameObject);
        }


        if(collision.gameObject.tag == "hole")
        {
            GameOver();
        }
    }
}
