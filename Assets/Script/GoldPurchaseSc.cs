using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveConstantVal;

public class GoldPurchaseSc : MonoBehaviour
{
	//	public Text[] AllplayerGems;

	void Start ()
	{
		GameManager.instt.Set_Player_Gold_tx ();

//		AllplayerGems [0].text = "" + PlayerPrefs.GetInt (ApiConstant.TotalGoEMS);
//		AllplayerGems [1].text = "" + PlayerPrefs.GetInt (ApiConstant.TotalGoEMS_2);
//		AllplayerGems [2].text = "" + PlayerPrefs.GetInt (ApiConstant.TotalGoEMS_3);
//		AllplayerGems [3].text = "" + PlayerPrefs.GetInt (ApiConstant.TotalGoEMS_4);
	}

	public void Purchase_10k_gold ()
	{
		//=========
		if (PlayerPrefs.GetInt (ApiConstant.multiplayerGame) == 1) {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 10000);
			DisplayCoinMultiplayer.instt.MultiPlayer_coin ();
		} else {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 10000);
			GameManager.instt.Set_Player_Gold_tx ();
		}
		//========
	}

	public void Purchase_20k_gold ()
	{
		//=========
		if (PlayerPrefs.GetInt (ApiConstant.multiplayerGame) == 1) {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 20000);
			DisplayCoinMultiplayer.instt.MultiPlayer_coin ();
		} else {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 20000);
			GameManager.instt.Set_Player_Gold_tx ();
		}
		//========
	}

	public void Purchase_40k_gold ()
	{
		//=========
		if (PlayerPrefs.GetInt (ApiConstant.multiplayerGame) == 1) {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 40000);
			DisplayCoinMultiplayer.instt.MultiPlayer_coin ();
		} else {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 40000);
			GameManager.instt.Set_Player_Gold_tx ();
		}
		//========
	}

	public void Purchase_100k_gold ()
	{
		//=========
		if (PlayerPrefs.GetInt (ApiConstant.multiplayerGame) == 1) {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 100000);
			DisplayCoinMultiplayer.instt.MultiPlayer_coin ();
		} else {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 100000);
			GameManager.instt.Set_Player_Gold_tx ();
		}
		//========
	}

	public void Purchase_250k_gold ()
	{
		//=========
		if (PlayerPrefs.GetInt (ApiConstant.multiplayerGame) == 1) {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 250000);
			DisplayCoinMultiplayer.instt.MultiPlayer_coin ();
		} else {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 250000);
			GameManager.instt.Set_Player_Gold_tx ();
		}
		//========
	}

	public void Purchase_1M_gold ()
	{
		//=========
		if (PlayerPrefs.GetInt (ApiConstant.multiplayerGame) == 1) {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 1000000);
			DisplayCoinMultiplayer.instt.MultiPlayer_coin ();
		} else {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) + 1000000);
			GameManager.instt.Set_Player_Gold_tx ();
		}
		//========
	}
}
