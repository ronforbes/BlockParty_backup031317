using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour {
	public string Text;
	public int X, Y;
	public Color Color;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}

	public void Play()
	{
		animator.SetTrigger ("Rise");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
