using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutControl : MonoBehaviour {
    public GameObject infoTextObj;
    public GameObject buttonRigh;
    public GameObject buttonLeft;
    public GameObject buttomMarker;
    public GameObject obstacle;
    public GameObject powerUp;
    private string infoText;
    private bool hold = false;
    private string text1 = "press right to fly up and right";
    private string text2 = "press left to fly up and left";
    private string text3 = "careful not to reach the end";
    private string text4 = "don't hit obstacles";
    private string text5 = "collect boost items";
    private bool firstDone = false;
    private bool secondDone = false;
    private bool thirdDone = false;
    private string text6 = "tap anywhere to continue";
    private bool finalStage = false;

    // Use this for initialization
    void Start () {
        infoText = infoTextObj.GetComponent<Text>().text;

    }
	
	// Update is called once per frame
	void Update () {
        if (!hold)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!firstDone)
                {
                    infoTextObj.GetComponent<Text>().text = text1;
                    buttonRigh.SetActive(true);
                    hold = true;
                } else if(firstDone && !secondDone)
                {
                    buttomMarker.SetActive(false);
                    infoTextObj.GetComponent<Text>().text = text4;
                    obstacle.SetActive(true);
                    secondDone = true;
                } else if (firstDone && secondDone && !thirdDone)
                {
                    obstacle.SetActive(false);
                    infoTextObj.GetComponent<Text>().text = text5;
                    powerUp.SetActive(true);
                    thirdDone = true;
                    
                } else if (firstDone && secondDone && thirdDone && !finalStage)
                {
                    powerUp.SetActive(false);
                    infoTextObj.GetComponent<Text>().text = text6;
                    finalStage = true;
                } else if (firstDone && secondDone && thirdDone && finalStage)
                {
                    PlayerPrefs.SetInt("FinishedTut",1);
                    SceneManager.LoadScene(1);
                }
                
            }
        }
	}

    public void RightPress()
    {
        infoTextObj.GetComponent<Text>().text = text2;
        buttonRigh.SetActive(false);
        buttonLeft.SetActive(true);
    }
    public void LeftPress()
    {
        infoTextObj.GetComponent<Text>().text = text3;
        buttonLeft.SetActive(false);
        buttomMarker.SetActive(true);
        hold = false;
        firstDone = true;
    }
}
