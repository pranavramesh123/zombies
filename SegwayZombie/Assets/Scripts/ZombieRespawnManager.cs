using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieRespawnManager : MonoBehaviour {

	[SerializeField] Transform[] respawnPoints = new Transform[8];
	[SerializeField] GameObject[] zombiePrefabs = new GameObject[3];
	[SerializeField] GameObject player;

	float time = 0f;
	float timeToNewZombie = 4f;

	void Update()
	{
		timeToNewZombie = 4f / GameMaster.Instance.level;

		if (Time.time > time + timeToNewZombie)
		{
			int zombieType = Random.Range (0, 3);
			GameObject zombie = Instantiate (zombiePrefabs [zombieType], respawnPoints [Random.Range (0, 8)]);

			zombie.GetComponent<NavMeshAgent> ().speed = Random.Range (GameMaster.Instance.level + 2, GameMaster.Instance.level + zombieType + 2);
			zombie.GetComponent<ZombieBehaviour> ().target = player;
			time = Time.time;
		}
	}
}
