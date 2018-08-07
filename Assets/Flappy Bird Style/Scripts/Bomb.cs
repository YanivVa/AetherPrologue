using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    private float tmpScrollSpeed;
    private Rigidbody2D rb2d;
    public float spinFactor = 100f;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        tmpScrollSpeed = GameControl.instance.scrollSpeed;
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();

        //Start the object moving.
        rb2d.velocity = new Vector2(0, GameControl.instance.scrollSpeedReset);
    }

    void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * spinFactor);
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
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bird" && !GameControl.instance.gameOver)
        {
            anim.SetTrigger("Explode");
            collision.GetComponent<Bird>().SetDeath();
            GameControl.instance.BirdDied();
        }
        if (collision.gameObject.tag == "Obstacles")
        {
            //collision.GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            anim.SetTrigger("Explode");
        }
    }

}
