using Cainos.CustomizablePixelCharacter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LumiScript : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    public bool isFollowing = false;
    private Transform followTarget;
    private GameObject player;
    private float _distanceFromTarget = 4.5f;
    private Animator _animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Text lumiFollowsText;
    public float lastFollowedPlayer = 0f;


    //jump
    public LayerMask groundLayer;
    public Transform feetPosition;
    public float jumpTimeCounter;
    private bool _isJumping;
    private bool _isGrounded;
    [SerializeField]
    private float _jumpForce = 2f;
    [SerializeField]
    private float _groundCheckCircle = 0.4f;
    [SerializeField]
    private float _jumpTime = 0.35f;
    private float lastJump = 0;
    




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        followTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        FollowPlayer();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ToogleFollow();
        }


        if (isFollowing)
        {
            lumiFollowsText.text = "Wait";
            
            if(Vector2.Distance(transform.position, followTarget.position) > _distanceFromTarget * 3)
            {


                StartFollowingPlayer();
               
            }
        }
        else
        {

            lumiFollowsText.text = "Follow Me";
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if(collision.tag == "Jump")
        {
            if (lastJump + 1000 > Time.realtimeSinceStartup)
            {
                JumpRight();
                lastJump = Time.realtimeSinceStartup;
            }
          
        }

        if(collision.tag == "Outlimits" && isFollowing)
        {
            transform.position = new Vector2(24.38f, -2.171665f);
            isFollowing = false;
        }
    }

    public void StartFollowingPlayer()
    {

      
        if (lastFollowedPlayer + 2 > Time.realtimeSinceStartup)
        {
            return;
        }
        lastFollowedPlayer = Time.realtimeSinceStartup;
        Vector2 targetPosition = followTarget.position;
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);


        // Calculate a closer position based on the distance and desired distance offset 
        if (distanceToTarget > _distanceFromTarget)
        {
            Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 closerPosition = targetPosition - directionToTarget * (_distanceFromTarget - 0.5f);
            targetPosition = closerPosition;
            float playerY = player.GetComponent<Transform>().position.y;
            float playerX = player.GetComponent<Transform>().position.x;
            bool yInHigherJumpArea = playerY < 18 && playerY > 10;
            bool xInHigherJumpArea = playerX < 120 && playerX > 56;

            if(yInHigherJumpArea && xInHigherJumpArea)
            {
                targetPosition.y = targetPosition.y + 8;
            }
            else
            {
                targetPosition.y = targetPosition.y + 4;
            }
           

            // Instantiate a new game object at the closer position 
            GameObject newLumi = Instantiate(gameObject, targetPosition, Quaternion.identity);
            newLumi.GetComponent<LumiScript>().isFollowing = true;

            // Destroy the existing game object 
            Destroy(gameObject);
        }
    }
    public void FollowPlayer()
    {
       
        if (Vector2.Distance(transform.position, followTarget.position) > _distanceFromTarget && isFollowing)
        {
            bool isTargetRunning = player.GetComponent<PixelCharacterController>().IsRunning;
            bool isTargetWalking = player.GetComponent<PixelCharacterController>().IsMoving;

            if (isTargetRunning)
            {
                _speed = 5.0f;
            }
            else if(isTargetWalking)
            {
                _speed = 3.0f;
            }

           transform.position = Vector2.MoveTowards(transform.position, followTarget.position, _speed * Time.deltaTime);
            _animator.SetBool("isRunning", true);
          

        

            if (followTarget.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
                //transform.eulerAngles = new Vector3(0, -180, 0);
              
            }
            else
            {
                spriteRenderer.flipX = false;
               // transform.eulerAngles = new Vector3(0, 0, 0);
            }
         
            
        }
        else
        {
            _animator.SetBool("isRunning", false);
 
        }
    }

    public void JumpRight()
    {

          
        _isGrounded = Physics2D.OverlapCircle(feetPosition.position, _groundCheckCircle, groundLayer);

        if (_isGrounded)
        {
            _isJumping = true;
            jumpTimeCounter = _jumpTime;
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * 3, ForceMode2D.Impulse);
        
        }

        if ( _isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                
                rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }

        }
    }


    public void ToogleFollow()
    {
        isFollowing = !isFollowing;

        if (isFollowing)
        {
            StartFollowingPlayer();

        }


    }


}
