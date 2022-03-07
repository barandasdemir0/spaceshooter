using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");

		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}

		if (gameControllerObject == null) {
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) {
			// Ignore Boundary
			return;
		}

		if (explosion != null) {
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.CompareTag("Player")) {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

			gameController.GameOver();
		} else {
			gameController.AddScore(scoreValue); // Only add to the score when not hitting the Player!
		}

		Destroy(other.gameObject); // Bolt or Player
		Destroy(gameObject); // Asteroid
	}
}
