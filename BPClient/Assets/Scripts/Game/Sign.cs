using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour {
	public string Text;
	public int X, Y;
	public Color Color;
	Animator animator;
	Animation animation;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animation = GetComponent<Animation> ();
	}

	public void Play()
	{
		//animator.enabled = true;
		//animator.Play ("Rise");
		animator.SetTrigger ("Rise");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
