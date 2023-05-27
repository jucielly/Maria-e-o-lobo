using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CherryScript : MonoBehaviour
{
   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();

            if (player != null)
            {
                player.LifeRecovery();
                Destroy(this.gameObject);
            }
        }
    }

}
