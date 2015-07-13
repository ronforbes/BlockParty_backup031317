using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaderboardRenderer : MonoBehaviour {
    Text rankText, nameText, scoreText;
	// Use this for initialization
	void Start () {
        rankText = GameObject.Find("Rank Text").GetComponent<Text>();
        nameText = GameObject.Find("Name Text").GetComponent<Text>();
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();

        int rank = 1;

        foreach (LeaderboardEntry entry in Leaderboard.Instance.LeaderboardEntries)
        {
            rankText.text += rank.ToString() + "\n";
            nameText.text += entry.Name + "\n";
            scoreText.text += entry.Score + "\n";

            rank++;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
