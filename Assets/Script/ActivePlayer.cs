using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveConstantVal;



public class ActivePlayer : MonoBehaviour
{
	public bool CharInPin;
	[HideInInspector]public Button Dicebt;

	[Header ("ALL PLAYER")]
	public Image[] Playerr;

	[Header ("MAIN PIN IMAGE")]
	public Image[] pinMainImg;

	[Header ("GREEN PIN")]
	//	[HideInInspector]
	public Sprite[] greenPin;

	[Header ("RED PIN")]
	//	[HideInInspector]
	public Sprite[] redPin;

	[Header ("BLUE PIN")]
	//	[HideInInspector]
	public Sprite[] bluePin;

	[Header ("YELLOW PIN")]
	//	[HideInInspector]	
	public Sprite[] yellowPin;

	[HideInInspector]public  int CurrentPlayerIS;
	[HideInInspector]public int playerNO;



	void OnEnable ()
	{
		//  1 - green  , 2 - red , 3 - yellow  , 4 - blue

		switch (CurrentPlayerIS) {
		case 1:
			if (!CharInPin) {
				for (int i = 0; i < pinMainImg.Length; i++) {
					pinMainImg [i].sprite = greenPin [i];
				}
			}
			break;

		case 2:
			for (int i = 0; i < pinMainImg.Length; i++) {

				pinMainImg [i].sprite = redPin [i];
			}
			break;

		case 3:

			for (int i = 0; i < pinMainImg.Length; i++) {

				pinMainImg [i].sprite = yellowPin [i];
			}
			break;

		case 4:
			for (int i = 0; i < pinMainImg.Length; i++) {

				pinMainImg [i].sprite = bluePin [i];
			}
			break;
		}

		Invoke ("PlayAnim", 0f);
	}

	void PlayAnim ()
	{
		GetComponent<Animator> ().enabled = true;
	}


	public void ReadyTOgo ()
	{
		this.gameObject.SetActive (false);
		GetComponent<Animator> ().enabled = false;
		Player_Active ();
		Playarea.instt.PassTurn ();
	}

	void Player_Active ()
	{
		switch (playerNO) {

		case 10:
			if (!Playarea.instt.IsPlayerOnSafeZone (Playerr [0].gameObject.GetComponent<PlayerManager> ().Position)) {
				Playerr [0].gameObject.GetComponent<PlayerManager> ().CheckAttackZone ();
			}
			Playerr [0].enabled = true;
			break;

		case 11:
			if (!Playarea.instt.IsPlayerOnSafeZone (Playerr [1].gameObject.GetComponent<PlayerManager> ().Position)) {
				Playerr [1].gameObject.GetComponent<PlayerManager> ().CheckAttackZone ();
			}
			Playerr [1].enabled = true;
			break;

		case 20:
			if (!Playarea.instt.IsPlayerOnSafeZone (Playerr [2].gameObject.GetComponent<PlayerManager> ().Position)) {
				Playerr [2].gameObject.GetComponent<PlayerManager> ().CheckAttackZone ();
			}
			Playerr [2].enabled = true;
			break;

		case 21:
			if (!Playarea.instt.IsPlayerOnSafeZone (Playerr [3].gameObject.GetComponent<PlayerManager> ().Position)) {
				Playerr [3].gameObject.GetComponent<PlayerManager> ().CheckAttackZone ();
			}
			Playerr [3].enabled = true;
			break;

		case 30:
			if (!Playarea.instt.IsPlayerOnSafeZone (Playerr [4].gameObject.GetComponent<PlayerManager> ().Position)) {
				Playerr [4].gameObject.GetComponent<PlayerManager> ().CheckAttackZone ();
			}
			Playerr [4].enabled = true;
			break;

		case 31:
			if (!Playarea.instt.IsPlayerOnSafeZone (Playerr [5].gameObject.GetComponent<PlayerManager> ().Position)) {
				Playerr [5].gameObject.GetComponent<PlayerManager> ().CheckAttackZone ();
			}
			Playerr [5].enabled = true;
			break;

		case 40:
			if (!Playarea.instt.IsPlayerOnSafeZone (Playerr [6].gameObject.GetComponent<PlayerManager> ().Position)) {
				Playerr [6].gameObject.GetComponent<PlayerManager> ().CheckAttackZone ();
			}
			Playerr [6].enabled = true;
			break;

		case 41:
			if (!Playarea.instt.IsPlayerOnSafeZone (Playerr [7].gameObject.GetComponent<PlayerManager> ().Position)) {
				Playerr [7].gameObject.GetComponent<PlayerManager> ().CheckAttackZone ();
			}
			Playerr [7].enabled = true;
			break;
		}
	}
}
