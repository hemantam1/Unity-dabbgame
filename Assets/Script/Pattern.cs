using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{

	void Start ()
	{

		StartCoroutine (callll ());
	}

	void Update ()
	{


	}



	IEnumerator callll ()
	{
		yield return new WaitForSeconds (1);
		Debug.Log ("call");
		StartCoroutine (callll ());
	}
}
