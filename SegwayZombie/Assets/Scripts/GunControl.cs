using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour {

	[SerializeField] GameObject bulletPrefab;
	[SerializeField] Transform gunPoint;
	[SerializeField] ParticleSystem spark;
	[SerializeField] float gunSpeed = 5f;
	[SerializeField] bool isMachineGun = false;

	Vector3 weaponRotation;
	float machineGunFireRate = 0.25f;
	float timeSinceFire = 0f;

	void Update()
	{
		WeaponRotation ();
		gunPoint.transform.rotation = Quaternion.LookRotation (weaponRotation);

		if (isMachineGun)
		{
			if (Input.GetKey (KeyCode.Mouse0) && Time.time > timeSinceFire + machineGunFireRate)
			{
				Fire ();
				timeSinceFire = Time.time;
			}
		}
		else if (Input.GetKeyDown (KeyCode.Mouse0))
			Fire ();
	}

	void Fire()
	{
		GameObject fire = Instantiate (bulletPrefab, gunPoint.position, gunPoint.rotation);
		Rigidbody rbBullet = fire.GetComponent<Rigidbody> ();
		spark.Play (true);

		rbBullet.velocity = fire.transform.forward.normalized * gunSpeed * Time.deltaTime;
	}

	void WeaponRotation()
	{
		weaponRotation = MousePosition.Instance.mousePosition - gunPoint.position;
	}

}
