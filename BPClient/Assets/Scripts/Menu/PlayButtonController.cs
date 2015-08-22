using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayButtonController : MonoBehaviour
{
	Button playButton;
	Text playButtonText;
	Animator identityUIAnimator;
	Animator horizontalLineAnimator;
	Animator titleImageAnimator;
	Animator playButtonAnimator;

	void Start ()
	{
		playButton = GetComponent<Button> ();
		playButtonText = GameObject.Find ("Play Button Text").GetComponent<Text> ();
		identityUIAnimator = GameObject.Find ("Identity UI").GetComponent<Animator> ();
		horizontalLineAnimator = GameObject.Find ("Horizontal Line").GetComponent<Animator> ();
		titleImageAnimator = GameObject.Find ("Title Image").GetComponent<Animator> ();
		playButtonAnimator = GameObject.Find ("Play Button").GetComponent<Animator> ();
	}

	public void Play ()
	{
		identityUIAnimator.SetBool ("isHidden", true);
		horizontalLineAnimator.SetBool ("isHidden", true);
		titleImageAnimator.SetBool ("isHidden", true);
		playButtonAnimator.SetBool ("isHidden", true);

		StartCoroutine (WaitAndLoadLevel());
	}	

	IEnumerator WaitAndLoadLevel()
	{
		yield return new WaitForSeconds (2.0f);
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
