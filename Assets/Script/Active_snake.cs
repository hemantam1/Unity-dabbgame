using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveConstantVal;


public class Active_snake : MonoBehaviour
{

	[HideInInspector]public Image Current_snake;
	[HideInInspector]public Button Dicebt;


	[Header ("Snake")]
	[HideInInspector]public Image[] Playerr;

	public Image pinImg;
	[HideInInspector]public Sprite[] allPinImg;

	[HideInInspector]public  int CurrentPlayerIS;
	[HideInInspector]public int playerNO;



	void OnEnable ()
	{
        Debug.Log(CurrentPlayerIS - 1);
		pinImg.sprite = allPinImg [CurrentPlayerIS ];
	}

	public void CloaseAll ()
	{
		Current_snake.enabled = false;
		
	}

	public void ReadyTOgo ()
	{
		Invoke ("Snake17", 0);
	}

	void Snake17 ()
	{
		Player_Active ();
		Playarea.instt.PassTurn ();
		Current_snake.enabled = true;
		this.gameObject.SetActive (false);
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