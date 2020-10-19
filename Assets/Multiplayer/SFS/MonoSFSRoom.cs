using UnityEngine;
using System.Collections;
using Sfs2X.Entities;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MonoSFSRoom : MonoBehaviour {
    public string roomName;
    public Room room;
    public void JoinRoom()
    {
        SFS.JoinRoom(roomName, (r)=>{room = r;}, null);   
    } 
    public void LeaveRoom()
    {
        SFS.LeaveRoom(room, null, null);   
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MonoSFSRoom))]
public class MonoSFSRoomEditor : Editor
{
    MonoSFSRoom script;
    void OnEnable()
    {
        script = target as MonoSFSRoom;
        UpdateName ();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); 
        if (GUILayout.Button("Join Room"))
        { 
            if (Application.isPlaying)
            {
                script.JoinRoom ();
            }
        } 
        if (GUILayout.Button("Leave Room"))
        { 
            if (Application.isPlaying)
            {
                script.LeaveRoom ();
            }
        } 
        if (GUI.changed) {
            UpdateName ();
        }
    }

    void UpdateName(){
        script.gameObject.name = string.Format ("Room [{0}]", script.roomName);
    }

    //469 471
}
#endif