using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

	public static GameMaster Instance;
	public int score = 0;

	public int level;
	[SerializeField] Text levelText;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (this);
	}

	void Update()
	{
		level = ((int)Time.timeSinceLevelLoad + 61) / 60;
		levelText.text = "Level: " + level;
	}

	public void Restart()
	{
		StartCoroutine ("DeadReload");
	}

	public IEnumerator DeadReload()
	{
		for (int i = 0; i < 3; i++)
		{
			if (i == 1)
				Destroy (Player.Instance.gameObject);

			if(i == 2)
				SceneManager.LoadScene ("Start");

			yield return new WaitForSeconds (4f);
		}
	}
}
