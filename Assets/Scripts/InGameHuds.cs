using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHuds : MonoBehaviour
{

    private LumiScript lumiScript;
    private bool isFollowing = false;
    private bool isLumingFollowing;
    // Start is called before the first frame update
    void Start()
    {
        isLumingFollowing = lumiScript.isFollowing;
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    public void ToogleFollow()
    {
        isFollowing = !isFollowing;
        print("ola" + isFollowing);
    }
}
