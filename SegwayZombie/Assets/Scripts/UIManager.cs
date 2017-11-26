using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager Instance;
	public Health playerHealth;

	[SerializeField] Text scoreText;
	[SerializeField] Text headShotText;
	[SerializeField] Text scoreGainText;
	int scoreTextOriginalSize;
	int scoreGainOriginalSize;
	int headShotTextOriginalSize;


	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (this);

		scoreTextOriginalSize = scoreText.fontSize;
		scoreGainOriginalSize = scoreGainText.fontSize;
		headShotTextOriginalSize = headShotText.fontSize;
	}

	//This function is called when a kill or puchase is made. It receives the changeAmount
	public void ScoreChange(int changeAmount)
	{
		GameMaster.Instance.score += changeAmount;

		if (changeAmount > 0)
		{
			scoreGainText.text = "Kill +" + changeAmount;
			StartCoroutine (Kill ());
		}

		scoreText.text = "Score: " + GameMaster.Instance.score;

		StartCoroutine ("ScorePop");
	}

	//Makes the overall score at top of screen pop
	IEnumerator ScorePop()
	{
		scoreText.fontSize = scoreTextOriginalSize;
		for (int i = 0; i < 4; i++)
		{
			if (i < 2)
				scoreText.fontSize++;
			else
				scoreText.fontSize--;

			yield return new WaitForSeconds (0.1f);
		}
		scoreText.fontSize = scoreTextOriginalSize;
	}

	//Shows the score gained next to kill, font size and alpha fade out
	IEnumerator Kill()
	{
		Color color = scoreGainText.color;
		color.a = 1f;
		scoreGainText.fontSize = scoreGainOriginalSize + 6;

		for (int i = 0; i < 50; i++)
		{
			color.a -= 0.02f;
			scoreGainText.color = color;

			if (i < 6)
				scoreGainText.fontSize -= 1;

			yield return new WaitForSeconds (0.02f);
		}
		
		scoreGainText.fontSize = scoreGainOriginalSize;
		color.a = 0f;
		scoreGainText.color = color;
	}

	//Shows "HeadShot" text next to kill, font size and alpha fade out
	public IEnumerator HeadShotKill()
	{
		Color color = headShotText.color;
		color.a = 1f;
		headShotText.fontSize = headShotTextOriginalSize + 6;

		for (int i = 0; i < 50; i++)
		{
			color.a -= 0.02f;
			headShotText.color = color;

			if (i < 6)
				headShotText.fontSize -= 1;

			yield return new WaitForSeconds (0.02f);
		}

		headShotText.fontSize = headShotTextOriginalSize;
		color.a = 0f;
		headShotText.color = color;
	}
}
