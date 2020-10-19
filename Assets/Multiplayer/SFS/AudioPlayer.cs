using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
	[System.Serializable]
	public class Audio
	{
		public string key;
		public AudioClip clip;
	}

	public static AudioPlayer _instance;

	public static AudioPlayer instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<AudioPlayer> ();
				if (_instance != null)
					_instance.Initialise ();
			}
			return _instance;
		}
		set {
			_instance = value;
		}
	}

	public List<Audio> audios;
	public static Dictionary<string, Audio> Audios;

	public static event System.Action<bool> MusicSet;

	[Space]
	public AudioSource fxSource;
	public AudioSource musicSource;

	[Space]
	public bool playerReady = false;
	public bool effectsOn = true, musicOn = true;

	void OnEnable ()
	{

		if (PlayerPrefs.HasKey ("musicOff")) {
			PlayerPrefs.DeleteKey ("musicOff");
		}
		instance = this;
		Audios = new Dictionary<string, Audio> ();
		for (int i = 0; i < audios.Count; i++)
			if (!Audios.ContainsKey (audios [i].key))
				Audios.Add (audios [i].key, audios [i]);  
		//print("New AP: " + transform.parent.parent.parent.name); 
		Initialise ();
	}

	public  void Initialise ()
	{
		if (PlayerPrefs.HasKey ("soundEffectOff"))
			effectsOn = false;
		else
			effectsOn = true;
		SetFx (effectsOn);
		if (PlayerPrefs.HasKey ("musicOff"))
			musicOn = false;
		else
			musicOn = true;

		print ("musicOn :" + musicOn);
		SetMusic (musicOn);
		playerReady = true;


		//print ("GameStatics.SoundOn : " + GameStatics.SoundOn);
		//if (GameStatics.SoundOn == 1) {
		//	effectsOn = true;
		//	SetFx (effectsOn);
		//	musicOn = true;

		//	print (" ------- ");
		//	SetMusic (musicOn);
		//} else {
		//	effectsOn = false;
		//	SetFx (effectsOn);
		//	musicOn = false;

		//	print (" ------- ");
		//	SetMusic (musicOn);
		//}




//		musicSource.gameObject.SetActive(false);
	}

	void OnDisable ()
	{ 
		//print("AP Destroyed: " + transform.name);
	}

	public static void PlaySFX (string key, float delay = 0)
	{
		if (string.IsNullOrEmpty (key) || !Audios.ContainsKey (key))
			return;
		instance.StartCoroutine (instance.play_c (Audios [key].clip, delay));
	}

	public static void PlaySFX (AudioClip clip, float delay = 0)
	{ 
		instance.StartCoroutine (instance.play_c (clip, delay));
	}

	IEnumerator play_c (AudioClip clip, float delay)
	{
		if (!instance.playerReady)
			yield break;
		if (!instance.effectsOn)
			yield break;
		if (delay > 0)
			yield return new WaitForSeconds (delay);
		fxSource.PlayOneShot (clip);
#if UNITY_EDITOR
		Visualize (clip.name);
#endif
		//Debug.Log(key+" "+useUnityPlayer.ToString(), gameObject);
	}


	public static void PlayPlayerTurnSFX (string key, float delay = 0)
	{
		if (string.IsNullOrEmpty (key) || !Audios.ContainsKey (key))
			return;
		instance.StartCoroutine (instance.play_cPlayerTurn (Audios [key].clip, delay));
	}

	public static void PlayPlayerTurnSFX (AudioClip clip, float delay = 0)
	{ 
		instance.StartCoroutine (instance.play_cPlayerTurn (clip, delay));
	}

	IEnumerator play_cPlayerTurn (AudioClip clip, float delay)
	{
		if (delay > 0)
			yield return new WaitForSeconds (delay);
		fxSource.PlayOneShot (clip);
		#if UNITY_EDITOR
		Visualize (clip.name);
		#endif
		//Debug.Log(key+" "+useUnityPlayer.ToString(), gameObject);
	}

	public static void SwitchFx ()
	{
		instance.SwitchFxIns ();
	}


	//----------------------------------------------

	public bool voiceEffectsOn = true;

	public static void SetVoiceFx (bool on)
	{
		instance.voiceEffectsOn = on;
	}

	public static void PlayVoiceSFX (string key, float delay = 0)
	{
		if (string.IsNullOrEmpty (key) || !Audios.ContainsKey (key))
			return;
		instance.StartCoroutine (instance.play_cVoice (Audios [key].clip, delay));
	}

	public static void PlayVoiceSFX (AudioClip clip, float delay = 0)
	{ 
		instance.StartCoroutine (instance.play_cVoice (clip, delay));
	}

	IEnumerator play_cVoice (AudioClip clip, float delay)
	{
		if (!instance.playerReady)
			yield break;
		if (!instance.voiceEffectsOn)
			yield break;
		if (delay > 0)
			yield return new WaitForSeconds (delay);

		fxSource.volume = 1;
		fxSource.PlayOneShot (clip);
		#if UNITY_EDITOR
		Visualize (clip.name);
		#endif
		//Debug.Log(key+" "+useUnityPlayer.ToString(), gameObject);
	}
	//----------------------------------------------
	public static void SetFx (bool on)
	{
		instance.SetFxIns (on);
	}

	public void SwitchFxIns ()
	{
		instance.SetFxIns (!effectsOn);
	}

	public void SetFxIns (bool on)
	{
        Debug.Log("SetFxIns : " + on);
		if (!on) {
			fxSource.volume = 0;
			effectsOn = false;
			PlayerPrefs.SetInt ("soundEffectOff", 1);
		} else {
			fxSource.volume = 1;
			effectsOn = true;
			PlayerPrefs.DeleteKey ("soundEffectOff");
		}
		float o = on ? 1 : .25f;
	}


	public static void PlayMusic ()
	{
		instance.PlayMusicIns ();
	}

	public void PlayMusicIns ()
	{
		Debug.Log ("musicOn : " + musicOn);
		Debug.Log ("musicSource.gameObject.activeSelf : " + musicSource.gameObject.activeSelf);
		musicSource.gameObject.SetActive (false);
		if (!musicOn) {
			Debug.Log ("Returning");
			return;	
		}
		if (musicSource.gameObject.activeSelf) {
			Debug.Log ("Returning");
			return;	
		}
		musicSource.gameObject.SetActive (true);
		musicSource.Play ();
	}

	public static void StopMusic ()
	{
		instance.StopMusicIns ();
	}

	public void StopMusicIns ()
	{
		if (!musicOn) {
			Debug.Log ("Returning");
			return;	
		}
		musicSource.gameObject.SetActive (false);
	}

	public static void StopMusicFx ()
	{
		instance.StopMusicInsfx ();
	}

	public void StopMusicInsfx ()
	{
		if (!effectsOn) {
			Debug.Log ("Returning");
			return;	
		}
		fxSource.gameObject.SetActive (false);
	}

	public static void SwitchMusic ()
	{
		instance.SwitchMusicIns ();
	}

	public static void SetMusic (bool on)
	{
        Debug.Log("ON ----" + on);
        print (" ------- ");
		instance.SetMusicIns (on);
	}

	public void SwitchMusicIns ()
	{

		print (" ------- ");
		SetMusicIns (!musicOn);
	}

	public void SetMusicIns (bool on)
	{
		Debug.Log ("ON ----" + on);
		if (!on) {
			musicSource.volume = 0;
			musicOn = false;
			PlayerPrefs.SetInt ("musicOff", 1);
			Debug.Log ("musicSource.gameObject.activeSelf : " + musicSource.gameObject.activeSelf);

		} else {
			musicSource.volume = 1;
			musicOn = true;
			PlayerPrefs.DeleteKey ("musicOff");
			Debug.Log ("keys deleted");
		}
		Debug.Log ("musicSource.gameObject.activeSelf : " + musicSource.gameObject.activeSelf);
		musicSource.gameObject.SetActive (on);
		Debug.Log ("musicSource.gameObject.activeSelf : " + musicSource.gameObject.activeSelf);
		if (MusicSet != null) {
			Debug.Log ("--------");
			MusicSet (musicOn);
		}
//        float o = on ? 1 : .25f;
	}

	public Text visualizeTxtComp;

	void Visualize (string msg)
	{
		if (visualizeTxtComp != null) {
			visualizeTxtComp.transform.parent.gameObject.SetActive (false);
			visualizeTxtComp.transform.parent.gameObject.SetActive (true);
			visualizeTxtComp.text = msg;
		}
	}


	// Added By Vinay
	public static void StopSfx (string currClip)
	{
		instance.StartCoroutine (instance.StopSfx_C (currClip));
	}

	public IEnumerator StopSfx_C (string clipKey)
	{
		fxSource.clip = Audios [clipKey].clip;
		fxSource.Stop ();
		yield return null;
	}
}
