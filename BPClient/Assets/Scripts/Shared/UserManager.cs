using UnityEngine;
using System.Collections;
using Bitrave.Azure;
using System.Collections.Generic;

public class UserManager : MonoBehaviour
{
	static UserManager instance;
	public static UserManager Instance {
		get {
			// Get the singleton instance of the Game Clock
			if (instance == null) {
				instance = GameObject.FindObjectOfType<UserManager> ();

				DontDestroyOnLoad (instance.gameObject);
			}

			return instance;
		}
	}

	public string Id;
	public string Name;
	public Texture2D Picture;

	void Awake ()
	{
		// If this is the first instance, make it the singleton. If a singleton already exists and another reference is found, destroy it
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			if (this != instance)
				Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		// Initialize the Facebook SDK
		FB.Init (OnInitialized);
	}

	void OnInitialized ()
	{

	}

	public void Login ()
	{
		// Log into Facebook
		FB.Login ("", OnFacebookLoggedIn);
	}

	public void Logout ()
	{
		// Log out of Facebook
		FB.Logout ();
		Id = "";
		Name = "";
		Picture = null;
	}

	void OnFacebookLoggedIn (FBResult result)
	{ 
		// Log into the Block Party service
		NetworkingManager.Instance.Service.LoginAsync (AuthenticationProvider.Facebook, FB.AccessToken, OnServiceLoggedIn);

		// Request the Facebook user profile
		FB.API ("/me", Facebook.HttpMethod.GET, OnGetMe);

		// Request the Facebook profile picture
		StartCoroutine ("LoadProfilePicture");
	}

	void OnServiceLoggedIn (AzureResponse<MobileServiceUser> response)
	{
		// If the login attempt was successful, save the user object. Otherwise, log the error
		if (response.Status == AzureResponseStatus.Success) {
			NetworkingManager.Instance.Service.User = response.ResponseData;
		} else {
			Debug.Log ("Error:" + response.StatusCode);
		}
	}

	void OnGetMe (FBResult result)
	{
		// Deserialize the result, and save the name
		Dictionary<string, object> profile = Facebook.MiniJSON.Json.Deserialize (result.Text) as Dictionary<string, object>;
		Id = profile ["id"] as string;
		Name = profile ["name"] as string;
	}

	IEnumerator LoadProfilePicture ()
	{
		// Wait for the HTTP request's response, and store the picture
		WWW httpRequest = new WWW ("https://graph.facebook.com/me/picture?access_token=" + FB.AccessToken);
		yield return httpRequest;
		Picture = new Texture2D (128, 128, TextureFormat.RGB24, true);
		httpRequest.LoadImageIntoTexture (Picture);
	}
}
