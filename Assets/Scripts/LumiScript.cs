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
    public bool isFollowing = false;
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
       
            FollowPlayer();
        
      

    }


    public void FollowPlayer()
    {
        if (Vector2.Distance(transform.position, followTarget.position) > _distanceFromTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, followTarget.position, _speed * Time.deltaTime);

            print("ta coreeno");
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
            print("nau ta coreeno");
        }
    }


}
