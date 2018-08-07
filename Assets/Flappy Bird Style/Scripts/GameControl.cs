using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public Text scoreText;						//A reference to the UI text component that displays the player's score.
	public GameObject gameOvertext;				//A reference to the object that displays the text which appears when the player dies.
    public GameObject winText;
    public GameObject exitButton;

    private int score = 0;						//The player's score.
	public bool gameOver = false;				//Is the game over?
	public float scrollSpeed = -1.5f;
    public bool boostSpeed = false;
    public float speedTimer = 3f;
    public float speedTimerReset;
    public float scrollSpeedReset;
    public bool bicarusBurned = false;
    //public float growthFacor = 0.005f;
    public float timeCounter = 1f;
    private float timeCounterReset;
    public float spawnSpeedFactor = 0.0025f;
    

    void Awake()
	{
        timeCounterReset = timeCounter;
        scrollSpeedReset = scrollSpeed;
        speedTimerReset = speedTimer;
        //If we don't currently have a game control...
        if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	}


    void Update()
	{
        if (boostSpeed)
        {
            speedTimer -= Time.deltaTime;
            if (speedTimer <= 0)
            {
                scrollSpeed = scrollSpeedReset;
                speedTimer = speedTimerReset;
                boostSpeed = false;
            }
        }
        timeCounter -= Time.deltaTime;
        if (!gameOver && timeCounter <= 0)
        {
            gameObject.GetComponent<ABombPool>().spawnRate -= spawnSpeedFactor;
            gameObject.GetComponent<CigarsPool>().spawnRate -= spawnSpeedFactor;
            timeCounter = timeCounterReset;
        }
        
		//If the game is over and the player has pressed some input...
		if (gameOver && Input.GetMouseButtonDown(0)) 
		{
			//...reload the current scene.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(GameObject.Find("GameController"));
            SceneManager.LoadScene(0);
            
        }
    }

	public void BirdScored()
	{
        
        //OpeningController.instance.destroyObj();
        //The bird can't score if the game is over.
        if (gameOver)	
			return;
		//If the game is not over, increase the score...
		score++;
		//...and adjust the score text.
		scoreText.text = "Score: " + score.ToString();
        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
	}

	public void BirdDied()
	{
		//Activate the game over text.
		gameOvertext.SetActive (true);
		//Set the game to be over.
		gameOver = true;
	}

    public void AetherWon()
    {
        winText.SetActive(true);
        exitButton.SetActive(true);
    }
    public void ReturnHome()
    {
        Destroy(GameObject.Find("GameController"));
        SceneManager.LoadScene(0);
    }
}
