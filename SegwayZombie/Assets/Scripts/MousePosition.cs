using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour {

	public static MousePosition Instance;

	[HideInInspector] public Vector3 mousePosition;
	[HideInInspector] public bool isValid;
	[SerializeField] LayerMask whatIsGround;
	[SerializeField] GameObject targetImage;

	Ray mouseRay;
	RaycastHit hit;
	Vector2 screenPosition;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (this);
	}

	void Update ()
	{
		isValid = false;
		targetImage.transform.position = Input.mousePosition;
		screenPosition = Input.mousePosition;
		mouseRay = Camera.main.ScreenPointToRay (screenPosition);

		if (Physics.Raycast (mouseRay, out hit, 100f, whatIsGround))
		{
			isValid = true;
			mousePosition = hit.point;
		}

	}
}
