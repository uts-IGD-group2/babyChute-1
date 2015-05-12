using UnityEngine;
using System.Collections;

public class BirdAnimator : MonoBehaviour {

	public Sprite[] sprites;
	public float framesPerSecond = 12;
	private Sprite initSpriteFrame;
	private SpriteRenderer spriteRenderer;

	void Awake ()
	{
		int rand = Random.Range(0, sprites.Length);
		print(rand);
		initSpriteFrame = sprites[rand];
	}

	void Start () 
	{
		spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
	}

	void Update () {
		int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
		index = index % sprites.Length;
		spriteRenderer.sprite = sprites[ index ];
	}


	void OnBecameInvisible() 
	{
		GetComponent<SpriteRenderer>().sprite = initSpriteFrame;
		
	}
}
