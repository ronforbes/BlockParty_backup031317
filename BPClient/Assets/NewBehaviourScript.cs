using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bitrave.Azure;
using System;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour {
    AzureMobileServices service;
    string name = "";
    Texture2D picture;
    Text greetingText;
    Text nameText;
    RawImage pictureRawImage;
    Button signInButton;

	// Use this for initialization
	void Start()
    {
        // Connect to the Block Party service
        //service = new AzureMobileServices("http://localhost:49753", "tEtsHvgLHoRZKUATnELAkzCLWXARVl99");
        service = new AzureMobileServices("http://blockparty.azure-mobile.net", "tEtsHvgLHoRZKUATnELAkzCLWXARVl99");

        // Ping the Block Party service
        //WWW userManagerHttpRequest = new WWW("http://localhost:49753/api/UserManager");
        WWW userManagerHttpRequest = new WWW("http://blockparty.azure-mobile.net/api/UserManager");
        StartCoroutine(OnUserManagerHttpRequest(userManagerHttpRequest));

        // Initialize the Facebook SDK
        FB.Init(OnInitialized);

        // Find the UI components
        greetingText = GameObject.Find("Greeting Text").GetComponent<Text>();
        nameText = GameObject.Find("Name Text").GetComponent<Text>();
        pictureRawImage = GameObject.Find("Picture Raw Image").GetComponent<RawImage>();
        signInButton = GameObject.Find("Sign In Button").GetComponent<Button>();
    }

    IEnumerator OnUserManagerHttpRequest(WWW httpRequest)
    {
        // Wait until the HTTP request has received a response
        yield return httpRequest;

        Debug.Log("Pinged User Manager. Request=" + httpRequest.text);
    }

    void OnInitialized()
    {
        Debug.Log("Initialized Facebook SDK. IsLoggedIn=" + FB.IsLoggedIn);
    }

    public void SignIn()
    {
        // Log into Facebook
        FB.Login("", OnFacebookLoggedIn);
    }

    void OnFacebookLoggedIn(FBResult result)
    {
        Debug.Log("Logged into Facebook. UserId=" + FB.UserId + ", AccessToken=" + FB.AccessToken);

        // Log into the Block Party service
        service.LoginAsync(AuthenticationProvider.Facebook, FB.AccessToken, OnServiceLoggedIn);

        // Request the Facebook user profile
        FB.API("/me", Facebook.HttpMethod.GET, OnGetMe);

        // Request the Facebook profile picture
        StartCoroutine("LoadProfilePicture");
    }

    void OnServiceLoggedIn(AzureResponse<MobileServiceUser> response)
    {
        // If the login attempt was successful, save the user object. Otherwise, log the error
        if (response.Status == AzureResponseStatus.Success)
        {
            service.User = response.ResponseData;
            Debug.Log("Logged into Block Party.");
        }
        else
        {
            Debug.Log("Error:" + response.StatusCode);
        }
    }

    void OnGetMe(FBResult result)
    {
        // Deserialize the result, and save the name
        Dictionary<string, object> profile = Facebook.MiniJSON.Json.Deserialize(result.Text) as Dictionary<string, object>;
        name = profile["name"] as string;
        Debug.Log("Received Facebook profile. name=" + profile["name"]);
    }

    IEnumerator LoadProfilePicture()
    {
        // Wait for the HTTP request's response, and store the picture
        WWW httpRequest = new WWW("https://graph.facebook.com/me/picture?access_token=" + FB.AccessToken);
        yield return httpRequest;
        picture = new Texture2D(128, 128, TextureFormat.DXT1, true);
        httpRequest.LoadImageIntoTexture(picture);
    }
	
	// Update is called once per frame
	void Update () {
        // If the name and picture are valid, display them. Otherwise, display the guest UI
        if (name != "" && picture != null)
        {            
            greetingText.text = "Welcome back";
            nameText.text = name;
            pictureRawImage.texture = picture;
            pictureRawImage.gameObject.SetActive(true);
            signInButton.gameObject.SetActive(false);
        }
        else
        {
            greetingText.text = "Hello Guest";
            nameText.text = "Sign in to play online";
            pictureRawImage.gameObject.SetActive(false);
            signInButton.gameObject.SetActive(true);
        }
	}
}
