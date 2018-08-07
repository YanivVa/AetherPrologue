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
    public float timeAdCounter;
    private float timeAdCounterReset;

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
        timeAdCounterReset = timeAdCounter;


        duringPlay = false;
        
    }

    // Update is called once per frame
    void Update () {

        timeAdCounter -= Time.deltaTime;
        if(GameControl.instance != null)
        {
            if (timeAdCounter <= 0 && GameControl.instance.gameOver)
            {
                GetComponent<UnityAdsExample>().ShowRewardedAd();
                timeAdCounter = timeAdCounterReset;
            }
        }
        
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

    public void GoToHomepage()
    {
        //WWW url = new WWW("https://weeboostudio.wixsite.com/weeboo");
        Application.OpenURL("https://weeboostudio.wixsite.com/weeboo");
    }
}
