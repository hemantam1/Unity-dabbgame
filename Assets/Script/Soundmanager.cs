using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveConstantVal;

public class Soundmanager : MonoBehaviour
{

	public static Soundmanager instance;

	public AudioSource Safezone;
	public AudioSource snakeBit;
	public AudioSource diceRoll;
	public AudioClip[] BGsound;
	public AudioSource bgMusic;
	public AudioSource buttonClick;
	public AudioSource closeBTClick;
	public AudioSource jump;
	public AudioSource LevelCompS;
	public AudioSource PlayerWinS;
	public AudioSource KillOpponent;

	public AudioClip Clip;


	void Awake ()
	{
		
		instance = this;
	}

	void Start ()
	{
		
		Play_Bg_music ();
	}



	public void Play_Jump ()
	{
		jump.Play ();
	}

	public void Play_Bg_music ()
	{
		switch (PlayerPrefs.GetInt (ApiConstant.CurrentEnvironment)) {
		case 2: // rain
			bgMusic.clip = BGsound [0];
			break;
		case 4:// dasert
			bgMusic.clip = BGsound [1];
			break;
		default:
			bgMusic.clip = BGsound [2];
			break;
		}
		bgMusic.Play ();

	}

	public void Kill_Opponent ()
	{
		KillOpponent.Play ();
	}

	public void Bg_Music_Play ()
	{
		bgMusic.clip = Clip;
		bgMusic.volume = 0.8f;
		bgMusic.Play ();
	}

	public void Play_ButtonClick ()
	{
		buttonClick.Play ();
	}

	public void Play_closeClick ()
	{
		closeBTClick.Play ();
	}

	public void Play_DiceRollSound ()
	{
		diceRoll.Play ();
	}

	public void Play_SnakeBitsound ()
	{
		snakeBit.Play ();
	}

	public void Play_SafeZone ()
	{
		Safezone.Play ();
	}

	public void Play_LevelComplete ()
	{
		LevelCompS.Play ();
	}

	public void Play_playerReach100 ()
	{
		PlayerWinS.Play ();
	}
}
