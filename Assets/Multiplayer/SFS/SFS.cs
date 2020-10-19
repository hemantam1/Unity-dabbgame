using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SFS : MonoBehaviour
{
    #region Public Vars & Events
    static SmartFox _sfs; 
	public static string serverURL;
    public static SmartFox sfs
    {
        get
        { 
            if (_sfs == null)
            {
                _sfs = new SmartFox();
            }
            return _sfs;
        } 
    }
    public static SFSConfig configuration = new SFSConfig() 
    {
        name = "Config 6",  /*TAG_NAME*/
        serverAddress = "localhost",  /*TAG_HOST*/
        serverPort = 9933,  /*TAG_PORT*/
        databaseAddress = "",  /*TAG_DB*/
    };

    public static bool OfflineMode = false;

    public static bool connected{ get { return connectedToServer; } } //this readable property is available to other classes 
	public static string databaseAddress{ get {return configuration.databaseAddress;} }
 
    public static bool loggedIn{ get { return connectedToServer; } } //this readable property is available to other classes
    public static User user
    {
        get{ 
            if (sfs == null)
                return null;
            else if (sfs.UserManager.UserCount == 0)
                return null;
            else 
                return sfs.UserManager.GetUserList()[0];
        }
    }
    public static string loggedInUsername
    {
        get
        { 
            if (user == null) return ""; 
            return user.Name;
        }
    }

    public static string currentZone
    {
        get{ 
            if (sfs != null && !string.IsNullOrEmpty(sfs.CurrentZone))
            {
                return sfs.CurrentZone;
            }
            return string.Empty;
        }
    }

    public static List<Room> joinedRooms
    {
        get
        { 
            if (sfs == null)
                return null;
            else
                return sfs.JoinedRooms;
        }
    }

    public static Room lastJoinedRoom
    {
        get{ 
            if (sfs == null)
                return null;
            else 
                return sfs.LastJoinedRoom;
        }
    }

    public static Room currentRoom
    {
        get
        { 
            if (joinedRooms == null)
                return null;
            else if (joinedRooms.Count > 0)
                return joinedRooms[joinedRooms.Count - 1];
            else
                return null; 
        }    
    }

    //An event ehich fires when we get an extension response from server definded at backend
    public static event System.Action<string, SFSObject, Room> ExtensionResponseReceived;
    public static event System.Action BulkResponsesReceived;
    public static event System.Action<Room> RoomPlayersUpdated;
    public static event System.Action<string, string, Room> PublicMessageReceived;
    public static event System.Action<string> ConnectionToServerLost;
    public static event System.Action<User, List<string>> UserVariableUpdated;

    static string lastUserName = "", lastZoneName = "", lastRoomName = "";  
    #endregion

    void OnEnable()
    {
        AssignListeners(); 
//		GameRoom_Screen.instance.gameTablePrefab.SetActive (false);
    }

    void OnDisable()
    {
        print(" --------------- ");
        SFSObj = null;
        DeassignListeners(); 
    }

    void Update()
    { 
        sfs.ProcessEvents(); 
        //#if UNITY_EDITOR
        //if (connected && EditorApplication.isCompiling) 
        //{
        //    print(" ------------ ");
        //    Logout(null,null);
        //    Disconnect(null);  
        //}
        //#endif
    }

    bool listenersAssigned = false;

    void AssignListeners()
    {
        if (!listenersAssigned)
        {
            sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
            sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
            sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
            sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            sfs.AddEventListener(SFSEvent.LOGOUT, OnLogout);
            sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
            sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError); 
            sfs.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnRoomEnter);
            sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnRoomExit);
			sfs.AddEventListener(SFSEvent.SPECTATOR_TO_PLAYER, SpectatorToPlayerSuccess); 
			sfs.AddEventListener (SFSEvent.SPECTATOR_TO_PLAYER_ERROR, SpectatorToPlayerError);
            sfs.AddEventListener(SFSEvent.PLAYER_TO_SPECTATOR, OnRoomPlayerUpdate); 
            sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
			sfs.AddEventListener(SFSEvent.PUBLIC_MESSAGE, OnNewPublicMessageReceived);
            sfs.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVariableUpdate);
            listenersAssigned = true;
        }
    }

    void DeassignListeners()
    {
        if (listenersAssigned)
        {
            sfs.RemoveAllEventListeners();
            listenersAssigned = false;
        }
    }

    static SFS SFSObj = null;
    static void VerifyInstance()
    {
        if (SFSObj == null)
        {
            GameObject obj = GameObject.Find("SFS");
            if (obj != null)
            {
                SFSObj = GameObject.FindObjectOfType<SFS>();
                print(" ----------- ");

            }
            
        }

        if (SFSObj == null)
        {
			Debug.Log ("SFS Object not found, creating new one.");
//            Logs.Add.Info("SFS Object not found, creating new one.");
            SFSObj = new GameObject("SFS").AddComponent<SFS>(); 
            DontDestroyOnLoad(SFSObj.gameObject);
        }
        else
        {
            print(" ----------- ");
        }
    }

    #region Connection
    public static bool connectedToServer; //don't make this public, it is not supposed to be modified from other classes
    static System.Action connectionSuccessAction;
    static System.Action<string> connectionFailureAction; 
    static System.Action disconnectSuccessAction;
    public static void Connect(System.Action successCallback, System.Action<string> failureCallback)
    { 
//        Logs.Add.Info("Connecting");
		Debug.Log("Connecting");
        if (connectedToServer)
        {
			Debug.Log ("Skipping Already connected");
//            Logs.Add.Info("Skipping, already connected.");
            if (successCallback != null) successCallback();
            return;
        } 

        VerifyInstance();   
        if (OfflineMode)
        {
            connectedToServer = true;
            successCallback();
        }
        else
        {
            connectionSuccessAction = successCallback;
            connectionFailureAction = failureCallback;
            sfs.Connect(configuration.serverAddress, configuration.serverPort); 
        }

		print ("Connection request sent "+ configuration.serverAddress + " " +  configuration.serverPort);
//        Logs.Add.Info("Connection request sent "+ configuration.serverAddress + " " +  configuration.serverPort);
    }

    public static void Disconnect(System.Action successCallback)
    {
        if (!connectedToServer)
        {
			Debug.Log ("Skipping, already disconnected.");
//            Logs.Add.Info("Skipping, already disconnected.");
            if (successCallback != null) successCallback();
            return;
        }

		Debug.Log ("Disconnecting");
//        Logs.Add.Info("Disconnecting");
        connectedToServer = false;
        userLoggedIn = false;   

        if (OfflineMode)
        {
            successCallback();
        }
        else
        {
            sfs.Disconnect(); 
            disconnectSuccessAction = successCallback;
        }
		Debug.Log ("Disconnected");
//        Logs.Add.Info("Disconnected");
    } 


	void SpectatorToPlayerError(BaseEvent evt){
		if(OnSpectatorToPlayerFailure != null){			
			OnSpectatorToPlayerFailure ();
		}
		OnSpectatorToPlayerSuccess = null;
	}

	void SpectatorToPlayerSuccess(BaseEvent evt ){
		OnSpectatorToPlayerFailure = null;
		if(OnSpectatorToPlayerSuccess != null){			
			OnSpectatorToPlayerSuccess();	
		}
	}


    void OnConnection(BaseEvent evt)
    {  
        connectedToServer = (bool)evt.Params["success"]; 

        if (connectedToServer)
        {
			Debug.Log ("Connected to Server\n"+evt.GetDumpFull());
//            Logs.Add.Info("Connected to Server\n"+evt.GetDumpFull());
            if (connectionSuccessAction != null) connectionSuccessAction(); 
        }
        else
        {
			DeassignListeners ();
            _sfs = null; 
            string error = (string)evt.Params ["errorMessage"];
			Debug.Log ("Could not connect to server.\n" + evt.GetDumpFull());
//            Logs.Add.Error("Could not connect to server.\n" + evt.GetDumpFull());
            if (connectionFailureAction != null) connectionFailureAction(error);
        }
        connectionSuccessAction = null;
        connectionFailureAction = null; 
    }

    void OnConnectionLost(BaseEvent evt)
    {
//        Logs.Add.Info("Connection to Server lost " + evt.Params["reason"].ToString());
		Debug.Log("Connection to Server lost " + evt.Params["reason"].ToString());
        connectedToServer = false;
        userLoggedIn = false; 

        if (disconnectSuccessAction != null)
        {
            disconnectSuccessAction();
            disconnectSuccessAction = null;
        }

//		resetConnection ();
        FireConnectionLostEvent(evt.Params["reason"].ToString());
    }

	void resetConnection(){
		sfs.RemoveAllEventListeners ();
		_sfs = null;
		listenersAssigned = false;
		AssignListeners ();
	}

    public static void FireConnectionLostEvent(string error) {
        if (ConnectionToServerLost != null) ConnectionToServerLost(error);
    } 

    public static void Reconnect(System.Action successCallback, System.Action<string> failureCallback)
    {
        if (string.IsNullOrEmpty(lastUserName) || string.IsNullOrEmpty(lastZoneName))
        {
            if (failureCallback != null) failureCallback("NoUserLoggedIn");
            return;
        }

        Connect(
            ()=>
            {
                Login
                (
                    lastUserName, 
                    "", 
                    lastZoneName, 
                    ()=>
                    {
                        if (!string.IsNullOrEmpty(lastRoomName)) 
                        {
                            JoinRoom
                            (
                                lastRoomName, 
                                (r)=>{ if(successCallback!=null)successCallback(); }, 
                                failureCallback
                            );
                        }
                    }, 
                    failureCallback
                );
            }, 
            failureCallback
        );
    }
    #endregion

    #region Login
    static bool userLoggedIn; //don't make this public, it is not supposed to be modified from other classes
    static System.Action loginSuccessAction;
    static System.Action<string> loginFailureAction;
    static System.Action logoutAction;
    public static void Login(string username, string password, string zone, System.Action successCallback, System.Action<string> failureCallback)
    {  

		Debug.Log ("Logging in as: "+username);
		Debug.Log ("Logging in with pass: "+password);
//        Logs.Add.Info("Logging in as: "+username);

        if (!connectedToServer)
        {
            Connect(() => { Login(username, password, zone, successCallback, failureCallback); }, (string error) => { if (failureCallback != null) failureCallback(error); });
            return;
        }

        if (userLoggedIn)
        {
            if (username == loggedInUsername)
            {
                if (currentZone == zone)
                {
					Debug.Log ("Skipped login attempt, already logged in same zone.");
//                    Logs.Add.Info("Skipped login attempt, already logged in same zone.");
                    if (successCallback != null) successCallback();
                    return;
                }
                else
                {
					Debug.Log ("Aborting login attempt, already logged in in different zone. Call SwitchZone instead.");
//                    Logs.Add.Info("Aborting login attempt, already logged in in different zone. Call SwitchZone instead.");
                    if (failureCallback != null) failureCallback("already logged in in different zone");
                    //if we call SwitchZone here, we will be caught in an endless loop otherwise. Since SwitchZone calls login again.
                    return;
                }
            }
            else {
				Debug.Log ("Aborting login attempt, already logged in with different user. Call Logout first.");
//                Logs.Add.Info("Aborting login attempt, already logged in with different user. Call Logout first.");
                if (failureCallback != null) failureCallback("already logged in with different user");
                return;
            }
        }
 
        loginSuccessAction = successCallback;
        loginFailureAction = failureCallback;

        VerifyInstance(); 
        sfs.Send(new LoginRequest(username, password, zone, null));
    } 

    void OnLogin(BaseEvent evt)
    {
		Debug.Log ("logged In" + evt.GetDumpFull());
//        Logs.Add.Info("logged In" + evt.GetDumpFull());  
        userLoggedIn = true;
        lastUserName = loggedInUsername;

        if (loginSuccessAction != null) loginSuccessAction();

        loginSuccessAction = null;
        loginFailureAction = null;  
    }

    void OnLoginError(BaseEvent evt)
    { 
		Debug.Log ("log In Failed\n" + evt.GetDumpFull());
//        Logs.Add.Error("log In Failed\n" + evt.GetDumpFull());  
        userLoggedIn = false;

        if (loginFailureAction != null) loginFailureAction(evt.Params["errorMessage"].ToString());
        loginSuccessAction = null;
        loginFailureAction = null; 
    }

    public static void Logout(System.Action successCallback, System.Action<string> failureCallback)
    {   
		Debug.Log ("Logging out");
//        Logs.Add.Info("Logging out");

        if (!connectedToServer)
        {
			Debug.Log ("Skipping, Not connected to server");
//            Logs.Add.Info("Skipping, Not connected to server");
            userLoggedIn = false;
            if (successCallback != null) successCallback();
            return;
        }

        if (!userLoggedIn)
        {
			Debug.Log ("Skipping, Already Logged out / not logged in");
//            Logs.Add.Info("Skipping, Already Logged out / not logged in");
            if (successCallback != null) successCallback();
            return;
        }
 
        logoutAction = successCallback;

        VerifyInstance(); 
        sfs.Send(new LogoutRequest());
    } 

    void OnLogout(BaseEvent evt)
    {
//        Logs.Add.Info("logged Out" + evt.GetDumpFull());
		Debug.Log("logged Out" + evt.GetDumpFull());
        userLoggedIn = false; 
        lastUserName = "";

        if (logoutAction != null) logoutAction();
        logoutAction = null;  
    }
    #endregion

    #region Zone
    //This is 10% luck, 20% skill, 15% concentrated power of will, 5% pleasure, 50% pain 
    //logout the user and remember the name
    static string rememberUsername = "";
    public static void SwitchZone(string zone, System.Action successCallback, System.Action<string> failureCallback)
    { 
        VerifyInstance();
//        Logs.Add.Info("Changing Zone: " + zone);
		Debug.Log("Changing Zone: " + zone);
        if (!userLoggedIn)
        {
			Debug.Log ("Aborted. No user logged in, unable to change zone." + zone);
//            Logs.Add.Info("Aborted. No user logged in, unable to change zone." + zone);
            if (failureCallback != null) failureCallback("No user logged in");
            return;
        }
        else
        {
            if (currentZone == zone)
            {
				Debug.Log ("Skipping, already logged in same zone." + zone);
//                Logs.Add.Info("Skipping, already logged in same zone." + zone);
                if (successCallback != null) successCallback();
                return;
            }
        }

        print(" -- ");
        rememberUsername = loggedInUsername;
        Logout(
            () => 
            { 
                Login(
                    rememberUsername ,
                    "",
                    zone,
                    ()=>
                    {
                        lastZoneName = zone; 
                        if(successCallback!=null) successCallback();
                    }, 
                    failureCallback
                ); 
            }, 
            failureCallback
        );
    } 
    #endregion

    #region Room
    static System.Action<Room> joinRoomSuccessAction;
    static System.Action<string> joinRoomFailureAction;
    public static void JoinRoom(string room, System.Action<Room> successCallback, System.Action<string> failureCallback)
    {  
//        Logs.Add.Info("Joining Room: " + room);
		Debug.Log("Joining Room: " + room);
        if (!userLoggedIn)
        {
			Debug.Log ("No user logged in, unable to join room.");
//            Logs.Add.Info("No user logged in, unable to join room.");
            if (failureCallback != null) failureCallback("No user logged in");
            return;
        } 

        for (int i = 0; i < joinedRooms.Count; i++)
        { 
            if (joinedRooms[i].Name.Equals(room))
            {
				Debug.Log ("Skipping, already joined the room.");
//                Logs.Add.Info("Skipping, already joined the room.");
                if (successCallback != null) successCallback(joinedRooms[i]);
                return;
            } 
        }
 
        joinRoomSuccessAction = successCallback;
        joinRoomFailureAction = failureCallback;

        VerifyInstance(); 
        sfs.Send(new JoinRoomRequest(room, null, -1, false));
    } 

    void OnRoomJoin(BaseEvent evt)
    {
		Debug.Log ("Room joined"+evt.GetDumpFull());
//        Logs.Add.Info("Room joined"+evt.GetDumpFull());

        Room room = (Room)evt.Params["room"]; 
        lastRoomName = room.Name;

        if (joinRoomSuccessAction != null) joinRoomSuccessAction(room);
        joinRoomSuccessAction = null;
        joinRoomFailureAction = null; 
    }

    void OnRoomJoinError(BaseEvent evt)
    { 
        //User user =(User) evt.Params["user"]; 
        string error = (string)evt.Params ["errorMessage"];
        if (user==null || user.Name.Equals(loggedInUsername)) { 
            if (error.Contains("already joined"))
            {
                string roomName = error.Split(' ')[1];
                for (int i = 0; i < joinedRooms.Count; i++)
                { 
                    if (joinedRooms[i].Name.Equals(roomName))
                    {
						Debug.Log ("Already joined the room."+evt.GetDumpFull());
//                        Logs.Add.Info("Already joined the room."+evt.GetDumpFull());
                        if (joinRoomSuccessAction != null) joinRoomSuccessAction(joinedRooms[i]);
                        return;
                    } 
                }
            }
            else
            {
				Debug.Log ("Failed to Join the room"+evt.GetDumpFull());
//                Logs.Add.Info("Failed to Join the room"+evt.GetDumpFull());
                if (joinRoomFailureAction != null) joinRoomFailureAction(error); 
            }  
        }
        joinRoomSuccessAction = null;
        joinRoomFailureAction = null; 
    }

    void OnRoomEnter(BaseEvent evt)
    {
        User u = (User)evt.Params ["user"];
        Room r = (Room)evt.Params ["room"];
		Debug.Log (u.Name + " entered the room: " + r.Name);
//        Logs.Add.Info(u.Name + " entered the room: " + r.Name);
        FireRoomPlayersUpdated (r); 
    }

    static System.Action leaveRoomSuccessAction; 
    public static void LeaveRoom(Room room, System.Action successCallback, System.Action<string> failureCallback)
    { 
        if (room==null)
        {
			Debug.Log ("Null Leave room request");
//            Logs.Add.Info("Null Leave room request");
            if(failureCallback!=null) failureCallback("Null Request");
            return;
        }

		Debug.Log ("Leaving the room: " + room.Name);
//        Logs.Add.Info("Leaving the room: " + room.Name);
        leaveRoomSuccessAction = successCallback; 
        for (int i = 0; i < joinedRooms.Count; i++)
        {
            if (room.Name.Equals(joinedRooms[i].Name))
            {
                sfs.Send(new LeaveRoomRequest(room));
				Debug.Log ("Leave room request sent. Room: " + room.Name);
//                Logs.Add.Info("Leave room request sent. Room: " + room.Name);
                return;
            }
        }
		Debug.Log ("Failed to leave the room: " + room.Name + ". Not joined.");
//        Logs.Add.Info("Failed to leave the room: " + room.Name + ". Not joined.");
        if (failureCallback != null) failureCallback("Not Joined");
    }  

    private void OnRoomExit(BaseEvent evt)
    {
        User u = (User)evt.Params ["user"];
        Room r = (Room)evt.Params ["room"];
		Debug.Log (u.Name + " exited the room: " + r.Name);
//        Logs.Add.Info(u.Name + " exited the room: " + r.Name);

        if (u.Name.Equals(loggedInUsername)) { 
            if (leaveRoomSuccessAction != null) leaveRoomSuccessAction();
            leaveRoomSuccessAction = null; 
        }

        FireRoomPlayersUpdated (r);
    }

    private void OnRoomPlayerUpdate(BaseEvent evt)
    { 
        Room r = (Room)evt.Params ["room"]; 
        FireRoomPlayersUpdated (r); 
    }

	static event System.Action OnSpectatorToPlayerSuccess;
	static event System.Action OnSpectatorToPlayerFailure;

	public static void SwitchSpectatorToPlayer(System.Action OnSuccess , System.Action OnFailue){
		sfs.Send (new SpectatorToPlayerRequest());

		OnSpectatorToPlayerSuccess = OnSuccess;
		OnSpectatorToPlayerFailure = OnFailue;
	}

    public static void FireRoomPlayersUpdated(Room r) {
        if (RoomPlayersUpdated != null) RoomPlayersUpdated(r);
    }
    #endregion
 
	#region Extensions 
	public static void SendExtensionRequest(string cmd, SFSObject parameters)
	{
		CallExtension(cmd, parameters, currentRoom, null, null);
	} 
	public static void SendExtensionRequest(string cmd, SFSObject parameters, Room room)
	{
		CallExtension(cmd, parameters, room, null, null);
	} 
	public static void SendExtensionRequest(string cmd, SFSObject parameters, Room room, System.Action<SFSObject, Room> sucessCallback, System.Action<string> failureCallback)
	{
		CallExtension(cmd, parameters, room, sucessCallback, failureCallback);
	}

    class PendingExtensionRequest
    {
        public string _cmd;
        public SFSObject _parameters;
        public Room _room;
        public System.Action<SFSObject, Room> _sucessCallback;
        public System.Action<string> _failureCallback;
        public Coroutine timeoutCr;
    } 
    static List<PendingExtensionRequest> pendingExtensionRequests = new List<PendingExtensionRequest>();
    public static void CallExtension(string cmd, SFSObject parameters, System.Action<SFSObject, Room> sucessCallback, System.Action<string> failureCallback)
    {
        CallExtension(cmd, parameters, currentRoom, sucessCallback, failureCallback);
    } 
    public static void CallExtension(string cmd, SFSObject parameters, Room room, System.Action<SFSObject, Room> sucessCallback, System.Action<string> failureCallback)
    {   
        if (!userLoggedIn)
        {
			Debug.Log ("Aborted. No user logged in, unable to call extension.");
//            Logs.Add.Info("Aborted. No user logged in, unable to call extension.");
            if (failureCallback != null) failureCallback("No user logged in");
            return;
        }

        VerifyInstance(); 
//        if (loginSuccessAction != null || failureCallback != null)
        {
//            for (int i = 0; i < pendingExtensionRequests.Count; i++)
//            {
//                if (pendingExtensionRequests[i]._cmd.Equals(cmd))
//                {
//                    if ((room == null && pendingExtensionRequests[i]._room == null) || (room != null && pendingExtensionRequests[i]._room != null && pendingExtensionRequests[i]._room.Id == room.Id))
//                    {
////                        Logs.Add.Info(string.Format("Ext Request IGNORED: {0} [{1}-{2}]\nParams:\n{3}", cmd, currentZone, (room==null ? "" : room.Name), parameters.GetDumpFull()));
////                        throw new System.Exception("ExtRequestAlreadySent");
//                          return;
//                    }
//                }    
//            } 

            pendingExtensionRequests.Add(new PendingExtensionRequest()
                {
                    _cmd = cmd,
                    _parameters = parameters,
                    _room = room,
                    _sucessCallback = sucessCallback,
                    _failureCallback = failureCallback, 
                    timeoutCr = SFSObj.StartCoroutine (ExtReqTimeout(cmd, room))
                });   
            
        }

        sfs.Send(new ExtensionRequest(cmd, parameters, room));
		Debug.Log (string.Format("Ext Request Sent: {0} [{1}-{2}]\nParams:\n{3}", cmd, currentZone, (room==null ? "" : room.Name), parameters.GetDumpFull()));
//        Logs.Add.Info(string.Format("Ext Request Sent: {0} [{1}-{2}]\nParams:\n{3}", cmd, currentZone, (room==null ? "" : room.Name), parameters.GetDumpFull()));
    }
    static IEnumerator ExtReqTimeout(string cmd, Room room){
        yield return new WaitForSeconds (50);
        for (int i = 0; i < pendingExtensionRequests.Count; i++)
        {
            if (pendingExtensionRequests[i]._cmd.Equals(cmd))
            {
                if ((room == null && pendingExtensionRequests[i]._room == null) || (room != null && pendingExtensionRequests[i]._room.Id == room.Id))
                {
					Debug.Log ("Ext Request Timeout: " + cmd);
//                    Logs.Add.Info("Ext Request Timeout: " + cmd);
                    if (pendingExtensionRequests[i]._failureCallback != null) pendingExtensionRequests[i]._failureCallback("Timeout"); 
                    pendingExtensionRequests.RemoveAt(i); 
                }
            }    
        }
    }
    void HandlePendingEvents(string cmd, SFSObject dataObject, Room room){
        for (int i = 0; i < pendingExtensionRequests.Count; i++)
        {
            if (pendingExtensionRequests[i]._cmd.Equals(cmd))
            {
                if (
                        (room == null && pendingExtensionRequests[i]._room == null) 
                    ||  (room != null && pendingExtensionRequests[i]._room != null  && pendingExtensionRequests[i]._room.Id == room.Id)
                )
                {
					if (dataObject.ContainsKey("dbResult") && (!dataObject.GetBool("dbResult")))
                    {
                        if (pendingExtensionRequests[i]._failureCallback != null) 
                            pendingExtensionRequests[i]._failureCallback(dataObject.GetUtfString("message")); 
                    }
                    else
                    {
                        if (pendingExtensionRequests[i]._sucessCallback != null)
                            pendingExtensionRequests[i]._sucessCallback(dataObject, room);
                    }
                    SFSObj.StopCoroutine(pendingExtensionRequests[i].timeoutCr);
                    pendingExtensionRequests.RemoveAt(i); 
                }
            }    
        }
    }

    public static bool blockEvents = false;
    public static string prevCmd = "";
    void OnExtensionResponse(BaseEvent evt)
    {
        if (blockEvents) return;

//		foreach (var x in evt.Params) {
//			print(x + ":  " + evt.Params[x]);
//		}
        string cmd = (string)evt.Params["cmd"]; 
        SFSObject dataObject = (SFSObject)evt.Params["params"];
		Room room = null;
		if(evt.Params.ContainsKey("room")){			
			room = (Room)evt.Params["room"]; 
		}

//		Debug.Log("OnExtensionResponse"+evt.GetDumpFull());
//        System.DateTime dt = 

        FireExtensionResponseReceived(cmd, dataObject, room);
        HandlePendingEvents (cmd, dataObject, room);
        prevCmd = cmd;
    }

    static float lastResponseTime = 0;
    static int responsesReceivedLastSec = 0;
    public static void FireExtensionResponseReceived(string cmd, SFSObject dataObject, Room room)
    {
//        if (!(cmd.Equals(prevCmd) && cmd.Equals("TurnStatus")))
        {
			Debug.Log (string.Format("Ext Response: {2}[Room: {4}][{0:00}:{1:00}]\nPramas:\n{3}", Mathf.FloorToInt(Time.timeSinceLevelLoad / 60), Time.timeSinceLevelLoad % 60, cmd, dataObject.GetDumpFull(), (room==null?"":room.Name)));
//            Logs.Add.Info(string.Format("Ext Response: {2}[Room: {4}][{0:00}:{1:00}]\nPramas:\n{3}", Mathf.FloorToInt(Time.timeSinceLevelLoad / 60), Time.timeSinceLevelLoad % 60, cmd, dataObject.GetDumpFull(), (room==null?"":room.Name)));
        }
        prevCmd = cmd;
        
        if (ExtensionResponseReceived != null) ExtensionResponseReceived(cmd, dataObject, room);
        if (Time.realtimeSinceStartup - lastResponseTime < .1f)
        {
            responsesReceivedLastSec++;
            if (responsesReceivedLastSec > 10)
            {
                if (BulkResponsesReceived != null) BulkResponsesReceived();
                responsesReceivedLastSec = 0;
            }
        }
        else responsesReceivedLastSec = 0;
        lastResponseTime = Time.realtimeSinceStartup;
    }
    #endregion

    #region Public Messages
    public static void SendNewPublicMessage(string msg)
    { 
		sfs.Send(new PublicMessageRequest(msg));
    }

    public static void SendNewPublicMessage(string msg, Room room)
    { 
        sfs.Send(new PublicMessageRequest(msg, new SFSObject(), room));
		Debug.Log ("Public Message Sent: " + msg + "\nRoom: " + room.Name);
//        Logs.Add.Info("Public Message Sent: " + msg + "\nRoom: " + room.Name);
    }

    void OnNewPublicMessageReceived(BaseEvent evt)
    {
		Debug.Log ("New Message Received" + evt.GetDumpFull());
//        Logs.Add.Info("New Message Received" + evt.GetDumpFull());
        string msg = evt.Params["message"].ToString();
		User sender = (SFSUser)evt.Params["sender"];  
        Room room = (Room)evt.Params["room"];  

//		if(SceneManager.GetActiveScene().name != "SpicyRummy"){
//			string msg1 = MessageJson.CreateFromJSON(msg).message;
//			BettingOptionsManager.instance.AddMsgToChatPanel (sender.Name, msg1);
//			TableManager.instance.ShowPlayerMessage(sender.Name, msg1);
//		}

        FirePublicMessageReceived(sender.Name, msg, room);
    }

    public static void UpdateUserVariable(List<UserVariable> variables)
    {
        if (variables != null)
        {
            sfs.Send(new SetUserVariablesRequest(variables));
        }
    }

    void OnUserVariableUpdate(BaseEvent evt)
    {
        User user = (User)evt.Params["user"];

        if (user == sfs.MySelf)
        {
            return;
        }

        print("user variable update --------- > SFS"+ user.Name);
        List<string> changedVars = (List <string>)evt.Params["changedVars"];
        foreach (string item in changedVars)
        {
            print("--- > "+item);
        }
        if (UserVariableUpdated != null)
            UserVariableUpdated(user, changedVars);

    }

    public static void FirePublicMessageReceived(string sender, string msg, Room room)
    {
        if (PublicMessageReceived != null)
            PublicMessageReceived(sender, msg, room);
    }
    #endregion
}


[System.Serializable]
public class SFSConfig
{
    public string name = "";
    public string serverAddress = "";
    public int serverPort = 0;
    public string databaseAddress = "";

    public SFSConfig()
    {
        //ohgodwhy
    }

    public SFSConfig(string n, string sa, int sp, string da)
    {
        name = n;
        serverAddress = sa;
        serverPort = sp;              
        databaseAddress = da;
    }
}

public static class SFSExt
{
    public static string GetDumpFull(this SFSObject obj)
    {
        StringBuilder stringBuilder = new StringBuilder();
        SFSDataWrapper sfsData = null;
        string[] keys = obj.GetKeys();
        for (int i = 0; i < keys.Length; i++)
        {
            sfsData = obj.GetData(keys[i]);
            stringBuilder.AppendFormat("[{1}] {0}: ", keys[i], ((SFSDataType)sfsData.Type).ToString().ToLower());
            if (sfsData.Type > 8 && sfsData.Type < 18)
                stringBuilder.Append(GetCollectionDump(sfsData.Data as IEnumerable));
            else
                stringBuilder.Append(sfsData.Data.ToString());
            stringBuilder.Append("\n");
        }
        return stringBuilder.ToString();
    }

    public static string GetCollectionDump(IEnumerable collection)
    {
        StringBuilder stringBuilder = new StringBuilder();
        IEnumerator e = collection.GetEnumerator();
        e.Reset();
        while (e.MoveNext())
        {
            stringBuilder.Append("\n" + e.Current.ToString());
        }
        return stringBuilder.ToString();
    }

    public static string GetDumpFull(this BaseEvent evt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("\nType: ");
        sb.Append(evt.Type);
        sb.Append("\nParams: ");
		foreach (KeyValuePair<string , object> item in evt.Params)
        {
            sb.Append("\n"); 
            sb.Append(item.Key); 
            sb.Append(" "); 
            sb.Append(item.Value); 
        }
        return sb.ToString();
    }

    public static string GetString(this SFSObject obj, string key)
    {
        if (!string.IsNullOrEmpty(key) && obj.ContainsKey(key))
        {
            return obj.GetUtfString(key);
        }
        return string.Empty;
    }


}



//TODO:
//logout failure handling  
//leave room timeout, in fact, all requests timeout
