using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	public float scrollSpeed;
	private float tileSizeZ;

	private Vector3 startPosition;

	void Start()
	{
		startPosition = transform.position;
		tileSizeZ = transform.localScale.y;
	}

	void Update()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
