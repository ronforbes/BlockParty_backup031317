using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockRenderer : MonoBehaviour
{
	Block block;
	BlockSlider slider;
	BlockFaller faller;
	BlockClearer clearer;
	BoardRaiser raiser;
	SpriteRenderer spriteRenderer;
	SpriteRenderer matchGradientRenderer;
	ParticleSystem particles;
	SpriteRenderer additiveLayer;
	public List<Sprite> Sprites;

	// Use this for initialization
	void Awake ()
	{
		block = GetComponent<Block> ();
		slider = GetComponent<BlockSlider> ();
		faller = GetComponent<BlockFaller> ();
		clearer = GetComponent<BlockClearer> ();
		raiser = GameObject.Find ("Board").GetComponent<BoardRaiser> ();
		spriteRenderer = transform.Find ("Sprite").GetComponent<SpriteRenderer> ();
		matchGradientRenderer = transform.Find ("Match Gradient").GetComponent<SpriteRenderer> ();
		particles = transform.Find ("Match Particles").GetComponent<ParticleSystem> ();
		additiveLayer = transform.Find ("Additive Layer").GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update ()
	{
		float timePercentage = 0.0f;
		float raiseOffset = raiser.Elapsed / BoardRaiser.Duration;

		switch (block.State) {
		case Block.BlockState.Empty:
			transform.position = new Vector3 (block.X, block.Y + raiseOffset, 0.0f);
			spriteRenderer.transform.localScale = Vector3.one;
			matchGradientRenderer.transform.localScale = Vector3.one;

			spriteRenderer.enabled = false;
			matchGradientRenderer.enabled = false;
			break;
            
		case Block.BlockState.Idle:
			transform.position = new Vector3 (block.X, block.Y + raiseOffset, 0.0f);
            
			spriteRenderer.enabled = true;
			spriteRenderer.sprite = Sprites [block.Type];

			matchGradientRenderer.enabled = false;

			if (block.Y != 0)
				spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			else
				spriteRenderer.color = new Color (0.5f, 0.5f, 0.5f, 1.0f);
			break;

		case Block.BlockState.Sliding:
			float destination = 0.0f;
			if (slider.Direction == BlockSlider.SlideDirection.Left) {
				destination = -transform.localScale.x;
			}

			if (slider.Direction == BlockSlider.SlideDirection.Right) {
				destination = transform.localScale.x;
			}

			timePercentage = slider.Elapsed / BlockSlider.Duration;
			transform.position = Vector3.Lerp (new Vector3 (block.X, block.Y + raiseOffset, 0.0f), new Vector3 (block.X + destination, block.Y + raiseOffset, 0.0f), timePercentage);

			if (block.Type == -1) {
				spriteRenderer.enabled = false;
			} else {
				spriteRenderer.enabled = true;
				spriteRenderer.sprite = Sprites [block.Type];
				spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			}
			break;

		case Block.BlockState.WaitingToFall:
			transform.position = new Vector3 (block.X, block.Y + raiseOffset, 0.0f);
			spriteRenderer.enabled = true;
			spriteRenderer.sprite = Sprites [block.Type];
			spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			break;

		case Block.BlockState.Falling:
			timePercentage = faller.Elapsed / BlockFaller.Duration;
			transform.position = Vector3.Lerp (new Vector3 (block.X, block.Y + raiseOffset, 0.0f), new Vector3 (block.X, block.Y + raiseOffset - transform.localScale.y, 0.0f), timePercentage);

			spriteRenderer.enabled = true;
			spriteRenderer.sprite = Sprites [block.Type];
			spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			break;

		case Block.BlockState.Matched:
			transform.position = new Vector3 (block.X, block.Y + raiseOffset, 0.0f);

			spriteRenderer.enabled = true;
			spriteRenderer.sprite = Sprites [block.Type];
			spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

			// Setup the additive layer to highlight the block
			additiveLayer.enabled = true;
			additiveLayer.transform.localScale = Vector3.one;
			additiveLayer.sprite = Sprites [block.Type];
			additiveLayer.color = new Color (1.0f, 1.0f, 1.0f, 0.25f);
			break;

		case Block.BlockState.WaitingToClear:
			transform.position = new Vector3 (block.X, block.Y + raiseOffset, 0.0f);

			spriteRenderer.enabled = true;
			spriteRenderer.sprite = Sprites [block.Type];
			spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

			// Setup the additive layer to highlight the block
			additiveLayer.enabled = true;
			additiveLayer.sprite = Sprites [block.Type];
			additiveLayer.color = new Color (1.0f, 1.0f, 1.0f, 0.25f);
			break;

		case Block.BlockState.Clearing:
			transform.position = new Vector3 (block.X, block.Y + raiseOffset, 0.0f);
                
			spriteRenderer.enabled = true;
			spriteRenderer.sprite = Sprites [block.Type];

			float alpha = 1.0f - clearer.Elapsed / BlockClearer.Duration;
			spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, alpha);

			float scale = 1.0f - clearer.Elapsed / BlockClearer.Duration;
			spriteRenderer.transform.localScale = new Vector3 (scale, scale, scale);

			matchGradientRenderer.enabled = true;

			float gradientAlpha = 1.0f - clearer.Elapsed / BlockClearer.Duration;
			matchGradientRenderer.color = new Color (1.0f, 1.0f, 1.0f, gradientAlpha);

			float gradientScale = 0.5f + 1.0f * clearer.Elapsed / BlockClearer.Duration;
			matchGradientRenderer.transform.localScale = new Vector3 (gradientScale, gradientScale, gradientScale);

			particles.Play ();

			// Setup the additive layer to highlight the block and shrink throughout the clear
			additiveLayer.enabled = true;
			additiveLayer.sprite = Sprites [block.Type];
			additiveLayer.color = new Color (1.0f, 1.0f, 1.0f, 0.25f);
			additiveLayer.transform.localScale = new Vector3 (scale, scale, scale);
			break;

		case Block.BlockState.WaitingToEmpty:
			transform.position = new Vector3 (block.X, block.Y + raiseOffset, 0.0f);
			transform.localScale = Vector3.one;

			spriteRenderer.enabled = false;
			matchGradientRenderer.enabled = false;
			additiveLayer.enabled = false;
			break;
		}


	}
}
