﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainDetector : MonoBehaviour
{
    public int ChainLength = 0;
    Board board;

    void Awake()
    {
        board = GetComponent<Board>();
    }

    public void IncrementChain()
    {
        ChainLength++;
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ScoreChain(ChainLength);
        }
    }

    void Update()
    {
        bool stopChain = true;

        // detect blocks that are eligible to particpate in chains
        for (int x = 0; x < Board.Columns; x++)
        {
            for (int y = 0; y < Board.Rows; y++)
            {
                if (board.Blocks [x, y].GetComponent<BlockChaining>().JustEmptied)
                {
                    for (int chainEligibleRow = y + 1; chainEligibleRow < Board.Rows; chainEligibleRow++)
                    {
                        if (board.Blocks [x, chainEligibleRow].State == Block.BlockState.Idle)
                        {
                            board.Blocks [x, chainEligibleRow].GetComponent<BlockChaining>().ChainEligible = true;
                            stopChain = false;
                        }
                    }
                }

                board.Blocks [x, y].GetComponent<BlockChaining>().JustEmptied = false;
            }
        }

        // stop the current chain if all of the blocks are idle or empty
        for (int x = 0; x < Board.Columns; x++)
        {
            for (int y = 0; y < Board.Rows; y++)
            {
                if (board.Blocks [x, y].State != Block.BlockState.Idle && 
                    board.Blocks [x, y].State != Block.BlockState.Empty && 
                    board.Blocks [x, y].State != Block.BlockState.Sliding)
                {
                    stopChain = false;
                }
            }
        }

        if (stopChain)
        {
            for (int x = 0; x < Board.Columns; x++)
            {
                for (int y = 0; y < Board.Rows; y++)
                {
                    board.Blocks [x, y].GetComponent<BlockChaining>().ChainEligible = false;
                }
            }

            if (ChainLength > 1)
            {
                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.ScoreChain(ChainLength);
                }
            }

            ChainLength = 1;
        }
    }
}