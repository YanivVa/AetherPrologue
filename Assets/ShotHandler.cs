using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotHandler : MonoBehaviour {
    public float shotSpeed = 5f;
    public float timeCounter = 4f;
    private float timeReset;
    public GameObject spriteObj;
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);
        timeReset = timeCounter;
    }
	
	// Update is called once per frame
	void Update () {
        timeCounter -= Time.deltaTime;
        if (timeCounter <= 0)
        {
            Destroy(this.gameObject);

            //timeCounter = timeReset;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacles" || collision.gameObject.tag == "Edges")
        {
            anim.SetTrigger("Explode");
            Destroy(gameObject);
        }
    }
}
