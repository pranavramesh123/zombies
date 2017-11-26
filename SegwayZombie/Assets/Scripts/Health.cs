using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	[SerializeField] int currentHealth;
	[SerializeField] Slider slider;
	[SerializeField] GameObject purchaseMessagePanel;
	[SerializeField] Text purchaseMessageText;
	[SerializeField] AudioSource hAudio;
	[SerializeField] AudioClip dieAudio;
	[SerializeField] AudioClip hurtAudio;
	[SerializeField] GameObject gameOverText;
	[SerializeField] GameObject damageScreen;

	[HideInInspector] public bool dead = false;

	public void TakeDamage(int DamageAmount)
	{
		if (dead)
			return;

		if (currentHealth > 0)
			currentHealth = Mathf.Clamp (currentHealth - DamageAmount, 0, 100);

		slider.value = currentHealth;

		if (currentHealth <= 0)
		{
			StartCoroutine (DieScreen ());
			dead = true;
			GetComponent<Player> ().enabled = false;
			GunControl[] gunScripts = GetComponentsInChildren<GunControl> ();

			foreach (GunControl g in gunScripts)
			g.enabled = false;
		}
		else 
			StartCoroutine (HurtScreen ());
	}

	public void BuyHealth()
	{
		if (dead)
			return;

		if (GameMaster.Instance.score >= 1000 && currentHealth != 100)
		{
			UIManager.Instance.ScoreChange (-1000);
			currentHealth = 100;
			slider.value = currentHealth;
			StartCoroutine (HealthPurchaseMessage ("MAX HEALTH BOUGHT!"));
		} 
		else if (currentHealth == 100)
			StartCoroutine(HealthPurchaseMessage("ALREADY HAVE MAX HEALTH!"));
		else
			StartCoroutine (HealthPurchaseMessage ("NOT ENOUGH POINTS!"));
	}

	IEnumerator HealthPurchaseMessage(string s)
	{
		purchaseMessagePanel.SetActive (true);
		purchaseMessageText.text = s;
		yield return new WaitForSeconds (2f);
		purchaseMessagePanel.SetActive (false);
	}
		

	IEnumerator HurtScreen()
	{
		hAudio.clip = hurtAudio;
		hAudio.Play ();		

		Color original = damageScreen.GetComponent<Image> ().color;
		original.a = 0f;
		damageScreen.GetComponent<Image> ().color = original;

		for (int i = 0; i < 30; i++)
		{
			if (i < 15)
				original.a += 0.01f;
			else
				original.a -= 0.01f;

			damageScreen.GetComponent<Image> ().color = original;
			yield return new WaitForSeconds (0.02f);
		}
		original.a = 0f;
		damageScreen.GetComponent<Image> ().color = original;
	}

	IEnumerator DieScreen()
	{
		hAudio.clip = dieAudio;
		hAudio.Play ();

		GameMaster.Instance.StartCoroutine (GameMaster.Instance.DeadReload ());

		Color original = damageScreen.GetComponent<Image> ().color;
		original.a = 0f;
		damageScreen.GetComponent<Image> ().color = original;

		for (int i = 0; i < 50; i++)
		{
			original.a += 0.01f;
			damageScreen.GetComponent<Image> ().color = original;
			if (i == 25)
				gameOverText.SetActive (true);

			yield return new WaitForSeconds (0.1f);
		}
	}

}
