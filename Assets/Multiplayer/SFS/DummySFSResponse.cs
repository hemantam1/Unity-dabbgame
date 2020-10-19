using UnityEngine;
using System.Collections;
using Sfs2X.Entities.Data;
using Sfs2X.Entities;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DummySFSResponse : MonoBehaviour
{

    [System.Serializable]
    public class BoolValue { public string key; public bool val; }

    [System.Serializable]
    public class IntValue { public string key; public int val; }

    [System.Serializable]
    public class DoubleValue { public string key; public double val; }

    [System.Serializable]
    public class StringValue { public string key; public string val; }

	[System.Serializable]
	public class BoolArray { public string key; public bool[] val; }

    [System.Serializable]
    public class IntArray { public string key; public int[] val; }

    [System.Serializable]
    public class DoubleArray { public string key; public double[] val; }

    [System.Serializable]
    public class StringArray { public string key; public string[] val; } 

    public string key;
    public BoolValue[] bools = new BoolValue[] { };
    public IntValue[] ints = new IntValue[] { };
    public DoubleValue[] doubles = new DoubleValue[] { };
    public StringValue[] strings = new StringValue[] { };
	public BoolArray[] boolArrays = new BoolArray[] { };
    public IntArray[] intArrays = new IntArray[] { };
    public DoubleArray[] doubleArrays = new DoubleArray[] { };
    public StringArray[] stringArrays = new StringArray[] { };
    public Room room;

    SFSObject _obj;
    public SFSObject obj {
        get {
            _obj = new SFSObject();
            for (int i = 0; i < bools.Length; i++) _obj.PutBool(bools[i].key, bools[i].val);
            for (int i = 0; i < ints.Length; i++) _obj.PutInt(ints[i].key, ints[i].val);
            for (int i = 0; i < doubles.Length; i++) _obj.PutDouble(doubles[i].key, doubles[i].val);
            for (int i = 0; i < strings.Length; i++) _obj.PutUtfString(strings[i].key, strings[i].val);
			for (int i = 0; i < boolArrays.Length; i++) _obj.PutBoolArray(boolArrays[i].key, boolArrays[i].val);
            for (int i = 0; i < intArrays.Length; i++) _obj.PutIntArray(intArrays[i].key, intArrays[i].val);
            for (int i = 0; i < doubleArrays.Length; i++) _obj.PutDoubleArray(doubleArrays[i].key, doubleArrays[i].val);
            for (int i = 0; i < stringArrays.Length; i++) _obj.PutUtfStringArray(stringArrays[i].key, stringArrays[i].val);
            return _obj;
        }
    }

    public void Fire() { 
        SFS.FireExtensionResponseReceived(key, obj, room);
    }

    public float repeatInterval = 3, repeatCount = 2;
    IEnumerator RepeatFire()
    {
        int c = 0;
        while (c < repeatCount)
        {
            c++;
            try { Fire(); }
            catch { }
            yield return new WaitForSeconds(repeatInterval);
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(DummySFSResponse))]
public class DummySFSResponseEditor : Editor
{
    DummySFSResponse script;
    int selectedRoom = 0;
    string[] roomNames = new string[0];

    void OnEnable()
    {
        script = target as DummySFSResponse;
        UpdateVars (); 
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("Room:");
        selectedRoom = GUILayout.SelectionGrid(selectedRoom, roomNames, 1);  
        if(script.room!=null) GUILayout.Label(script.room.Name);
        GUILayout.Space(10);
        if (GUILayout.Button("Invoke"))
        { 
            if (Application.isPlaying)
            {
                script.Fire ();
            }
        }  
        if (GUILayout.Button("Repeat Invoke"))
        { 
            if (Application.isPlaying)
            {
                script.StopCoroutine ("RepeatFire");
                script.StartCoroutine ("RepeatFire");
            }
        }  

        if (GUI.changed) { UpdateVars (); }
    }

    void UpdateVars(){
        script.gameObject.name = string.Format ("{0}", script.key); 

        roomNames = new string[SFS.joinedRooms.Count + 1];
        for (int i = 0; i < SFS.joinedRooms.Count; i++)
        {
            roomNames[i] = SFS.joinedRooms[i].Name;
        }
        roomNames[roomNames.Length - 1] = "Null";

        try { script.room = SFS.joinedRooms[selectedRoom]; } 
        catch { script.room = null; }  
    } 
}
#endif