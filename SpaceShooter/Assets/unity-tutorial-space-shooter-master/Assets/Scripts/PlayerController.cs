using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float fireRate = 0.5F;
	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;

	private float myTime = 0.0F;
	private float nextFire = 0.5F;
	private Rigidbody rb;
	private AudioSource audioSource;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		myTime = myTime + Time.deltaTime;

		if (Input.GetButton("Fire1") && myTime > nextFire) {
			nextFire = myTime + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

			audioSource.Play();

			nextFire = nextFire - myTime;
			myTime = 0.0F;
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler(
			0.0f,
			0.0f,
			rb.velocity.x * -tilt
		);
	}
}
