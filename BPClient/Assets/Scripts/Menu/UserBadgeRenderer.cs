using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bitrave.Azure;
using System;
using System.Collections.Generic;

public class UserBadgeRenderer : MonoBehaviour
{
	Text greetingText;
	Text nameText;
	RawImage pictureRawImage;
	Button signInButton;
	Button signOutButton;

	// Use this for initialization
	void Start ()
	{
		// Find the UI components
		greetingText = GameObject.Find ("Greeting Text").GetComponent<Text> ();
		nameText = GameObject.Find ("Name Text").GetComponent<Text> ();
		pictureRawImage = GameObject.Find ("Picture Raw Image").GetComponent<RawImage> ();
		signInButton = GameObject.Find ("Sign In Button").GetComponent<Button> ();
		signOutButton = GameObject.Find ("Sign Out Button").GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the name and picture are valid, display them. Otherwise, display the guest UI
		if (UserManager.Instance.Name != "" && UserManager.Instance.Picture != null) {            
			greetingText.text = "Welcome back";
			nameText.text = UserManager.Instance.Name;
			pictureRawImage.texture = UserManager.Instance.Picture;
			pictureRawImage.gameObject.SetActive (true);
			signInButton.gameObject.SetActive (false);
			signOutButton.gameObject.SetActive (true);
		} else {
			greetingText.text = "Hello Guest";
			nameText.text = "Sign in to play online";
			pictureRawImage.gameObject.SetActive (false);
			signInButton.gameObject.SetActive (true);
			signOutButton.gameObject.SetActive (false);
		}
	}
}
