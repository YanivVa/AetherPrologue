using UnityEngine;
using System.Collections;
using System;

public class RepeatingBackground : MonoBehaviour 
{
    
    public BoxCollider2D edgeCollider;		//This stores a reference to the collider attached to the edge.
	private float edgeVerticalLength;		//A float to store the x-axis length of the collider2D attached to the edge GameObject.
    private SpriteRenderer groundSprite;
    private BoxCollider2D groundCollider;
    private bool firstRepeat = false;

	//Awake is called before Start.
	private void Awake ()
	{
        //Get and store a reference to the collider2D attached to edge.
        //edgeCollider = GetComponent<BoxCollider2D> ();
        //Store the size of the collider along the x axis (its length in units).
        groundSprite = GetComponent<SpriteRenderer>();
        groundCollider = GetComponent<BoxCollider2D>();
        edgeVerticalLength = edgeCollider.size.y;
	}

	//Update runs once per frame
	private void Update()
	{
		//Check if the difference along the y axis between the main Camera and the position of the object this is attached to is greater than edgeVerticalLength.
		if (transform.position.y < -edgeVerticalLength)
		{
			//If true, this means this object is no longer visible and we can safely move it forward to be re-used.
			RepositionBackedge ();
		}
    }
    

    //Moves the object this script is attached to right in order to create our looping backedge effect.
    private void RepositionBackedge()
	{
		//This is how far to the right we will move our backedge object, in this case, twice its length. This will position it directly to the right of the currently visible backedge object.
		Vector2 edgeOffSet = new Vector2(0, edgeVerticalLength * 2f);

		//Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
		transform.position = (Vector2) transform.position + edgeOffSet;
        if (!firstRepeat)
        {
            groundSprite.enabled = false;
            groundCollider.enabled = false;
            firstRepeat = true;

        }
	}
}