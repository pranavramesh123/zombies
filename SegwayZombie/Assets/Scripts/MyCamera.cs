using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {

	public float smoothing = 5f;
	public Vector3 offset = new Vector3 (0f, 15f, -22f);
	public GameObject player;
	Vector3 targetCamPos;

	void Reset()
	{
		offset = new Vector3 (0f, 15f, -22f);
	}

	void FixedUpdate()
	{
		if (player != null)
			targetCamPos = new Vector3(player.transform.position.x, 0f, player.transform.position.z) + offset;
		
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
