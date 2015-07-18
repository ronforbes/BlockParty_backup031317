using UnityEngine;
using System.Collections;

public class Ad : MonoBehaviour
{
	UnityEngine.iOS.ADBannerView ad;

	// Use this for initialization
	void Start ()
	{
		ad = new UnityEngine.iOS.ADBannerView (UnityEngine.iOS.ADBannerView.Type.Banner, UnityEngine.iOS.ADBannerView.Layout.Bottom);
		UnityEngine.iOS.ADBannerView.onBannerWasClicked += OnBannerWasClicked;
		UnityEngine.iOS.ADBannerView.onBannerWasLoaded += OnBannerWasLoaded;
	}

	void OnBannerWasClicked ()
	{

	}

	void OnBannerWasLoaded ()
	{
		ad.visible = true;
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
