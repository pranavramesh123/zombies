using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour {

	[SerializeField] UnityEngine.AI.NavMeshAgent navMeshAgent;
	[SerializeField] int damageAmount = 10;
	[SerializeField] WaitForSeconds damageSpeed = new WaitForSeconds(2f);
	public GameObject target;

	bool playerInRange = false;

	void Start()
	{
		StartCoroutine (ChasePlayer());
	}

	IEnumerator ChasePlayer()
	{
		yield return null;

		while (navMeshAgent.enabled)
		{
			if(target != null)
			navMeshAgent.SetDestination (target.transform.position);

			yield return null;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") 
		{
			playerInRange = true;
			StartCoroutine ("AttackPlayer");
		}

	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") 
		{
			playerInRange = false;
			StopCoroutine ("AttackPlayer");
		}
	}

	IEnumerator AttackPlayer()
	{
		yield return new WaitForSeconds (0.05f);
		while (playerInRange)
		{
			UIManager.Instance.playerHealth.TakeDamage (damageAmount);
			yield return damageSpeed;
		}
	}
}
