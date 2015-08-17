using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayButtonController : MonoBehaviour
{
	Button playButton;
	Text playButtonText;

	void Start ()
	{
		playButton = GetComponent<Button> ();
		playButtonText = GameObject.Find ("Play Button Text").GetComponent<Text> ();
	}

	public void Play ()
	{
		string level = GameClock.Instance.State.ToString ();
		Application.LoadLevel (level);
	}	

	void Update ()
	{
		if (GameClock.Instance.State == GameClock.ClockState.Default) {
			playButton.interactable = false;
			playButtonText.text = "Loading...";
		} else {
			playButton.interactable = true;
			playButtonText.text = "Play";
		}
	}
}
