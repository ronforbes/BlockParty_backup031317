using UnityEngine;
using System.Collections;

public class BackButtonController : MonoBehaviour
{
	Animator canvasAnimator;

	void Start()
	{
		canvasAnimator = GameObject.Find ("Animation Canvas").GetComponent<Animator> ();
	}

    public void Back()
    {
		canvasAnimator.SetBool ("isOpen", false);

		StartCoroutine (WaitAndLoadLevel());
	}	
	
	IEnumerator WaitAndLoadLevel()
	{
		do {
			yield return new WaitForEndOfFrame();
		} while(canvasAnimator.IsInTransition(0) || canvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Open"));

		Application.LoadLevel("Menu");
	}
}
