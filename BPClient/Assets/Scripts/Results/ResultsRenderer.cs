using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultsRenderer : MonoBehaviour
{
    Text scoreText, levelText, blocksClearedText, combosText, chainsText;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        levelText = GameObject.Find("Level Text").GetComponent<Text>();
        blocksClearedText = GameObject.Find("Blocks Cleared Text").GetComponent<Text>();
        combosText = GameObject.Find("Combos Text").GetComponent<Text>();
        chainsText = GameObject.Find("Chains Text").GetComponent<Text>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if(ScoreManager.Instance != null)
        {
            scoreText.text = ScoreManager.Instance.Score.ToString();
            levelText.text = ScoreManager.Instance.Level.ToString();
            blocksClearedText.text = ScoreManager.Instance.Matches.ToString();
            combosText.text = ScoreManager.Instance.Combos.ToString();
            chainsText.text = ScoreManager.Instance.Chains.ToString();
        }
    }
}
