using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayButtonController : MonoBehaviour
{
	Button playButton;
	Text playButtonText;
	Animator canvasAnimator;

	void Start ()
	{
		playButton = GetComponent<Button> ();
		playButtonText = GameObject.Find ("Play Button Text").GetComponent<Text> ();

		canvasAnimator = GameObject.Find ("Canvas").GetComponent<Animator> ();
	}

	public void Play ()
	{
		canvasAnimator.SetBool ("isOpen", false);
	
		StartCoroutine (WaitAndLoadLevel());
	}	

	IEnumerator WaitAndLoadLevel()
	{
		do {
			yield return new WaitForEndOfFrame();
		} while(canvasAnimator.IsInTransition(0) || canvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Open"));

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
