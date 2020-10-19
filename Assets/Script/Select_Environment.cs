using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveConstantVal;

public class Select_Environment : MonoBehaviour
{
	[HideInInspector]public GameObject[] Environment_effects;
	[HideInInspector]public Text[] all_Text;
	[HideInInspector]public Sprite[] allEnvironment;
	public Image mainBG;

	public Image playareaBG;
	public Sprite grassEnvironment;
	public Sprite iceEnvironment;


	void Start ()
	{
		switch (PlayerPrefs.GetInt (ApiConstant.CurrentEnvironment)) {

		case 1:
			Theam_1 ();
			break;
		case 2:
			Theam_2 ();
			break;
		case 3:
			Theam_3 ();
			break;
		case 4:
			Theam_4 ();
			break;
		}

	}

	void Apply_Effect (int val)
	{
		for (int i = 0; i < 4; i++) {
			Environment_effects [i].SetActive (false);
		}
		Environment_effects [val].SetActive (true);
	}

	void Update ()
	{
		
	}

	// star Theam

	public void Theam_1 ()
	{
		Apply_Effect (0);
		PlayerPrefs.SetInt (ApiConstant.CurrentEnvironment, 1);
		mainBG.sprite = allEnvironment [0];
		playareaBG.enabled = false;

		Normal_Text ();
		Soundmanager.instance.Play_Bg_music ();
	}

	// Grass theam
	public void Theam_2 ()
	{
		Apply_Effect (1);
		PlayerPrefs.SetInt (ApiConstant.CurrentEnvironment, 2);
		mainBG.sprite = allEnvironment [1];
		playareaBG.enabled = true;
		playareaBG.sprite = grassEnvironment;
		Normal_Text ();
		Soundmanager.instance.Play_Bg_music ();
	}

	// ice theam
	public void Theam_3 ()
	{
		
		Apply_Effect (2);
		PlayerPrefs.SetInt (ApiConstant.CurrentEnvironment, 3);
		mainBG.sprite = allEnvironment [2];
		playareaBG.sprite = iceEnvironment;
		Blue_Text ();
		Soundmanager.instance.Play_Bg_music ();
	}

	// desert theam
	public void Theam_4 ()
	{
		
		Apply_Effect (3);
		PlayerPrefs.SetInt (ApiConstant.CurrentEnvironment, 4);
		
		mainBG.sprite = allEnvironment [3];
		playareaBG.enabled = false;

		Normal_Text ();
		Soundmanager.instance.Play_Bg_music ();

	}

	void Normal_Text ()
	{
		
		Color myColor = new Color ();
		ColorUtility.TryParseHtmlString ("#FFFFFFFF", out myColor);


		for (int i = 0; i < all_Text.Length; i++) {
			all_Text [i].color = myColor;
		}
	}

	void Blue_Text ()
	{
		Color myColor = new Color ();
		ColorUtility.TryParseHtmlString ("#1C2185FF", out myColor);

		for (int i = 0; i < all_Text.Length; i++) {
			all_Text [i].color = myColor;
		}
	}
}
