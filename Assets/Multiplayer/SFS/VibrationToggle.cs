using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VibrationToggle : MonoBehaviour 
{
    Toggle toggle; 

    void OnEnable()
    {
        if (toggle==null)
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggle);
        }
        toggle.isOn = Vibration.enabled; 
    }

    void OnToggle(bool b)
    { 
        Vibration.Set(b);
    }
}
