using UnityEngine;
using System.Collections;

public class Ad : MonoBehaviour
{
	static Ad instance;
	public static Ad Instance {
		get {
			// Get the singleton instance of the Game Clock
			if (instance == null) {
				instance = GameObject.FindObjectOfType<Ad> ();
				
				DontDestroyOnLoad (instance.gameObject);
			}
			
			return instance;
		}
	}

	UnityEngine.iOS.ADBannerView ad;

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
		DontDestroyOnLoad (gameObject);

		ad = new UnityEngine.iOS.ADBannerView (UnityEngine.iOS.ADBannerView.Type.Banner, UnityEngine.iOS.ADBannerView.Layout.Bottom);
		UnityEngine.iOS.ADBannerView.onBannerWasClicked += OnBannerWasClicked;
		UnityEngine.iOS.ADBannerView.onBannerWasLoaded += OnBannerWasLoaded;
	}

	void OnBannerWasClicked ()
	{

	}

	void OnBannerWasLoaded ()
	{
		if (ad != null) {
			ad.visible = true;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (!ad.visible) {
			if (Application.loadedLevelName == "Results" || Application.loadedLevelName == "Lobby") {
				ad.visible = true;
			}
		}

		if (ad.visible) {
			if (Application.loadedLevelName == "Menu" || Application.loadedLevelName == "Game") {
				ad.visible = false;
			}
		}
	}

	void OnDestroy ()
	{
		if (ad != null) {
			ad.visible = false;
		}
	}
}
