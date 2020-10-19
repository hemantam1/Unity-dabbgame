using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DisconnectionHandler : MonoBehaviour {
    
	void OnEnable ()
    {
        SFS.ConnectionToServerLost += OnConnectionLost;
        StartCoroutine(DelayedOnEnable());
    }

    IEnumerator DelayedOnEnable()
    {
        yield return null; 
        Check();
    }
	
	void OnDisable ()
    {
        SFS.ConnectionToServerLost -= OnConnectionLost;
    }

    void OnApplicationPause(bool paused) {
        if (!paused) Check(); 
    }

    bool Check()
    {
        if (!SFS.connected) OnConnectionLost("UNKNOWN"); 
        return SFS.connected;
    }

    void OnConnectionLost(string error)
    {
        StartCoroutine(OnConnectionLost_c(error));
    }
    IEnumerator OnConnectionLost_c(string error)
    {
        //string cause = "Looks like your device has lost the internet connectivity. \nReconnect once you're connected to internet again :)";

        //WWW ping = new WWW("www.google.com");
        //yield return ping;
        //if (string.IsNullOrEmpty(ping.error)) cause = "Our servers are probably down for the moment. \nPlease check back again :)";
        string cause = error;
        yield break;
    }
}
