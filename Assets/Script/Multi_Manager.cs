using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using ExitGames.Client.Photon;
using SaveConstantVal;

public class Multi_Manager : MonoBehaviour
{
	//PhotonView Pview;
	public static Multi_Manager inst;
	int totalPlayer;
	public int lastValSave;
	public int FbUserNo;
	public string[] SaveFBIDs;
	public string[] Insta_Dow_url;

	void Awake ()
	{
		inst = this;
	}

	void Start ()
	{
		SaveFBIDs = new string[4];
		Insta_Dow_url = new string[4];

		//Pview = GetComponent <PhotonView> ();
	}

	void Update ()
	{
		
	}

	public void CloseDisplay ()
	{
		//Pview.RPC ("CloaseBlack", PhotonTargets.All);
	}

	//[PunRPC]
	void CloaseBlack ()
	{
		Playarea.instt.WaitScreenObj.SetActive (false);
	}

	public void DisplaySkipM (int val)
	{
		//Pview.RPC ("CallSkipMethodDisplay", PhotonTargets.Others, val);
	}

	//[PunRPC]
	void CallSkipMethodDisplay (int val)
	{
		Playarea.instt.SkipHandler (val);
	}

	public void SetPayerName_miltiplayer ()
	{
		//Pview.RPC ("SetNameMulti", PhotonTargets.All);
	}

	//[PunRPC]
	public void  SetNameMulti ()
	{
		//string PlayerNo = (string)PhotonNetwork.player.CustomProperties ["save_indx"];
//		int pnooo = int.Parse (PlayerNo);

//		Debug.Log ("call method");
		//foreach (PhotonPlayer ply in PhotonNetwork.playerList) {
		//	string indx1 = (string)ply.CustomProperties ["save_indx"];
		//	string namee = (string)ply.CustomProperties ["save_palyer_name"];
		//	string CoinVal = (string)ply.CustomProperties ["save_palyer_coin_val"];
		//	string GetFbID = (string)ply.CustomProperties ["save_FaceBook_ID"];
		//	string L_Type = (string)ply.CustomProperties ["Save_login_type"];
		//	string Inst_url = (string)ply.CustomProperties ["save_insta_profile_url"];
		//	string Tot_m = (string)ply.CustomProperties ["Total_Match_play"];
		//	string M_win = (string)ply.CustomProperties ["Total_Match_winnn"];

		//	int Loginn_Type = int.Parse (L_Type);

		//	int indx = int.Parse (indx1);
		//	if (indx != 0 && totalPlayer <= indx) {
		//		totalPlayer = indx;
		//		switch (indx) {
		//		case 1:
		//			Playarea.instt.PlayerDecide (0);
		//			ApiConstant.Total_Match_1 = int.Parse (Tot_m);
		//			ApiConstant.No_of_time_win_1 = int.Parse (M_win);
		//			Playarea.instt.playarea_text [0].text = namee;
		//			GameManager.instt.PlayerGoldTx [0].text = CoinVal.ToString ();
		//			GameManager.instt.PlayerGoldTx [4].text = CoinVal.ToString ();
		//			PlayerPrefs.SetInt (ApiConstant.TotalGold, int.Parse (CoinVal));

		//			if (Loginn_Type == 2 && Inst_url != null) {
		//				Insta_Dow_url [indx - 1] = Inst_url;
		//				Insta_img_Download.instt.Download_Image_1 (Inst_url);
		//			}
		//			break;
		//		case 2:
		//			Playarea.instt.PlayerDecide (1);
		//			ApiConstant.Total_Match_2 = int.Parse (Tot_m);
		//			ApiConstant.No_of_time_win_2 = int.Parse (M_win);
		//			Playarea.instt.playarea_text [1].text = namee;
		//			GameManager.instt.PlayerGoldTx [1].text = CoinVal.ToString ();
		//			PlayerPrefs.SetInt (ApiConstant.TotalGold_2, int.Parse (CoinVal));
		//			if (Loginn_Type == 2 && Inst_url != null) {
		//				Insta_Dow_url [indx - 1] = Inst_url;
		//				Insta_img_Download.instt.Download_Image_2 (Inst_url);
		//			}
		//			break;
		//		case 3:
		//			Playarea.instt.PlayerDecide (2);
		//			ApiConstant.Total_Match_3 = int.Parse (Tot_m);
		//			ApiConstant.No_of_time_win_3 = int.Parse (M_win);
		//			Playarea.instt.playarea_text [2].text = namee;
		//			GameManager.instt.PlayerGoldTx [2].text = CoinVal.ToString ();
		//			PlayerPrefs.SetInt (ApiConstant.TotalGold_3, int.Parse (CoinVal));
		//			if (Loginn_Type == 2 && Inst_url != null) {
		//				Insta_Dow_url [indx - 1] = Inst_url;
		//				Insta_img_Download.instt.Download_Image_3 (Inst_url);
		//			}
		//			break;
		//		case 4:
		//			Playarea.instt.PlayerDecide (3);
		//			ApiConstant.Total_Match_4 = int.Parse (Tot_m);
		//			ApiConstant.No_of_time_win_4 = int.Parse (M_win);
		//			Playarea.instt.playarea_text [3].text = namee;
		//			GameManager.instt.PlayerGoldTx [3].text = CoinVal.ToString ();
		//			PlayerPrefs.SetInt (ApiConstant.TotalGold_4, int.Parse (CoinVal));
		//			if (Loginn_Type == 2 && Inst_url != null) {
		//				Insta_Dow_url [indx - 1] = Inst_url;
		//				Insta_img_Download.instt.Download_Image_4 (Inst_url);
		//			}
		//			break;
		//		}
		//	}

			//=======fb img get======
			//if (indx != 0) {
			//	switch (indx) {
			//	case 1:
			//		if (Loginn_Type == 1 && GetFbID != "") {
			//			SaveFBIDs [0] = GetFbID;
			//			//FacebookManager.Instance.Download_Profile_image1 (GetFbID);
			//		}
			//		break;
			//	case 2:
			//		if (Loginn_Type == 1 && GetFbID != "") {
			//			SaveFBIDs [1] = GetFbID;
			//			//FacebookManager.Instance.Download_Profile_image2 (GetFbID);
			//		}
			//		break;
			//	case 3:
			//		if (Loginn_Type == 1 && GetFbID != "") {
			//			SaveFBIDs [2] = GetFbID;
			//			//FacebookManager.Instance.Download_Profile_image3 (GetFbID);
			//		}
			//		break;
			//	case 4:
			//		if (Loginn_Type == 1 && GetFbID != "") {
			//			SaveFBIDs [3] = GetFbID;
			//			//FacebookManager.Instance.Download_Profile_image4 (GetFbID);
			//		}
			//		break;
			//	}
			//}
			//=============
		//}
	}
	//=========================================

	//========== multiplayer dice show all player ========================================

	public void MultiplayerAnimationPlay ()
	{
		//Pview.RPC ("AnimationPlay", PhotonTargets.All);
	}

	//[PunRPC]
	public void  AnimationPlay ()
	{
		if (!Playarea.instt.isSamePlayerTurn) {
			Playarea.instt.TurnPass_multiplayer = true;
		}
		Playarea.instt.isStopAnim = true;
		Playarea.instt.Player_Dice_click ();
	}

	public void SetDiceValmultiplayer (int val)
	{
		//Pview.RPC ("DicevalSet_Multiplayer", PhotonTargets.All, val);
	}

	//[PunRPC]
	public void  DicevalSet_Multiplayer (int val)
	{
		Playarea.instt.isStopAnim = false;
		Playarea.instt.SetDiceVal_After_Anim (val);
		lastValSave = val;
	}

	public void Multiplayer_turn_pass ()
	{
		//Pview.RPC ("TurnPass_Multiplayer", PhotonTargets.Others);
	}

	//[PunRPC]
	public void  TurnPass_Multiplayer ()
	{
		Playarea.instt.PassTurn ();
	}

	public void Multiplayer_skipBt ()
	{
		//Pview.RPC ("Skipppp", PhotonTargets.Others);
	}

	//[PunRPC]
	public void  Skipppp ()
	{
		Playarea.instt.isSamePlayerTurn = false;
		Playarea.instt.StopCorutineFunction ();
		Playarea.instt.PassTurn ();
	}

	public void Save_SkipVal (int skip_no, int val)
	{
		//Pview.RPC ("Skip_Multiplayer", PhotonTargets.All, skip_no, val);
	}

	//[PunRPC]
	public void  Skip_Multiplayer (int skip_no, int val)
	{
		switch (skip_no) {
		case 1:
			Playarea.instt.Skip_BT [0].SetActive (false);
			Playarea.instt.skip_player1 = val;
			break;
		case 2:
			Playarea.instt.Skip_BT [1].SetActive (false);
			Playarea.instt.skip_player2 = val;
			break;
		case 3:
			Playarea.instt.Skip_BT [2].SetActive (false);
			Playarea.instt.skip_player3 = val;
			break;
		case 4:
			Playarea.instt.Skip_BT [3].SetActive (false);
			Playarea.instt.skip_player4 = val;
			break;
		}
	}

	public void Save_Undoo ()
	{
		//Pview.RPC ("Undo_multiplayer", PhotonTargets.All);
	}

	//[PunRPC]
	public void  Undo_multiplayer ()
	{
		Playarea.instt.Multiplayer_Undo_Bt_click ();
	}

	public void Multiplayer_Move_player (int indx_no, int CurrDiceVal)
	{
		//Pview.RPC ("PlayerMove_multi", PhotonTargets.All, indx_no, CurrDiceVal);
	}

	//[PunRPC]
	public void  PlayerMove_multi (int indx_no, int CurrDiceVal)
	{
		Playarea.instt.DiceValIS = CurrDiceVal;
		switch (indx_no) {
		case 10:
			Playarea.instt.Player1Manager [0].GetComponent<PlayerManager> ().multiplayer_all_palyerMove ();
			break;
		case 11:
			Playarea.instt.Player1Manager [1].GetComponent<PlayerManager> ().multiplayer_all_palyerMove ();
			break;
		case 20:
			Playarea.instt.Player2Manager [0].GetComponent<PlayerManager> ().multiplayer_all_palyerMove ();
			break;
		case 21:
			Playarea.instt.Player2Manager [1].GetComponent<PlayerManager> ().multiplayer_all_palyerMove ();
			break;
		case 30:
			Playarea.instt.Player3Manager [0].GetComponent<PlayerManager> ().multiplayer_all_palyerMove ();
			break;
		case 31:
			Playarea.instt.Player3Manager [1].GetComponent<PlayerManager> ().multiplayer_all_palyerMove ();
			break;
		case 40:
			Playarea.instt.Player4Manager [0].GetComponent<PlayerManager> ().multiplayer_all_palyerMove ();
			break;
		case 41:
			Playarea.instt.Player4Manager [1].GetComponent<PlayerManager> ().multiplayer_all_palyerMove ();
			break;
		}
	}
	//=====Safe zone

	public void Set_Safe_Zone (string one, string two, string three, string four, string five)
	{
		//Pview.RPC ("SafeZone_Multiplayer", PhotonTargets.All, one, two, three, four, five);
	}

	//[PunRPC]
	public void SafeZone_Multiplayer (string one, string two, string three, string four, string five)
	{
		Playarea.instt.Display_SafeZone_In_multiplayer (one, two, three, four, five);
	}

	//====================
	//===== question mark

	public void Set_Que_Zone (string one, string two, string three, string four, string five)
	{
		//Pview.RPC ("Ques_Multiplayer", PhotonTargets.All, one, two, three, four, five);
	}

	//[PunRPC]
	public void Ques_Multiplayer (string one, string two, string three, string four, string five)
	{
		Playarea.instt.Display_question_In_multiplayer (one, two, three, four, five);
	}

	//====================

	public void sendmesg (string nm, string msg)
	{
		int LangNumber = 0;
		if (PlayerPrefs.GetInt ("LanguageNumber") == 0) {
			LangNumber = 0;
		} else {
			LangNumber = 1;
		}
		print ("Language Is From Send Message" + LangNumber);
		//Pview.RPC ("messagesend", PhotonTargets.All, nm, msg, LangNumber);
	}

	public void imojisimg (string nm, int emono)
	{
		//int hj = emono + 1;
		int LangNumber = 0;
		if (PlayerPrefs.GetInt ("LanguageNumber") == 0) {
			LangNumber = 0;
		} else {
			LangNumber = 1;
		}
		print ("Language Is From Emoji" + LangNumber);
		//Pview.RPC ("emmosjiesno", PhotonTargets.All, nm, emono, LangNumber);
	}

	//[PunRPC]
	public void emmosjiesno (string nm, int emo, int LanguageNumber)
	{
		Playarea.instt.emojis (nm, emo, LanguageNumber);
	}

	//[PunRPC]
	public void messagesend (string nm, string msg, int languagenumber)
	{
		Playarea.instt.messeage (nm, msg, languagenumber);

	}
	//============
	public void Exchange_pos_multi (int p1_pno, int p1_pos, int p2_pno, int p2_pos)
	{
		//Pview.RPC ("changePoss", PhotonTargets.Others, p1_pno, p1_pos, p2_pno, p2_pos);
	}

	//[PunRPC]
	void changePoss (int p1_pno, int p1_pos, int p2_pno, int p2_pos)
	{
		Playarea.instt.switchPlayer_no1 = p1_pno;
		Playarea.instt.switch_indx1 = p1_pos;
		Playarea.instt.switchPlayer_no2 = p2_pno;
		Playarea.instt.switch_indx2 = p2_pos;
		Playarea.instt.Exchange_pos ();
	}

	//==================================================

	public void putBack_poss_multi (int p1_pno, int p1_pos)
	{
		//Pview.RPC ("PutBakPoss", PhotonTargets.Others, p1_pno, p1_pos);
	}

	//[PunRPC]
	void PutBakPoss (int p1_pno, int p1_pos)
	{
		Playarea.instt.switchPlayer_no1 = p1_pno;
		Playarea.instt.switch_indx1 = p1_pos;

		Playarea.instt.Put_IntoBack_pos ();
	}

	public void CardPopUp (string msg)
	{
		//Pview.RPC ("OpenAlertCardWithMsg", PhotonTargets.Others, msg);
	}

	//[PunRPC]
	void  OpenAlertCardWithMsg (string msg)
	{
		Playarea.instt.Open_Card_Popup (msg);
	}

	public void Card3Multi_skip (int val)
	{
		//Pview.RPC ("SkipOnePlayerTurn", PhotonTargets.Others, val);
	}

	//[PunRPC]
	void  SkipOnePlayerTurn (int val)
	{
		Playarea.instt.Is_3rd_Card_SkipTurnActive = true;
		Playarea.instt.WhichPlayer_Turn_Skip = val;
	}

	public void Card4Multi_rollAginn (int val)
	{
		//Pview.RPC ("RollForPlayer", PhotonTargets.Others, val);
	}

	//[PunRPC]
	void  RollForPlayer (int val)
	{
		Playarea.instt.Is_4th_RollTime = true;
		Playarea.instt.WhichPlayerRollAgin = val;
	}

	public void CardValueSet_multi (int val)
	{
		//Pview.RPC ("CardRandomValIs", PhotonTargets.All, val);
	}

	//[PunRPC]
	void  CardRandomValIs (int val)
	{
		Playarea.instt.RandomValForCar = val;
	}

	//==================================================
}
