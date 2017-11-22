using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

	public GameObject loadScreen;
	public Slider slider;
	public Text loadText;

	[SerializeField] GameObject instructions;

	public void StartGame()
	{
		instructions.SetActive (true);
	}

	public void LoadGame()
	{
		StartCoroutine(Load());
	}

	IEnumerator Load()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (1);
		instructions.SetActive (false);
		loadScreen.SetActive (true);

		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);
			slider.value = progress;
			loadText.text = (progress * 100f).ToString ("0") + "%";

			yield return null;
		}
	}
}
