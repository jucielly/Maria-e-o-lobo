using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.5f;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * _speed;
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
      
            Destroy(this.gameObject);
    }
}
