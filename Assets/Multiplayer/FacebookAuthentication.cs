using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookAuthentication : MonoBehaviour
{

    public void OnFacebookStart()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }


    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            FBLogin();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {

       
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }


    public void FBLogin()
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    string meQueryString = "/me?fields=id,name,first_name,last_name,email";
    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);

            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            FB.API(meQueryString, HttpMethod.GET, FbApiCallback);
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    string userName = string.Empty;
    string passWord = string.Empty;
    string url = string.Empty;

    string id = string.Empty;
    string firstname = string.Empty;
    string lastname = string.Empty;
    string gender = string.Empty;
    string email = string.Empty;
    string type = string.Empty;

    void FbApiCallback(IGraphResult result)
    {
        Debug.Log("APICallback Facebook :" + result.RawResult);
        if (result.Error != null)
        {
            Debug.Log("Error Occured");
            return;
        }

        Debug.Log("Result = " + result.RawResult);
        if (result.ResultDictionary.TryGetValue("id", out id))
        {
            Debug.Log("ID = " + id.ToString());
        }
        if (result.ResultDictionary.TryGetValue("first_name", out firstname))
        {
            Debug.Log("firstname= " + firstname.ToString());
        }
        if (result.ResultDictionary.TryGetValue("last_name", out lastname))
        {
            Debug.Log("lastname= " + lastname.ToString());
        }
        if (result.ResultDictionary.TryGetValue("gender", out gender))
        {
            Debug.Log("gender= " + gender.ToString());
        }
        if (result.ResultDictionary.TryGetValue("email", out email))
        {
            Debug.Log("email= " + email.ToString());
        }
        userName = id.ToString();
        url = "https" + "://graph.facebook.com/" + userName + "/picture?type=large";
    }
}
