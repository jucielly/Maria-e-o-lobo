using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogSript : MonoBehaviour
{
    [SerializeField]
    private float _moveX = -1f;
    [SerializeField]
    private float _speed = 3f;
    private Rigidbody2D rb;
    private bool _isFacingRight;
    private Vector3 localScale;
    private SpriteRenderer _spriteRenderer;
    private int life = 4;


    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();


    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(_moveX * _speed, rb.velocity.y);
    }

    private void LateUpdate()
    {
        Flip();
    }


    void Flip()
    {
        if (_moveX > 0)
        {
            _isFacingRight = true;
            _spriteRenderer.flipX = false;

        }
        else if (_moveX < 0)
        {
            _isFacingRight = false;
            _spriteRenderer.flipX = true;
        }


        if(((_isFacingRight) && (localScale.x < 0)) || ((_isFacingRight) && (localScale.x > 0)))
        {
            localScale.x *= 1;
        }

        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            _moveX *= -1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Arrow")
        {
            life = life - 1;
          
            if(life < 1)
            {
                Destroy(this.gameObject);
            }
        }


        /*if (collision.gameObject.tag == "Player")
        {
            //damage player
          PlayerScript  player = collision.gameObject.GetComponent<PlayerScript>();

            if(player != null)
            {
                player.Damage();
            }
        }*/
    }

}
