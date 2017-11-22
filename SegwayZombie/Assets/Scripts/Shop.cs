using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	[SerializeField] GameObject shopUI;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
			shopUI.SetActive (true);
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") 
			shopUI.SetActive (false);
	}
}
