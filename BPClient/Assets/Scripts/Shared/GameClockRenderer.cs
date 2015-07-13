using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameClockRenderer : MonoBehaviour
{
    Text text;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = String.Format("{0}:{1:00}", GameClock.Instance.TimeRemaining.Minutes, GameClock.Instance.TimeRemaining.Seconds);
	}
}
