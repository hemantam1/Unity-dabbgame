using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveConstantVal;

public class DisplayCoinMultiplayer : MonoBehaviour
{
	public static DisplayCoinMultiplayer instt;

	void Awake ()
	{
		instt = this;
	}

	void Update ()
	{
		
	}



	public void  MultiPlayer_coin ()
	{
        //string PlayerNo = (string)PhotonNetwork.player.CustomProperties ["save_indx"];
        string PlayerNo = "1";
        int indx = int.Parse (PlayerNo);

		switch (indx) {
		case 1:
			GameManager.instt.PlayerGoldTx [0].text = "" + PlayerPrefs.GetInt (ApiConstant.TotalGold);
			GameManager.instt.PlayerGoldTx [4].text = "" + PlayerPrefs.GetInt (ApiConstant.TotalGold);
			break;
		case 2:
			GameManager.instt.PlayerGoldTx [1].text = "" + PlayerPrefs.GetInt (ApiConstant.TotalGold);
			break;
		case 3:
			GameManager.instt.PlayerGoldTx [2].text = "" + PlayerPrefs.GetInt (ApiConstant.TotalGold);
			break;
		case 4:
			GameManager.instt.PlayerGoldTx [3].text = "" + PlayerPrefs.GetInt (ApiConstant.TotalGold);
			break;
		}
	}
}
