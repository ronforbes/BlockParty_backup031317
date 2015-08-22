using UnityEngine;
using System.Collections;

public class BackButtonController : MonoBehaviour
{
	Animator gameUIAnimator;
	Animator boardPanelAnimator;

	void Start()
	{
		gameUIAnimator = GameObject.Find ("Game UI").GetComponent<Animator> ();
		boardPanelAnimator = GameObject.Find ("Board Panel").GetComponent<Animator> ();
	}

    public void Back()
    {
		gameUIAnimator.SetBool ("isHidden", true);
		boardPanelAnimator.SetBool ("isHidden", true);

		StartCoroutine (WaitAndLoadLevel());
	}	
	
	IEnumerator WaitAndLoadLevel()
	{
		yield return new WaitForSeconds (1.0f);

		Application.LoadLevel("Menu");
	}
}
