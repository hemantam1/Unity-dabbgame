using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveConstantVal;
using UnityEngine.UI;

public class PlayOffline : MonoBehaviour
{

	public GameObject selectName;
	public GameObject selectOffilePlayer;
	public GameObject[] checkMarkObj;
	public GameObject[] playerName;

	void OnEnable ()
	{
		PlayerPrefs.SetInt (ApiConstant.offlinePlayerSelection, 4);
		selectOffilePlayer.SetActive (true);
		selectName.SetActive (false);
	}

	public void Select_1_player ()
	{
		PlayerPrefs.SetInt (ApiConstant.offlinePlayerSelection, 1);
		FalseCheckMarckObj (0);
	}

	public void Select_2_player ()
	{
		PlayerPrefs.SetInt (ApiConstant.offlinePlayerSelection, 2);
		FalseCheckMarckObj (1);
	}

	public void Select_3_player ()
	{
		PlayerPrefs.SetInt (ApiConstant.offlinePlayerSelection, 3);
		FalseCheckMarckObj (2);
	}

	public void Select_4_player ()
	{
		PlayerPrefs.SetInt (ApiConstant.offlinePlayerSelection, 4);
		FalseCheckMarckObj (3);
	}

	void FalseCheckMarckObj (int val)
	{
		for (int i = 0; i < 4; i++) {
			checkMarkObj [i].SetActive (false);
			playerName [i].SetActive (false);
		}
		checkMarkObj [val].SetActive (true);

		for (int i = 0; i <= val; i++) {
			playerName [i].SetActive (true);
		}

	}

	public void Play_Click ()
	{
		selectOffilePlayer.SetActive (false);
		selectName.SetActive (true);
	}

}
