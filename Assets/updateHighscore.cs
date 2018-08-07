using UnityEngine;
using UnityEngine.UI;

public class updateHighscore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //TODO: if is not neccecry "yaniv" , if score is 0 its still true
        if (PlayerPrefs.HasKey("score"))
        {
            Debug.Log(PlayerPrefs.HasKey("score"));
            gameObject.GetComponent<Text>().text += PlayerPrefs.GetInt("score");
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
