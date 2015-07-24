using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultsRenderer : MonoBehaviour
{
	Text scoreText, blocksClearedText;

	// Use this for initialization
	void Start ()
	{
		scoreText = GameObject.Find ("Score Text").GetComponent<Text> ();
		blocksClearedText = GameObject.Find ("Blocks Cleared Text").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ScoreManager.Instance != null) {
			scoreText.text = ScoreManager.Instance.Score.ToString ();
			blocksClearedText.text = ScoreManager.Instance.Matches.ToString ();
		}
	}
}
