using Cainos.CustomizablePixelCharacter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    }

    public void StartFollowingPlayer()
    {
        Vector2 targetPosition = followTarget.position;
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

        // Calculate a closer position based on the distance and desired distance offset
        if (distanceToTarget > _distanceFromTarget)
        {
            Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 closerPosition = targetPosition - directionToTarget * (_distanceFromTarget - 0.5f);
            targetPosition = closerPosition;

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
            }
            else
            {
                spriteRenderer.flipX = false;
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


}
