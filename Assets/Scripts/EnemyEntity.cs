using UnityEngine;
using System.Collections;

public class EnemyEntity  : MonoBehaviour {
	
	// Use this for initialization
	public float speed = 1;
	private Sprite initSpriteFrame;

	public AudioClip SoundOnDeath;

    private GameController Game_Ctrl;


	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		GetComponent<Rigidbody2D>().velocity = transform.up * speed;

        Game_Ctrl = FindObjectOfType( typeof(GameController) ) as GameController;
	}


	public void HitPlayer() 
	{
		AudioSource.PlayClipAtPoint(SoundOnDeath, transform.position);
		Destroy(this.gameObject);
	}
}
