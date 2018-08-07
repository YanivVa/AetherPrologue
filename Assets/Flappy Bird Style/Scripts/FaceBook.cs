using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceBook : MonoBehaviour {

    public Text userIdText;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
    }

    public void LogIn()
    {
        FB.LogInWithReadPermissions(callback: OnLogIn);
    }

    public void OnLogIn(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = AccessToken.CurrentAccessToken;
            userIdText.text=token.UserId;
        }
        else
        {
            Debug.Log("Canceled Login");
        }
    }

    public void Share()
    {
        FB.ShareLink(
            contentTitle: "AetheR Prologue",
            contentURL: new System.Uri("aether.uphero.com"),
            contentDescription: "Who can break My HighScore",
            callback: OnShare);
    }

    private void OnShare(IShareResult result)
    {
        if(result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Share error" + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("Share succceed");
        }

    }
    



}
