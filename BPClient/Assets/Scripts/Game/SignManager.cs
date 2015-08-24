using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignManager : MonoBehaviour {
	public Sign[,] Signs;
	public Sign SignPrefab;

	void Awake()
	{
		Signs = new Sign[Board.Columns, Board.Rows];

		for (int x = 0; x < Board.Columns; x++) {
			for (int y = 0; y < Board.Rows; y++) {
				Signs [x, y] = Instantiate(SignPrefab, Vector3.zero, Quaternion.identity) as Sign;
				Signs [x, y].transform.parent = transform;
				Signs [x, y].X = x;
				Signs [x, y].Y = y;
			}
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}

	public void CreateSign(int x, int y, string text, Color color)
	{
		Signs [x, y].Text = text;
		Signs [x, y].Color = color;
		Signs [x, y].Play ();
	}

	public void Play()
	{
		Signs [3, 5].Play ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
