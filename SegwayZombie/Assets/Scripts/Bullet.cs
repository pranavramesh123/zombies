using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] ParticleSystem particleEffect;
	[SerializeField] AudioSource soundEffect;
	[SerializeField] ParticleSystem bodyBloodEffect;
	[SerializeField] int bulletDamage = 10;
	bool somethingHit = false;

	void Awake()
	{
		Invoke ("DestroyBullet", 4f);
	}

	void OnCollisionEnter(Collision col)
	{
		if (somethingHit)
			return;

		GetComponent<BoxCollider> ().enabled = false;
		Destroy (GetComponent<Rigidbody> ());

		if (col.gameObject.tag == "EnemyHead")
		{
			col.gameObject.GetComponentInParent<ZombieHealth> ().TakeDamage (bulletDamage,true);
			StartCoroutine (BulletHitZombie());
		} 
		else if(col.gameObject.tag == "EnemyBody")
		{
			col.gameObject.GetComponentInParent<ZombieHealth> ().TakeDamage (bulletDamage,false);
			bodyBloodEffect.Play ();
			StartCoroutine (BulletHitZombie());
		}
		else
		{
			StartCoroutine (BulletHitObject());
		}
			
		somethingHit = true;
	}
		

	public IEnumerator BulletHitObject()
	{
		particleEffect.Play (true);
		soundEffect.Play ();
		yield return new WaitForSeconds (0.5f);

		Destroy (gameObject);
	}

	public IEnumerator BulletHitZombie()
	{	
		soundEffect.Play ();
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}

	void DestroyBullet()
	{
		Destroy (gameObject);
	}
		
}
