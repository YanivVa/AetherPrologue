using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABombPool : MonoBehaviour {
    public GameObject aBombsPrefab;                                 //The aBombs game object.
    public int aBombsPoolSize = 5;                                  //How many aBombs to keep on standby.
    public float spawnRate = 3f;                                    //How quickly aBombs spawn.
    public float aBombsMin = -3.5f;                                 //Minimum x value of the aBombs position.
    public float aBombsMax = 3.5f;                                  //Maximum x value of the aBombs position.

    private GameObject[] aBombs;                                    //Collection of pooled aBombs.
    private int currentaBombs = 0;                                  //Index of the current aBombs in the collection.

    private Vector2 objectPoolPosition = new Vector2(0, 20f);       //A holding position for our unused aBombs offscreen.
    private float spawnYPosition = 10f;

    private float timeSinceLastSpawned;
    private bool allSpawned = false;
    private int j = 1;
    private Quaternion tmpQ = Quaternion.identity;
    private bool endPool = false;

    void Start()
    {
        timeSinceLastSpawned = 0f;
        tmpQ.eulerAngles = new Vector3(0, 0, -90f);
        //Initialize the aBombs collection.
        aBombs = new GameObject[aBombsPoolSize];
        //Loop through the collection... 
        /*
        for (int i = 0; i < aBombsPoolSize; i++)
        {
            //...and create the individual aBombs.
            aBombs[i] = (GameObject)Instantiate(aBombsPrefab, objectPoolPosition, tmpQ);

            //Physics2D.IgnoreCollision(Bird.instance.GetComponent<PolygonCollider2D>(), aBombs[i].GetComponent<PolygonCollider2D>());

        }
        */
        timeSinceLastSpawned = 0f;
        float spawnXPosition = Random.Range(aBombsMin, aBombsMax);
        objectPoolPosition.x = spawnXPosition;
        aBombs[0] = (GameObject)Instantiate(aBombsPrefab, objectPoolPosition, tmpQ);
        //aBombs[0].GetComponent<Rigidbody2D>().add
    }


    //This spawns aBombs as long as the game is not over.
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        float tmpSpawnRate = Random.Range(spawnRate - 1, spawnRate + 1);
        if (!allSpawned && timeSinceLastSpawned >= tmpSpawnRate)
        {
            timeSinceLastSpawned = 0f;
            float spawnXPosition = Random.Range(aBombsMin, aBombsMax);
            objectPoolPosition.x = spawnXPosition;
            aBombs[j] = (GameObject)Instantiate(aBombsPrefab, objectPoolPosition, tmpQ);
            if (j == aBombsPoolSize - 1)
            {
                allSpawned = true;
                timeSinceLastSpawned = -9f;
            }
            j++;

        }

        if (GameControl.instance.bicarusBurned && !endPool)
        {
            for (int i = 0; i < aBombsPoolSize; i++)
            {
                if (aBombs[i] != null)
                {
                    if (aBombs[i].GetComponent<Boost>() != null)
                    {
                        if (aBombs[i].GetComponent<Boost>().pEFirst.emission.enabled)
                        {
                            var emission = aBombs[i].GetComponent<Boost>().pEFirst.emission;
                            emission.enabled = false;
                            var emissionS = aBombs[i].GetComponent<Boost>().pESecond.emission;
                            emissionS.enabled = false;
                        }

                    }
                }
                
                
            }
            endPool = true;
        }

        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate && allSpawned && !GameControl.instance.bicarusBurned)
        {
            timeSinceLastSpawned = 0f;

            //Set a random y position for the aBombs
            float spawnXPosition = Random.Range(aBombsMin, aBombsMax);

            //...then set the current aBombs to that position.
            if (!aBombs[currentaBombs].GetComponent<SpriteRenderer>().enabled)
            {
                aBombs[currentaBombs].GetComponent<Animator>().SetTrigger("Restore");
                aBombs[currentaBombs].GetComponent<SpriteRenderer>().enabled = true;
                aBombs[currentaBombs].GetComponent<Rigidbody2D>().simulated = true;

            }

            aBombs[currentaBombs].transform.position = new Vector2(spawnXPosition, spawnYPosition);

            //Increase the value of currentaBombs. If the new size is too big, set it back to zero
            currentaBombs++;

            if (currentaBombs >= aBombsPoolSize)
            {
                currentaBombs = 0;
            }
        }
    }
}