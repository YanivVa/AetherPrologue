using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningController : MonoBehaviour {
    public static OpeningController instance;
    private bool isLoaded = false;
    public Sprite muteS;
    public Sprite PlayS;
    public Button audioB;
    public Button continueB;
    public bool isMute = true;
    private Button b;
    private bool duringPlay = false;

    // Use this for initialization
    void Start () {
        if (!PlayerPrefs.HasKey("score"))
        {
            PlayerPrefs.SetInt("score", 0);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Awake()
    {
        /*
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
            */
            
        /*
        if (isMute)
        {
            audioB.image.sprite = muteS;
            gameObject.GetComponent<AudioSource>().Stop();
        } else if (!isMute)
        {
            audioB.image.sprite = PlayS;
            gameObject.GetComponent<AudioSource>().Play();
        }
        */
        duringPlay = false;
        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !duringPlay)
        {
            Application.Quit();
        }
    }

    public void continueToPlay()
    {
        if (PlayerPrefs.HasKey("FinishedTut"))
        {
            duringPlay = true;
            SceneManager.LoadScene(1);
        } else
        {
            SceneManager.LoadScene(2);
        }
        
    }

    public void changeSprite()
    {
        if (isMute)
        {
            gameObject.GetComponent<AudioSource>().Play();
            audioB.image.sprite = PlayS;
            isMute = false;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();
            audioB.image.sprite = muteS;
            isMute = true;
        }
    }

    public void destroyObj()
    {
        Destroy(gameObject);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void FBlogin()
    {
               
    }

    public void FBshare()
    {

    }
}
