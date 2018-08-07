using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour 
{
	private Rigidbody2D rb2d;
    public float speedTimer = 3f;
    private float tmpScrollSpeed = -1.5f;
    public float bGScrollSpeedFactor = -2f;

    // Use this for initialization
    void Start () 
	{
        tmpScrollSpeed = GameControl.instance.scrollSpeed;
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();

		//Start the object moving.
		rb2d.velocity = new Vector2 (0, GameControl.instance.scrollSpeed + bGScrollSpeedFactor / 2);
	}
    
	void FixedUpdate()
	{
        
        if (GameControl.instance.scrollSpeed < tmpScrollSpeed)
        {
            tmpScrollSpeed = GameControl.instance.scrollSpeed;
            rb2d.velocity = new Vector2(0, tmpScrollSpeed + bGScrollSpeedFactor);

            //bustStarted = true;
        } else if(GameControl.instance.scrollSpeed > tmpScrollSpeed)
        {
            tmpScrollSpeed = GameControl.instance.scrollSpeed;
            rb2d.velocity = new Vector2(0, tmpScrollSpeed + bGScrollSpeedFactor/2);
        }
        
        // If the game is over, stop scrolling.
        if (GameControl.instance.gameOver == true || GameControl.instance.bicarusBurned)
		{
			rb2d.velocity = Vector2.zero;
		}
        
	}
    
    
}
