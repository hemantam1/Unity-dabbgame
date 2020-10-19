using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveConstantVal;

public class ChooseLevel : MonoBehaviour
{
	//public ConnectAndJoinRandom connectAndJoinnn;
	int p1, p2, p3;
	public Text P1_tx, P2_tx, P3_tx;
	[HideInInspector]public	int p1_bid, p2_bid, p3_bid;

	void Start ()
	{
		p1 = p2 = p3 = 1;
		p1_bid = p2_bid = p3_bid = 1000;
	}

	void Update ()
	{
		
	}

	public void Player1_vs_1_bid ()
	{
		if (p1 != 7) {
			p1++;
		} else {
			p1 = 1;
		}

		//switch (p1) {

		//case 1:
		//	P1_tx.text = "1K";
		//	connectAndJoinnn.bidDIGIT = 4;
		//	p1_bid = 1000;
		//	connectAndJoinnn.BidAmout = 1000;
		//	break;
		//case 2:
		//	P1_tx.text = "2K";
		//	connectAndJoinnn.bidDIGIT = 4;
		//	p1_bid = 2000;
		//	connectAndJoinnn.BidAmout = 2000;
		//	break;
		//case 3:
		//	P1_tx.text = "5K";
		//	connectAndJoinnn.bidDIGIT = 4;
		//	p1_bid = 5000;
		//	connectAndJoinnn.BidAmout = 5000;
		//	break;
		//case 4:
		//	P1_tx.text = "10K";
		//	connectAndJoinnn.bidDIGIT = 5;
		//	p1_bid = 10000;
		//	connectAndJoinnn.BidAmout = 10000;
		//	break;
		//case 5:
		//	P1_tx.text = "50K";
		//	connectAndJoinnn.bidDIGIT = 5;
		//	p1_bid = 50000;
		//	connectAndJoinnn.BidAmout = 50000;
		//	break;
		//case 6:
		//	P1_tx.text = "100K";
		//	p1_bid = 100000;
		//	connectAndJoinnn.BidAmout = 100000;
		//	connectAndJoinnn.bidDIGIT = 6;
		//	break;
		//case 7:
		//	P1_tx.text = "1M";
		//	connectAndJoinnn.bidDIGIT = 7;
		//	p1_bid = 1000000;
		//	connectAndJoinnn.BidAmout = 1000000;
		//	break;
		//}
	}

	public bool Check1_vs_1_bidAmot ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.TotalGold) >= p1_bid) {
			PlayerPrefs.SetInt (ApiConstant.TotalGold, PlayerPrefs.GetInt (ApiConstant.TotalGold) - p1_bid);

			GameManager.instt.Set_Player_Gold_tx ();
			return true;
			
		} else {
			GameManager.instt.MsgText.text = "You don't have enough money ";
			GameManager.instt.messageDisplay.SetActive (true);
			return false;
		}
	}
}
