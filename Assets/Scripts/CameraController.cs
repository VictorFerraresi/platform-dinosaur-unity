using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Camera>().backgroundColor = Color.white;
		offset = transform.position - player.transform.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		Vector3 aux = player.transform.position + offset;
		aux.y = 0;
		transform.position = aux;
	}

	public void OnRestartButtonClicked()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}
}