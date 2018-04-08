using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class PlayerController : MonoBehaviour {
	
	private float moveSpeed = 6.0f;
	private float jumpHeight = 10.0f;

	private int actualSprite = 0;
	private float spriteInterval = 0.1f;
	private Sprite[] sprites = new Sprite[2];

	private String genomeBasePath = "Genomes/genome_";

	[SerializeField]
	private List<Cactus> cactus = new List<Cactus> ();

	[SerializeField]
	private List<Jumped> jumps = new List<Jumped>();

	private List<Genome> genomes = Utils.loadAllGenomes ();

	private bool isGrounded = true;

	// Use this for initialization
	void Start () {		
		sprites = Resources.LoadAll<Sprite> ("Art/Player");

		GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = false;

		//Load Cactus
		foreach(GameObject c in GameObject.FindGameObjectsWithTag("cactus"))
		{			
			int cacType;
			switch (c.name) {
				case "cactus_1":
					cacType = 1;
					break;
				case "cactus_2":
					cacType = 2;
					break;
				case "cactus_3":
					cacType = 3;
					break;
				default:
					cacType = 1;
					break;
			}
			Cactus toAdd = new Cactus () {
				type = cacType,
				position = c.transform.position
			};
			cactus.Add (toAdd);
		}
	}
	
	// Update is called once per frame
	void Update () {
		spriteInterval -= Time.deltaTime;
		if (spriteInterval < 0)
		{
			spriteInterval = 0.1f;
			GetComponent<SpriteRenderer> ().sprite = sprites[actualSprite];
			actualSprite = 1 - actualSprite;
		}

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if(isGrounded && Input.GetKeyDown(KeyCode.Space)) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
			isGrounded = false;

			Cactus c = getNextNearestCactus ();
			Jumped jump = new Jumped {
				nearestCactus = c,
				distanceToNearestCactus = GetComponent<Rigidbody2D> ().position -  c.position
			};

			jumps.Add (jump);
		}
	}

	// Called when a collision happens
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name.StartsWith ("cactus")) {			
			GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = true;
			Time.timeScale = 0;

			Genome genome = new Genome {
				fitness = Genetic.calculateFitness(jumps, cactus),
				jumps = jumps
			};
				
			JsonUtility.ToJson (genome);

			String filename = genomeBasePath + DateTime.Now.ToString("HHmmssddMMyy") + ".json";
			if (File.Exists(filename))
			{
				Debug.Log(filename+" already exists.");
				return;
			}
			var sr = File.CreateText(filename);
		
			sr.WriteLine (JsonUtility.ToJson (genome));
			sr.Close();

			jumps.Clear();
		} else if (coll.gameObject.name.StartsWith ("Ground")) {
			isGrounded = true;
		}
	}

	Cactus getNextNearestCactus() {
		float nearestDist = float.PositiveInfinity;
		Cactus nearestCactus = null;
		foreach(Cactus c in cactus)
		{
			float cacX = c.position.x;
			float playerX = GetComponent<Rigidbody2D> ().position.x;
			if (cacX > playerX) {
				float dist = cacX - playerX;
				if (dist < nearestDist) {
					nearestDist = dist;
					nearestCactus = c;
				}
			}
		}

		return nearestCactus;
	}
}