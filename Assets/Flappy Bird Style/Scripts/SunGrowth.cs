using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunGrowth : MonoBehaviour {

    private bool growthFlag = true;
    public float timeCounter = 1f;
    public float growthFacor = 0.005f;
    public float sunGrowthMax = 0.6f;
    private float growthTimeCounter;
    

    // Use this for initialization
    void Start()
    {
        growthTimeCounter = timeCounter;
    }

    // Update is called once per frame
    void Update()
    {
        
       if (GameControl.instance.gameOver == true)
        {
           growthFlag = false;
        }
        //timeCounter += Time.deltaTime;
        timeCounter -= Time.deltaTime;
       if (timeCounter <= 0f && growthFlag)
        {
            transform.localScale += new Vector3(growthFacor, growthFacor, 0);
            timeCounter = growthTimeCounter;
        }
        
        if (transform.localScale.x >= sunGrowthMax)
        {
            growthFlag = false;
            bicarusBurn();
        }

    }

    private void bicarusBurn()
    {
        GameControl.instance.bicarusBurned = true;
        //GameControl.instance.BirdDied();
    }
}
