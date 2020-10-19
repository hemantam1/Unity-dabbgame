using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveConstantVal;


public class DicePurchase : MonoBehaviour
{
	public static DicePurchase instance;
	public GameObject[] allButton;
	public GameObject[] tickMark;
	public Sprite[] dice_1;
	public Sprite[] dice_2;
	public Sprite[] dice_3;
	public Sprite[] dice_4;
	public Sprite[] dice_5;
	public Sprite[] dice_6;

	public Sprite[] oneTOSix_1;
	public Sprite[] oneTOSix_2;
	public Sprite[] oneTOSix_3;
	public Sprite[] oneTOSix_4;
	public Sprite[] oneTOSix_5;
	public Sprite[] oneTOSix_6;

	void Awake ()
	{
		instance = this;
	}

	void Start ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.DicePur1) == 1) {
			allButton [0].SetActive (false);
		}
		if (PlayerPrefs.GetInt (ApiConstant.DicePur2) == 1) {
			allButton [1].SetActive (false);
		}
		if (PlayerPrefs.GetInt (ApiConstant.DicePur3) == 1) {
			allButton [2].SetActive (false);
		}
		if (PlayerPrefs.GetInt (ApiConstant.DicePur4) == 1) {
			allButton [3].SetActive (false);
		}
		if (PlayerPrefs.GetInt (ApiConstant.DicePur5) == 1) {
			allButton [4].SetActive (false);
		}
		if (PlayerPrefs.GetInt (ApiConstant.DicePur6) == 1) {
			allButton [5].SetActive (false);
		}

		Apply_Dice ();
	}

	void Update ()
	{
		
	}

	public void  CloseALlRickMark (int val)
	{
		for (int i = 0; i < 6; i++) {
			tickMark [i].SetActive (false);
		}
		tickMark [val].SetActive (true);
	}

	public void BuyChar_1 ()
	{
//		OpenIABTest.instance.ProductsPurchase (ApiConstant.DicePur1);

		print ("Button Pressed Dice 1");
		
		PlayerPrefs.SetInt (ApiConstant.DicePur1, 1);
		allButton [0].SetActive (false);
	}

	public void BuyChar_2 ()
	{
//		OpenIABTest.instance.ProductsPurchase (ApiConstant.DicePur2);

		print ("Button Pressed Dice 2");

		PlayerPrefs.SetInt (ApiConstant.DicePur2, 1);
		allButton [1].SetActive (false);
	}

	public void BuyChar_3 ()
	{
//		OpenIABTest.instance.ProductsPurchase (ApiConstant.DicePur3);
		print ("Button Pressed Dice 3");
		PlayerPrefs.SetInt (ApiConstant.DicePur3, 1);
		allButton [2].SetActive (false);
	}

	public void BuyChar_4 ()
	{
//		OpenIABTest.instance.ProductsPurchase (ApiConstant.DicePur4);
		print ("Button Pressed Dice 4");
		PlayerPrefs.SetInt (ApiConstant.DicePur4, 1);
		allButton [3].SetActive (false);
	}

	public void BuyChar_5 ()
	{
//		OpenIABTest.instance.ProductsPurchase (ApiConstant.DicePur5);
		print ("Button Pressed Dice 5");
		PlayerPrefs.SetInt (ApiConstant.DicePur5, 1);
		allButton [4].SetActive (false);
	}

	public void BuyChar_6 ()
	{
//		OpenIABTest.instance.ProductsPurchase (ApiConstant.DicePur5);
		print ("Button Pressed Dice 6");
		PlayerPrefs.SetInt (ApiConstant.DicePur6, 1);
		allButton [5].SetActive (false);
	}

	public void SelectDice_1 ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.DicePur1) == 1) {
			PlayerPrefs.SetInt (ApiConstant.Chr_Dive_Apply, 1);
			Apply_Dice ();
			CloseALlRickMark (0);
		}
	}

	public void SelectDice_2 ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.DicePur2) == 1) {
			PlayerPrefs.SetInt (ApiConstant.Chr_Dive_Apply, 2);
			Apply_Dice ();
		}
	}

	public void SelectDice_3 ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.DicePur3) == 1) {
			PlayerPrefs.SetInt (ApiConstant.Chr_Dive_Apply, 3);
			Apply_Dice ();
		}
	}

	public void SelectDice_4 ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.DicePur4) == 1) {
			PlayerPrefs.SetInt (ApiConstant.Chr_Dive_Apply, 4);
			Apply_Dice ();
		}
	}

	public void SelectDice_5 ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.DicePur5) == 1) {
			PlayerPrefs.SetInt (ApiConstant.Chr_Dive_Apply, 5);
			Apply_Dice ();
		}
	}

	public void SelectDice_6 ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.DicePur6) == 1) {
			PlayerPrefs.SetInt (ApiConstant.Chr_Dive_Apply, 6);
			Apply_Dice ();
		}
	}

	void Apply_Dice ()
	{
		switch (PlayerPrefs.GetInt (ApiConstant.Chr_Dive_Apply)) {
		case 1:
			CloseALlRickMark (0);
			//==================
			GameManager.instt.playarea_SCript.diceImg.sprite = oneTOSix_1 [1];
			for (int i = 0; i < 8; i++) {
				GameManager.instt.playarea_SCript.diceAllImage [i] = dice_1 [i];
			}
			for (int j = 0; j < 6; j++) {
				GameManager.instt.playarea_SCript.oneTosixImg [j] = oneTOSix_1 [j];
			}
			//==================
			break;
		case 2:
			CloseALlRickMark (1);
			//==================
			GameManager.instt.playarea_SCript.diceImg.sprite = oneTOSix_2 [1];
			for (int i = 0; i < 8; i++) {
				GameManager.instt.playarea_SCript.diceAllImage [i] = dice_2 [i];
			}
			for (int j = 0; j < 6; j++) {
				GameManager.instt.playarea_SCript.oneTosixImg [j] = oneTOSix_2 [j];
			}
			//==================
			break;
		case 3:
			CloseALlRickMark (2);
			//==================
			GameManager.instt.playarea_SCript.diceImg.sprite = oneTOSix_3 [1];
			for (int i = 0; i < 8; i++) {
				GameManager.instt.playarea_SCript.diceAllImage [i] = dice_3 [i];
			}
			for (int j = 0; j < 6; j++) {
				GameManager.instt.playarea_SCript.oneTosixImg [j] = oneTOSix_3 [j];
			}
			//==================
			break;
		case 4:
			CloseALlRickMark (3);
			//==================
			GameManager.instt.playarea_SCript.diceImg.sprite = oneTOSix_4 [1];
			for (int i = 0; i < 8; i++) {
				GameManager.instt.playarea_SCript.diceAllImage [i] = dice_4 [i];
			}
			for (int j = 0; j < 6; j++) {
				GameManager.instt.playarea_SCript.oneTosixImg [j] = oneTOSix_4 [j];
			}
			//==================
			break;
		case 5:
			CloseALlRickMark (4);
			//==================
			GameManager.instt.playarea_SCript.diceImg.sprite = oneTOSix_5 [1];
			for (int i = 0; i < 8; i++) {
				GameManager.instt.playarea_SCript.diceAllImage [i] = dice_5 [i];
			}
			for (int j = 0; j < 6; j++) {
				GameManager.instt.playarea_SCript.oneTosixImg [j] = oneTOSix_5 [j];
			}
			//==================
			break;
		case 6:
			CloseALlRickMark (5);
			//==================
			GameManager.instt.playarea_SCript.diceImg.sprite = oneTOSix_6 [1];
			for (int i = 0; i < 8; i++) {
				GameManager.instt.playarea_SCript.diceAllImage [i] = dice_6 [i];
			}
			for (int j = 0; j < 6; j++) {
				GameManager.instt.playarea_SCript.oneTosixImg [j] = oneTOSix_6 [j];
			}
			//==================
			break;
		}
	}
}
