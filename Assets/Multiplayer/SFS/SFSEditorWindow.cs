#if UNITY_EDITOR
using UnityEngine;
using System.Diagnostics; 
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary; 

public class SFSEditorWindow : EditorWindow
{  
    [System.Serializable]
    public class Configurations
    { 
        public List<SFSConfig> list = new List<SFSConfig>(); 
    } 

    public static Configurations cf = new Configurations();

    static string configFileLoc;
    static string scriptLoc;
    static string editorScriptLoc;
    void SetFileLocations()
    { 
        if (string.IsNullOrEmpty(configFileLoc))
        { 
            StringBuilder fileLocation = new StringBuilder(GetScriptLocation(this));
            //              Debug.Log (fileLocation);  

            for (int i = 0; i < fileLocation.Length; i++)
            {
                if (fileLocation.ToString()[0].Equals('/'))
                    break;
                else
                    fileLocation.Remove(0, 1);
            }
            //              Debug.Log (fileLocation); 

            for (int i = fileLocation.Length - 1; i >= 0; i--)
            {
                if (fileLocation.ToString()[i].Equals('/'))
                    break;
                else
                    fileLocation.Remove(i, 1);
            }
            //              Debug.Log (fileLocation); 

            //              Debug.Log (Application.dataPath + fileLocation); 
            configFileLoc = Application.dataPath + fileLocation + "serverConfig.json"; 
            scriptLoc = Application.dataPath + fileLocation + "SFS.cs"; 
            editorScriptLoc = Application.dataPath + fileLocation + "SFSEditorWindow.cs"; 
        } 
    }
    Vector2 scrollPos = Vector2.zero;

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(configFileLoc, FileMode.OpenOrCreate);

        bf.Serialize(file, cf);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(configFileLoc))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(configFileLoc, FileMode.Open);
            try
            {
                cf = (Configurations)bf.Deserialize(file); 
                file.Close();
            }
            catch (System.Exception)
            {
                file.Close();
                SaveDefault(); 
            }
        }
        else SaveDefault();
    }

    static void SaveDefault()
    {
        cf = new Configurations(); 
        cf.list.Add(new SFSConfig("Beta", "beta.mobzway.com", 9933, "beta.mobzway.com"));
        cf.list.Add(new SFSConfig("LocalDot2", "192.168.0.2", 9938, "192.168.0.43"));
        cf.list.Add(new SFSConfig("LocalDot8", "192.168.0.8", 9938, "192.168.0.43"));
        Save();
    } 

	[MenuItem("Window/SFS")]
	static void Init()
	{  
		SFSEditorWindow window = (SFSEditorWindow)EditorWindow.GetWindow(typeof(SFSEditorWindow));
		window.Show(); 
	}

    void OnEnable()
    {
        titleContent = new GUIContent("SFS");
        SetFileLocations();
        Load();
    }

    // This will only get called 10 times per second.
    public void OnInspectorUpdate()
    {
        Repaint();
    }

	void OnGUI()
    {   
        GUILayout.BeginHorizontal();
        GUILayout.Label("Configure", EditorStyles.whiteBoldLabel);
        if (GUILayout.Button("Edit Script", GUILayout.Width(80)))
        {
            UnityEngine.Debug.Log(scriptLoc);

            //            ProcessStartInfo pi = new ProcessStartInfo(scriptLoc);
            //            pi.Arguments = Path.GetFileName(scriptLoc);
            //            pi.UseShellExecute = true;
            //            pi.WorkingDirectory = Path.GetDirectoryName(scriptLoc);
            //            pi.FileName = "C:/Program Files/Unity/MonoDevelop/bin/MonoDevelop.exe";
            //            pi.Verb = "OPEN";
            //            Process.Start(pi);

            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(scriptLoc, 12);
        }
        if (GUILayout.Button("Edit Window", GUILayout.Width(80)))
        { 
            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(editorScriptLoc, 11);
        }
        GUILayout.EndHorizontal();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		//Player Info
		GUILayout.Label("Player Info", EditorStyles.boldLabel);
		StringBuilder sb = new StringBuilder(); 
//		sb.Append ("Player Info::");

		sb.Append ("Connected: ");
		sb.Append (SFS.connected); 

		sb.Append ("\nLogged In: ");
		sb.Append (SFS.loggedIn); 

		sb.Append ("\nUsername: ");
        sb.Append (SFS.loggedInUsername);

        if (SFS.user != null)
        {
            sb.Append (": ");
            sb.Append (SFS.user.Id);
            if (SFS.joinedRooms.Count>0)
            {
                sb.Append (" : ");
                sb.Append (SFS.user.PlayerId);
            }
        }

		sb.Append ("\nZone: ");
		sb.Append (SFS.currentZone);

		sb.Append ("\nRooms: ");
        if (SFS.joinedRooms!=null)
        {
            for (int i = 0; i < SFS.joinedRooms.Count; i++)
            {
                sb.Append("\n");
                sb.Append(SFS.joinedRooms[i].Name);
                sb.Append (": ");
                sb.Append (SFS.user.GetPlayerId(SFS.joinedRooms[i]));
            }
        }
		EditorGUILayout.HelpBox(sb.ToString(), MessageType.None, true);


		//Server Info
		GUILayout.Label("Server Info", EditorStyles.boldLabel);
		sb = new StringBuilder(); 
//		sb.Append ("Server Info::"); 

		sb.Append ("Server Address: "); 
		sb.Append (SFS.configuration.serverAddress); 

		sb.Append ("\nPort: "); 
		sb.Append (SFS.configuration.serverPort); 

		sb.Append ("\nDatabase Address: "); 
		sb.Append (SFS.configuration.databaseAddress);  
		EditorGUILayout.HelpBox(sb.ToString(), MessageType.None, true);

        if (GUILayout.Button("Disconnect", GUILayout.Width(80)))
        { 
            SFS.Disconnect(null);
        }

        //Server Configs
        GUILayout.Space(10);  
        GUILayout.Label("Server Configurations: ", EditorStyles.boldLabel); 

        for (int i = 0; i < cf.list.Count; i++)
        {
            GUILayout.BeginHorizontal();
            cf.list[i].name = GUILayout.TextField(cf.list[i].name);

            if (cf.list[i].serverAddress.Equals(SFS.configuration.serverAddress) 
                && cf.list[i].serverPort.Equals(SFS.configuration.serverPort) 
                && cf.list[i].databaseAddress.Equals(SFS.configuration.databaseAddress)) 
            {
                GUI.enabled = false;
                GUILayout.Button("USING", GUILayout.Width(70));
                GUI.enabled = true;
            }
            else
            {
                if (GUILayout.Button("USE", GUILayout.Width(70)))
                {   
                    ModifyText(scriptLoc, "/*TAG_NAME*/", '=', ',', (" \"" + cf.list[i].name + "\""));
                    ModifyText(scriptLoc, "/*TAG_HOST*/", '=', ',', (" \"" + cf.list[i].serverAddress + "\""));
                    ModifyText(scriptLoc, "/*TAG_PORT*/", '=', ',', " "+ cf.list[i].serverPort.ToString());
                    ModifyText(scriptLoc, "/*TAG_DB*/", '=', ',', (" \"" + cf.list[i].databaseAddress + "\"")); 
                    ForceCompileScripts();
                }
            }

            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                cf.list.RemoveAt(i); 
                Save(); 
                return;
            }
            GUILayout.EndHorizontal();

            cf.list[i].serverAddress = EditorGUILayout.TextField("Server Address: ", cf.list[i].serverAddress);
            cf.list[i].serverPort = EditorGUILayout.IntField("Port: ", cf.list[i].serverPort); 
            cf.list[i].databaseAddress = EditorGUILayout.TextField("Database Address: ", cf.list[i].databaseAddress); 
            GUILayout.Space(10);
        } 

        if (GUILayout.Button("Add Configuration"))
        {
            cf.list.Add(new SFSConfig("Config "+ cf.list.Count.ToString(), "", 0, ""));
            Save();
        }
        EditorGUILayout.EndScrollView();

        if (GUI.changed)
        {
            Save();
        }
	}


	public static void ModifyText(string path, string tag, char startChar, char endChar, string stringToInsert)
	{
		StringBuilder newScript = new StringBuilder();

		using (StreamReader streamReader = new StreamReader(path))
		{
			string line;
			while ((line = streamReader.ReadLine()) != null)
			{
				if (line.Contains(tag))
				{ 
					//Debug.Log(line);
					int si = line.IndexOf(startChar), ei = line.IndexOf(endChar);
					line = line.Remove(si + 1, ei - si - 1);
					line = line.Insert(si + 1, stringToInsert);
					//Debug.Log(line);
				}
				newScript.AppendLine(line);
				//Debug.Log(line); 
			}
			streamReader.Close();
		}
		//        Debug.Log(newScript);
        using (FileStream fileStream = new FileStream(path+"Temp", FileMode.OpenOrCreate, FileAccess.ReadWrite))
		{
			StreamWriter streamWriter = new StreamWriter(fileStream);
			streamWriter.Write("");
			streamWriter.Write(newScript);
			streamWriter.Close();
			fileStream.Close();
		} 

        File.Copy(path+"Temp", path,true);
        File.Delete(path + "Temp");
	} 

	public static string GetScriptLocation(ScriptableObject script)
	{
		return AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(script));
	}

	public static void ForceCompileScripts() {
		//AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceSynchronousImport);
		MonoScript cMonoScript = MonoImporter.GetAllRuntimeMonoScripts()[0];
		MonoImporter.SetExecutionOrder(cMonoScript, MonoImporter.GetExecutionOrder(cMonoScript));
        AssetDatabase.Refresh();
	}
}

#endif
