using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class LumiScript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _jumpForce = 5f;
    private bool isFollowing = false;
    private Transform followTarget;
    private float _distanceFromTarget = 6.0f;
    private Animator _animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        followTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            FollowPlayer();
        }
      

    }


    public void FollowPlayer()
    {
        if (Vector2.Distance(transform.position, followTarget.position) > _distanceFromTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, followTarget.position, _speed * Time.deltaTime);

         

            if(followTarget.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
             _animator.SetBool("isRunning", true);
            
        }
        else
        {
            isFollowing = false;
            _animator.SetBool("isRunning", false);
        }
    }


    public void StartFollow()
    {
        isFollowing = true;
    }

    public void StopFollow()
    {
        isFollowing = false;
    }

}
