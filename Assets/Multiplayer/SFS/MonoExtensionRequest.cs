using UnityEngine;
using System.Collections;
using Sfs2X.Entities.Data;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MonoExtensionRequest : MonoBehaviour {
	[System.Serializable]
	public class BoolValue { public string key; public bool val; }

	[System.Serializable]
	public class IntValue { public string key; public int val; }

	[System.Serializable]
	public class DoubleValue { public string key; public double val; }

	[System.Serializable]
	public class StringValue { public string key; public string val; }

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
	public IntArray[] intArrays = new IntArray[] { };
	public DoubleArray[] doubleArrays = new DoubleArray[] { };
	public StringArray[] stringArrays = new StringArray[] { };

	SFSObject _obj;
	public SFSObject obj {
		get {
			_obj = new SFSObject();
			for (int i = 0; i < bools.Length; i++) _obj.PutBool(bools[i].key, bools[i].val);
			for (int i = 0; i < ints.Length; i++) _obj.PutInt(ints[i].key, ints[i].val);
			for (int i = 0; i < doubles.Length; i++) _obj.PutDouble(doubles[i].key, doubles[i].val);
			for (int i = 0; i < strings.Length; i++) _obj.PutUtfString(strings[i].key, strings[i].val);
			for (int i = 0; i < intArrays.Length; i++) _obj.PutIntArray(intArrays[i].key, intArrays[i].val);
			for (int i = 0; i < doubleArrays.Length; i++) _obj.PutDoubleArray(doubleArrays[i].key, doubleArrays[i].val);
			for (int i = 0; i < stringArrays.Length; i++) _obj.PutUtfStringArray(stringArrays[i].key, stringArrays[i].val);
			return _obj;
		}
	}

	public void Fire() {
        SFS.CallExtension(key, obj, null, null);
//		Debug.LogFormat ("Ext Req Sent: {0}\n{1}",key,obj.GetDumpFull());
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(MonoExtensionRequest))]
public class NewExtensionRequestEditor : Editor
{
	MonoExtensionRequest script;
	void OnEnable()
	{
        script = target as MonoExtensionRequest;
        UpdateName ();
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI(); 
		if (GUILayout.Button("Send"))
		{ 
			if (Application.isPlaying)
			{
				script.Fire ();
			}
		} 
        if (GUI.changed) {
            UpdateName ();
        }
    }
    void UpdateName(){
        script.gameObject.name = string.Format ("Extension [{0}]", script.key);
    }
}
#endif