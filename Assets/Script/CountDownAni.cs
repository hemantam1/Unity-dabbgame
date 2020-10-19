using UnityEngine;
using System.Collections;

public class CountDownAni : MonoBehaviour
{

	public static CountDownAni instance;

	public bool _iSAnimationComplete = false;

	void Awake ()
	{
		instance = this;
	}

	public void InComplete ()
	{
		_iSAnimationComplete = false;
	}

	public void Complete ()
	{
		_iSAnimationComplete = true;
	}

	//	public void Start (int seconds)
	//	{
	//		StartCoroutine ("RunTimer", seconds);
	//	}
	//
	//	IEnumerator RunTimer (int seconds)
	//	{
	//		while (seconds > 0) {
	//			yield return new WaitForSeconds (1);
	//			seconds -= 1;
	//		}
	//	}
}

