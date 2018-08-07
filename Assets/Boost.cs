using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour {
    private float tmpScrollSpeed;
    private Rigidbody2D rb2d;
    public float newScrollSpeed = -1.5f;
    public float boostSpeedFactor = 200f;
    public ParticleSystem pEFirst;
    public ParticleSystem pESecond;
    public bool moduleEnabled;

    // Use this for initialization
    void Start()
    {
        tmpScrollSpeed = GameControl.instance.scrollSpeed;
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();

        //Start the object moving.
        rb2d.velocity = new Vector2(0, GameControl.instance.scrollSpeedReset);
    }

    void FixedUpdate()
    {
        if (GameControl.instance.scrollSpeed != tmpScrollSpeed)
        {
            tmpScrollSpeed = GameControl.instance.scrollSpeed;
            rb2d.velocity = new Vector2(0, GameControl.instance.scrollSpeed);

            //bustStarted = true;
        }
        // If the game is over, stop scrolling.
        if (GameControl.instance.gameOver || GameControl.instance.bicarusBurned)
        {
            rb2d.velocity = Vector2.zero;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bird" && !GameControl.instance.bicarusBurned)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            //pEFirst.enableEmission = false;
            var emission = pEFirst.emission;
            emission.enabled = false;
            var emissionS = pESecond.emission;
            emissionS.enabled = false;
            //pESecond.emission.enabled = tmpPE;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, boostSpeedFactor));
            GameControl.instance.scrollSpeed = newScrollSpeed;
            GameControl.instance.boostSpeed = true;
            GameControl.instance.speedTimer = GameControl.instance.speedTimerReset;
            GameControl.instance.BirdScored();
        }
    }
    /*
    void OnGUI()
    {
        moduleEnabled = GUI.Toggle(new Rect(25, 45, 100, 30), moduleEnabled, "Enabled");
    }
    */

}
