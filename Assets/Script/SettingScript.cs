using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveConstantVal;

public class SettingScript : MonoBehaviour
{
	public GameObject soundImg;
	public GameObject vibrationImg;

	void Start ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.sound_check) == 0) {
			soundImg.SetActive (false);
			AudioListener.volume = 1;
		} else {
			soundImg.SetActive (true);
			AudioListener.volume = 0;
		}

		if (PlayerPrefs.GetInt (ApiConstant.vibration_check) == 0) {
			vibrationImg.SetActive (false);
		} else {
			vibrationImg.SetActive (true);
		}
	}

	void Update ()
	{
		
	}

	public void Sound_Setting ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.sound_check) == 0) {
			PlayerPrefs.SetInt (ApiConstant.sound_check, 1);
			soundImg.SetActive (true);
			AudioListener.volume = 0;
		} else {
			PlayerPrefs.SetInt (ApiConstant.sound_check, 0);
			soundImg.SetActive (false);
			AudioListener.volume = 1;
		}
	}

	public void Vibration_Setting ()
	{
		if (PlayerPrefs.GetInt (ApiConstant.vibration_check) == 0) {
			PlayerPrefs.SetInt (ApiConstant.vibration_check, 1);
			vibrationImg.SetActive (true);

		} else {
			PlayerPrefs.SetInt (ApiConstant.vibration_check, 0);
			vibrationImg.SetActive (false);
		}
	}
}
