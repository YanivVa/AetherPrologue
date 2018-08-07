using UnityEngine;
using System.Collections;

public class CigarsPool : MonoBehaviour 
{
	public GameObject cigarsPrefab;									//The cigars game object.
	public int cigarsPoolSize = 5;									//How many cigars to keep on standby.
	public float spawnRate = 3f;									//How quickly cigars spawn.
	public float cigarsMin = -3.5f;									//Minimum x value of the cigars position.
	public float cigarsMax = 3.5f;									//Maximum x value of the cigars position.

	private GameObject[] cigars;									//Collection of pooled cigars.
	private int currentcigars = 0;									//Index of the current cigars in the collection.

	private Vector2 objectPoolPosition = new Vector2 (0, 20f);		//A holding position for our unused cigars offscreen.
	private float spawnYPosition = 10f;

	private float timeSinceLastSpawned;
    private bool allSpawned = false;
    private int j = 1;
    private bool endPool = false;

    void Start()
	{
		timeSinceLastSpawned = 0f;
        
		//Initialize the cigars collection.
		cigars = new GameObject[cigarsPoolSize];
        //Loop through the collection... 
        /*
		for(int i = 0; i < cigarsPoolSize; i++)
		{
			//...and create the individual cigars.
			cigars[i] = (GameObject)Instantiate(cigarsPrefab, objectPoolPosition, Quaternion.identity);
            
            //Physics2D.IgnoreCollision(Bird.instance.GetComponent<PolygonCollider2D>(), cigars[i].GetComponent<PolygonCollider2D>());
        }
        */
        float spawnXPosition = Random.Range(cigarsMin, cigarsMax);
        objectPoolPosition.x = spawnXPosition;
        cigars[0] = (GameObject)Instantiate(cigarsPrefab, objectPoolPosition, Quaternion.identity);
    }


	//This spawns cigars as long as the game is not over.
	void Update()
	{
        
		timeSinceLastSpawned += Time.deltaTime;
        float tmpSpawnRate = Random.Range(spawnRate - 1, spawnRate + 1);
        if (!allSpawned && timeSinceLastSpawned >= tmpSpawnRate)
        {
            timeSinceLastSpawned = 0f;
            float spawnXPosition = Random.Range(cigarsMin, cigarsMax);
            objectPoolPosition.x = spawnXPosition;
            cigars[j] = (GameObject)Instantiate(cigarsPrefab, objectPoolPosition, Quaternion.identity);
            if (j == cigarsPoolSize-1)
            {
                allSpawned = true;
                timeSinceLastSpawned = -9f;
            }
            j++;
            
        }
        if (GameControl.instance.bicarusBurned && !endPool)
        {
            for (int i = 0; i < cigarsPoolSize; i++)
            {
                var emission = cigars[i].GetComponent<Boost>().pEFirst.emission;
                emission.enabled = false;
                var emissionS = cigars[i].GetComponent<Boost>().pESecond.emission;
                emissionS.enabled = false;
            }
            endPool = true;
        }

        if (!GameControl.instance.gameOver && timeSinceLastSpawned >= spawnRate && allSpawned && !GameControl.instance.bicarusBurned) 
		{	
			timeSinceLastSpawned = 0f;

			//Set a random y position for the cigars
			float spawnXPosition = Random.Range(cigarsMin, cigarsMax);

            //...then set the current cigars to that position.
            if (!cigars[currentcigars].GetComponent<SpriteRenderer>().enabled)
            {
                //cigars[currentcigars].GetComponent<SpriteRenderer>().enabled = true;
                cigars[currentcigars].GetComponent<Rigidbody2D>().simulated = true;
                var emission = cigars[currentcigars].GetComponent<Boost>().pEFirst.emission;
                emission.enabled = true;
                var emissionS = cigars[currentcigars].GetComponent<Boost>().pESecond.emission;
                emissionS.enabled = true;
            }
            
            cigars[currentcigars].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            
            //Increase the value of currentcigars. If the new size is too big, set it back to zero
            currentcigars ++;

			if (currentcigars >= cigarsPoolSize) 
			{
				currentcigars = 0;
			}
		}
	}
}