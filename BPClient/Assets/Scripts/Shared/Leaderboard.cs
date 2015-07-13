using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardEntry
{
    public int Rank { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
}

public class Leaderboard : MonoBehaviour
{
    static Leaderboard instance;
    public static Leaderboard Instance
    {
        get
        {
            // Get the singleton instance of the Game Clock
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Leaderboard>();

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    public List<LeaderboardEntry> LeaderboardEntries = new List<LeaderboardEntry>();

    void Awake()
    {
        // If this is the first instance, make it the singleton. If a singleton already exists and another reference is found, destroy it
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    public void Reset()
    {
        LeaderboardEntries.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
