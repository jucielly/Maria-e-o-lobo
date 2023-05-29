using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumiScript : MonoBehaviour
{
    [SerializeField]
   private float _speed = 3.0f;
    private Transform followTarget;
    // Start is called before the first frame update
    void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, followTarget.position) > 6)
        {
            transform.position = Vector2.MoveTowards(transform.position, followTarget.position, _speed * Time.deltaTime);
        }
       
    }
}
