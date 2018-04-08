using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {


	public Text scoreText;
	private int score;
	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore ();
	}
	
	// Update is called once per frame
	void Update () {
		score++;
		UpdateScore ();
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
}
