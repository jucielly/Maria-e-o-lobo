using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHuds : MonoBehaviour
{
    public Text lumiFollowsText;
   
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }


 


    public void ToogleFollow()
    {
     bool lumiIsFollowing = !GameObject.FindGameObjectWithTag("Lumi").GetComponent<LumiScript>().isFollowing;
     GameObject.FindGameObjectWithTag("Lumi").GetComponent<LumiScript>().isFollowing = lumiIsFollowing;

        if (!lumiIsFollowing)
        {
            lumiFollowsText.text = "Seguir";
        }
        else
        {
            lumiFollowsText.text = "Parar de seguir";
        }
        print("ola" + lumiIsFollowing);
    }
}
