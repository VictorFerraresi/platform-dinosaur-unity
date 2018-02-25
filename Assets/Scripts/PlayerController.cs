using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float moveSpeed = 6.0f;
	private float jumpHeight = 10.0f;

	private int actualSprite = 0;
	private float spriteInterval = 0.1f;
	private Sprite[] sprites = new Sprite[2];

	// Use this for initialization
	void Start () {
		sprites = Resources.LoadAll<Sprite> ("Art/Player");
		GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		spriteInterval -= Time.deltaTime;
		if ( spriteInterval < 0)
		{
			spriteInterval = 0.1f;
			GetComponent<SpriteRenderer> ().sprite = sprites[actualSprite];
			actualSprite = 1 - actualSprite;
		}

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if(Input.GetKeyDown(KeyCode.Space)) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
		}
	}

	// Called when a collision happens
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name.StartsWith("cactus")){
			GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = true;
			Time.timeScale = 0;
		}
	}
}