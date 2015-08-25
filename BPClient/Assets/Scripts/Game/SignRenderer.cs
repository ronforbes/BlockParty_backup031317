using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignRenderer : MonoBehaviour {
	Sign sign;
	BoardRaiser raiser;
	Text text;
	public float FloatOffset;

	void Awake()
	{
		sign = GetComponent<Sign> ();
		raiser = GameObject.Find ("Board").GetComponent<BoardRaiser> ();
		text = GetComponent<Text> ();
	}

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (sign.X, sign.Y, 0.0f);;
	}
	
	// Update is called once per frame
	void Update () {
		float raiseOffset = raiser.Elapsed / BoardRaiser.Duration;
		transform.position = new Vector3 (sign.X, sign.Y + raiseOffset + FloatOffset, 0.0f);
		text.text = sign.Text;
		text.color = new Color(sign.Color.r, sign.Color.g, sign.Color.b, text.color.a);
	}
}
