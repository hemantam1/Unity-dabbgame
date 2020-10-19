using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    Toggle toggle;
    public bool toggleSFX;
    public bool toggleMusic;

    void OnEnable()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
            //toggle.onValueChanged.AddListener(OnToggle);
            toggle.onValueChanged.AddListener((value) =>
            {
                OnToggle(value);
            });
            Debug.Log("Listner Added");
        }
        if (toggleSFX)
        {
            toggle.isOn = AudioPlayer.instance.effectsOn;
        }
        if (toggleMusic)
        {
            toggle.isOn = AudioPlayer.instance.musicOn;
        }
    }



    void OnToggle(bool b)
    {
        Debug.Log("toggle.isOn " + toggle.isOn);
        Debug.Log("b " + b);
        if (toggleSFX)
        {
            AudioPlayer.SetFx(b);
        }
        if (toggleMusic)
        {
            AudioPlayer.SetMusic(b);
        }
    }
}
