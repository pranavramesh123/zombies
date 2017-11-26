using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

	[SerializeField] GameObject pistol;
	[SerializeField] GameObject machineGun;
	[SerializeField] GameObject pistolImage;
	[SerializeField] GameObject machineGunImage;
	[SerializeField] GameObject reserveGun;
	[SerializeField] GameObject pistolImage2;
	[SerializeField] GameObject machineGunImage2;
	[SerializeField] GameObject disabledMachineGun;
	[SerializeField] GameObject purchaseMessagePanel;
	[SerializeField] Text purchaseMessageText;

	bool hasMachineGun = false;

	public void BuyMachineGune()
	{
		if (Player.Instance.GetComponent<Health> ().dead)
			return;

		if (GameMaster.Instance.score >= 2000 && !hasMachineGun) 
		{
			UIManager.Instance.ScoreChange (-2000);
			hasMachineGun = true;
			reserveGun.SetActive (true);
			SwitchWeapon ();
			disabledMachineGun.SetActive (false);
			StartCoroutine (GunPurchaseMessage ("MACHINE GUN BOUGHT!"));
		} 
		else if (hasMachineGun)
		{
			StartCoroutine(GunPurchaseMessage("ALREADY HAVE MACHINE GUN!"));
		}
		else
		{
			StartCoroutine (GunPurchaseMessage ("NOT ENOUGH POINTS!"));
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space) && hasMachineGun)
			SwitchWeapon ();
	}

	void SwitchWeapon()
	{
		if (pistol.activeSelf)
		{
			pistol.SetActive (false);
			pistolImage.SetActive (false);
			pistolImage2.SetActive (true);

			machineGun.SetActive (true);
			machineGunImage.SetActive (true);
			machineGunImage2.SetActive (false);
		}
		else
		{
			machineGun.SetActive (false);
			machineGunImage.SetActive (false);
			machineGunImage2.SetActive (true);

			pistol.SetActive (true);
			pistolImage.SetActive (true);
			pistolImage2.SetActive (false);
		}
	}

	IEnumerator GunPurchaseMessage(string s)
	{
		purchaseMessagePanel.SetActive (true);
		purchaseMessageText.text = s;
		yield return new WaitForSeconds (2f);

		purchaseMessagePanel.SetActive (false);
	}

}
