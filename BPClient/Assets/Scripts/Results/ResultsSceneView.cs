using UnityEngine;
using System.Collections;
using System;

public class ResultsSceneView : MonoBehaviour {
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameClock.Instance.State == GameClock.ClockState.Results &&
		   GameClock.Instance.TimeRemaining <= TimeSpan.FromSeconds(3))
		{
			//animator.SetBool("isOpen", false);
		}
	}
}
