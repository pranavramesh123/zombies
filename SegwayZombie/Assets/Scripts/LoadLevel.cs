using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

	[SerializeField] GameObject loadScreen;
	[SerializeField] Slider slider;
	[SerializeField] Text loadText;
	[SerializeField] GameObject instructions;
	bool barComplete = false;

	public void StartGame()
	{
		instructions.SetActive (true);
	}

	public void LoadGame()
	{
		StartCoroutine (LoadBar ());
	}

	IEnumerator LoadBar()
	{
		instructions.SetActive (false);
		loadScreen.SetActive (true);
		int rand = Random.Range (5, 30);
		for (int i = 0; i <= 100; i++)
		{
			slider.value = i / 100f;
			loadText.text = i + "%";

			//SceneManager.LoadSceneAsync causes the game to stop for a short time before operation.progress starts, so this short time should be a random point at start of load bar
			if (i == rand)
				StartCoroutine (Load());

			yield return null;
		}
		barComplete = true;
	}

	IEnumerator Load()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (1);
		operation.allowSceneActivation = false;

		while (!barComplete)
		{
			yield return null;
		}

		operation.allowSceneActivation = true;
		/*
		//Alternative method: The operation.progress jumped from 0 to 100 really quickly and didn't look right.
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);
			slider.value = progress;
			loadText.text = (progress * 100f).ToString ("0") + "%";
			yield return null;
		}
		*/
	}
}
