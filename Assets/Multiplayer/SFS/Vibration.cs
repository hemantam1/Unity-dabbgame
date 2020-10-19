using UnityEngine;
using System.Collections;

public class Vibration
{
    static bool initialized;
    static bool _enabled;
    public static bool enabled
    {
        get{ 
            if (!initialized)
            {
                if (PlayerPrefs.HasKey("Vibrr")) _enabled = PlayerPrefs.GetInt("Vibrr") == 1;
                else _enabled = true;
                initialized = true;
            }
            return _enabled;
        }   
        set{ 
            _enabled = value;
            PlayerPrefs.SetInt("Vibrr", _enabled?1:0);
        }
    } 

    public static void Toggle()
    {
        Set(!enabled);
    }

    public static void Set(bool _enabled)
    {
        enabled = _enabled;
    }

    public static void Vibrate()
    {
        #if UNITY_ANDROID || UNITY_IOS
        if(enabled) Handheld.Vibrate();
        #endif
    }
}
