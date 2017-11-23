using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player Instance;
	public Vector3 MoveDirection = Vector3.zero;
	public Vector3 LookDirection = Vector3.forward;
	public float speed = 6f;
	public Rigidbody rb;
	Vector3 weaponRotation;

	[SerializeField] GameObject aimPivot;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (this);
	}

	void Update()
	{
		HandleMoveInput ();
	}

	void FixedUpdate()
	{
		rb.velocity = MoveDirection.normalized * speed;

		LookDirection = new Vector3 (LookDirection.x, 0f, LookDirection.z);
		rb.MoveRotation (Quaternion.Lerp(transform.rotation, Quaternion.LookRotation (LookDirection), 10f * Time.deltaTime));

		//Handling aim
		WeaponRotation ();
		aimPivot.transform.rotation = Quaternion.Lerp(aimPivot.transform.rotation, Quaternion.LookRotation (weaponRotation), 10f * Time.deltaTime);
	}

	void HandleMoveInput()
	{
		float horizonal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		MoveDirection = new Vector3 (horizonal, 0f, vertical);

		if (MousePosition.Instance != null && MousePosition.Instance.isValid)
			LookDirection = MousePosition.Instance.mousePosition - transform.position;

	}

	void WeaponRotation()
	{
		weaponRotation = MousePosition.Instance.mousePosition - aimPivot.transform.position;
	}
}
