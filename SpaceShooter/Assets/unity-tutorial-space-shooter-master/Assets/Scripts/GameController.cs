using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject[] asteroids;
	public Vector3 spawnValues;
	public int asteroidCount;
	public float startWait;
	public float spawnWait;
	public float waveWait;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private int score;
	private bool gameOver;
	private bool restart;

	void Start()
	{
		score = 0;
		UpdateScore();

		gameOver = false;
		gameOverText.text = "";

		restart = false;
		restartText.text = "";

		StartCoroutine(SpawnWaves());
	}

	void Update()
	{
		if (restart) {
			if (Input.GetKeyDown(KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);

		while(true) {
			for (int i = 0; i < asteroidCount; i++) {
				GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];

				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				Instantiate(asteroid, spawnPosition, spawnRotation);

				yield return new WaitForSeconds(spawnWait);
			}

			yield return new WaitForSeconds(waveWait);

			if (gameOver) {
				restart = true;
				restartText.text = "Press 'R' to Restart";
				break;
			}
		}
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score.ToString();
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverText.text = "GAME OVER";
	}
}
