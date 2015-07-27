﻿using UnityEngine;
using System.Collections;

public class BlockFaller : MonoBehaviour
{
	Block block;
	float delayElapsed;
	const float delayDuration = 0.1f;
	public float Elapsed;
	public const float Duration = 0.1f;
	public Block Target;
	public bool JustFell;
	BlockChaining chaining;

	// Use this for initialization
	void Awake ()
	{
		block = GetComponent<Block> ();
		chaining = GetComponent<BlockChaining> ();
	}
	
	public void Fall ()
	{
		block.State = Block.BlockState.WaitingToFall;

		delayElapsed = 0.0f;
	}

	public void ContinueFalling ()
	{
		FinishWaitingToFall ();
	}

	void FinishWaitingToFall ()
	{
		block.State = Block.BlockState.Falling;

		Elapsed = 0.0f;
	}

	// Update is called once per frame
	void Update ()
	{
		if (block.State == Block.BlockState.WaitingToFall) {
			delayElapsed += Time.deltaTime;

			if (delayElapsed >= delayDuration) {
				FinishWaitingToFall ();
			}
		}

		if (block.State == Block.BlockState.Falling) {
			Elapsed += Time.deltaTime;

			if (Elapsed >= Duration) {
				Target.State = Block.BlockState.Falling;
				Target.Type = block.Type;
				Target.GetComponent<BlockChaining> ().ChainEligible = chaining.ChainEligible;
				Target.GetComponent<BlockFaller> ().JustFell = true;

				block.State = Block.BlockState.Empty;
				block.Type = -1;
				chaining.ChainEligible = false;
			}
		}
	}
}
