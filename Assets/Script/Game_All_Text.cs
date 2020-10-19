using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game_All_Text : MonoBehaviour
{

	public static Game_All_Text instance;

	public Toggle TGEnglish, TGArabic;

	public GameObject languageScreen;

	[Header ("Offile Player Text")]
	public InputField[] playerNameInput = new InputField[4];


	[Header ("Chat Panel")]
	public InputField chatPrefabInput;



	void Awake ()
	{
		instance = this;

		int languagenumber = 0;

		if (TGEnglish.isOn) {
			languagenumber = 0;
			SetGameLanguage_English ();
		} else {
			languagenumber = 1;
			SetGameLanguage_Arabic ();
		}

		PlayerPrefs.SetInt ("LanguageNumber", languagenumber);
	}

	// Use this for initialization
	void Start ()
	{
		if (Application.systemLanguage == SystemLanguage.Arabic) {
			SetGameLanguage_Arabic ();
		} else {
			SetGameLanguage_English ();
		}
	}


	public void SetGameLanguage_Arabic ()
	{
		PlayerPrefs.SetInt ("LanguageNumber", 1);
		for (int i = 0; i < playerNameInput.Length; i++) {
			playerNameInput [i].placeholder.GetComponent<Text> ().alignment = TextAnchor.MiddleRight;
			playerNameInput [i].textComponent.alignment = TextAnchor.MiddleRight;
		}
		chatPrefabInput.placeholder.GetComponent<Text> ().alignment = TextAnchor.MiddleRight;
		chatPrefabInput.textComponent.alignment = TextAnchor.MiddleRight;

	}

	public void SetGameLanguage_English ()
	{
		PlayerPrefs.SetInt ("LanguageNumber", 0);
		for (int i = 0; i < playerNameInput.Length; i++) {
			playerNameInput [i].placeholder.GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;
			playerNameInput [i].textComponent.alignment = TextAnchor.MiddleLeft;
		}
		chatPrefabInput.placeholder.GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;
		chatPrefabInput.textComponent.alignment = TextAnchor.MiddleLeft;
	}

	public void LanguageScreenOn ()
	{
		languageScreen.SetActive (true);
	}

	public void LanguageScreenOff ()
	{
		languageScreen.SetActive (false);
	}
}
