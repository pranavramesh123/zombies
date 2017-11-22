using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour {

	[SerializeField] int currentHealth;
	[SerializeField] int deathScore;
	[SerializeField] ParticleSystem headBloodEffect;
	[SerializeField] GameObject head;
	[SerializeField] UnityEngine.AI.NavMeshAgent navMesh;
	[SerializeField] ZombieBehaviour zombieBehaviour;
	[SerializeField] UnityEngine.UI.Slider slider;
	[SerializeField] GameObject healthBar;
	bool dead = false;

	void Update()
	{
		healthBar.transform.rotation = Quaternion.Euler (new Vector3(0f, 0f, 0f));
	}

	public void TakeDamage(int damageAmount, bool headHit)
	{
		if (headHit && head != null)
			ExplodeHead ();

		if (!dead && !headHit)
		{
			currentHealth -= damageAmount;
			if (currentHealth <= 0)
				StartCoroutine(Die ());
		}
		slider.value = currentHealth;
	}
		

	IEnumerator Die()
	{
		dead = true;
		currentHealth = 0;
		UIManager.Instance.ScoreChange (deathScore);
		navMesh.enabled = false;
		zombieBehaviour.enabled = false;

		yield return new WaitForSeconds (1f);

		Destroy (gameObject);
	}

	void ExplodeHead()
	{
		Destroy (head);
		headBloodEffect.Play ();

		if (!dead)
		{
			UIManager.Instance.StartCoroutine(UIManager.Instance.HeadShotKill());
			StartCoroutine (Die ());
		}
	}

}
