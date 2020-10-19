using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoGame : MonoBehaviour
{

	void Start ()
	{
		
	}

	void OnEnable ()
	{
		GetComponent<Image> ().fillAmount = 1;
		StartCoroutine (DecreaseTime ());
	}

	void Update ()
	{
		
	}

	IEnumerator DecreaseTime ()
	{
		yield return new WaitForSeconds (0.5f); //0.05f
		if (GetComponent<Image> ().fillAmount > 0 && this.gameObject.activeSelf == true) {
			GetComponent<Image> ().fillAmount -= 0.067f;
			StartCoroutine (DecreaseTime ());
		} else {
			this.gameObject.SetActive (false);
		}
	}
}
