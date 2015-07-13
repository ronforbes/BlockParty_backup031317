using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreRenderer : MonoBehaviour
{
    Text scoreText;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = ScoreManager.Instance.Score.ToString();
        }
    }
}
