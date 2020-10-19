using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MonoSFSConnection : MonoBehaviour {
    public string username;
    public string zone; 
    public bool connected;

//    void OnEnable()
//    {
//        if (connectOnEnable) Connect ();
//    }

    public void Connect(){
        SFS.Connect(()=>{connected = true;}, null); 
    }
    public void Dosconnect(){
        SFS.Disconnect(null); 
    }

    public void Login()
    {
        SFS.Login(username, "", zone, ()=>{connected = true;}, null);
    }

    public void Logout()
    {
        print(" -- ");
        SFS.Logout(null, null);
    }  

    public void Switch()
    {
        SFS.SwitchZone(zone, null, null);
    }  

    void OnApplicationQuit()
    {
        if (connected)
        {
            print(" -- ");
            SFS.LeaveRoom (SFS.currentRoom, null, null);
            SFS.Logout (null, null);
            SFS.Disconnect (null);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MonoSFSConnection))]
public class NewSFSConnectionEditor : Editor
{
    MonoSFSConnection script;
    void OnEnable()
    {
        script = target as MonoSFSConnection;
        UpdateName ();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); 
        if (GUILayout.Button("Connect"))
        { 
            if (Application.isPlaying)
            {
                script.Connect ();
            }
        }  
        if (GUILayout.Button("Disconnect"))
        { 
            if (Application.isPlaying)
            {
                script.Dosconnect ();
            }
        }  
        if (GUILayout.Button("Login"))
        { 
            if (Application.isPlaying)
            {
                script.Login ();
            }
        }  
        if (GUILayout.Button("Logout"))
        { 
            if (Application.isPlaying)
            {
                script.Logout ();
            }
        } 
        if (GUILayout.Button("Switch To"))
        { 
            if (Application.isPlaying)
            {
                script.Switch ();
            }
        } 
        if (GUI.changed) {
            UpdateName ();
        }
    }

    void UpdateName(){
        script.gameObject.name = string.Format ("Connection: [{0}-{1}]", script.username, script.zone);
    }

    //469 471
}
#endif