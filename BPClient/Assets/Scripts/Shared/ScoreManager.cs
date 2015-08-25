﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ScoreManager>();
                
                // tell unity not to destroy this object when loading a new scene
                if (instance != null)
                {
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            
            return instance;
        }
    }

    public int Score;
    public int Level;
    public int Matches;
    public int Combos;
    public Dictionary<int, int> ComboLengths;
    public int Chains;
    public Dictionary<int, int> ChainLengths;

    const int raiseValue = 1;
    public const int MatchValue = 10;
    const int comboValue = 100;
    const int chainValue = 1000;

    void Awake()
    {
        if (instance == null)
        {
            // if i'm the first instance, make me the singleton
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            // if a singleton already exists and you find another reference in scene, destroy it
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Reset();   
    }

    public void Reset()
    {
        Score = 0;
        Level = 1;
        Matches = 0;
        Combos = 0;
        Chains = 0;

        ComboLengths = new Dictionary<int, int>();

        for (int comboLength = 4; comboLength < Board.Rows * Board.Columns; comboLength++)
        {
            ComboLengths.Add(comboLength, 0);
        }

        ChainLengths = new Dictionary<int, int>();

        for (int chainLength = 2; chainLength < Board.Rows * Board.Columns / 3; chainLength++)
        {
            ChainLengths.Add(chainLength, 0);
        }
    }

    public void ScoreRaise()
    {
        Score++;
    }

    public void ScoreMatch()
    {
        Score += MatchValue;
        Matches++;
    }

    public void ScoreCombo(int comboLength)
    {
        Score += comboValue * comboValue;
        Combos++;

        if (!ComboLengths.ContainsKey(comboLength))
        {
            ComboLengths.Add(comboLength, 0);
        }
        ComboLengths [comboLength]++;
    }

    public void ScoreChain(int chainLength)
    {
        Score += chainLength * chainValue;
        Chains++;

        if (!ChainLengths.ContainsKey(chainLength))
        {
            ChainLengths.Add(chainLength, 0);
        }
        ChainLengths [chainLength]++;
    }
    
    // Update is called once per frame
    void Update()
    {
        Level = (int)Mathf.Clamp(Score / 1000, 1, 99);
    }
} 