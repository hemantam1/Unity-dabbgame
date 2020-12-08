using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using SaveConstantVal;

public class NetworkManger : MonoBehaviourPunCallbacks
{
    public static NetworkManger instance;

    [SerializeField]
    string GameVersion = " 0.0.1";

    string _nickmname = "";
    [SerializeField]
    string NickName { get { if (NickName == "") _nickmname = "Player"; return _nickmname; } }
    bool roomOwner = false;
    private void OnEnable()
    {
        GameManager.StartOneVsOnematch += OneVsOne;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    string roomname;
    public void OneVsOne()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Not connected to server ");
            return;
        }
        RoomOptions opt = new RoomOptions();
        opt.MaxPlayers = 2;
        roomname = "Room" + PlayerPrefs.GetString("TempPlayerName");
        PhotonNetwork.JoinOrCreateRoom(roomname, opt, TypedLobby.Default);
    }
    public void TwoVsTwo()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Not connected to server ");
            return;
        }
        RoomOptions opt = new RoomOptions();
        opt.MaxPlayers = 4;
        roomname = "Room" + PlayerPrefs.GetString("TempPlayerName");
        PhotonNetwork.JoinOrCreateRoom(roomname, opt, TypedLobby.Default);
    }


    /// <summary>
    /// Call Backs
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Commected to master");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnect" + cause);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created -" + roomname);

    }
    public void OnCreatedRoomFailed()
    {
        Debug.Log("Room Cannot be created");
    }


    // On joined room
    public override void OnJoinedRoom()
    {
      
    }   


}

    // public static ConnectAndJoinRandom instt;
    // public int BidAmout, bidDIGIT;
    // public GameObject codeTx;
    // public Text enterPrivateTableCode;
    // byte bts;
    // ExitGames.Client.Photon.Hashtable playerInd, PlayName, PlayerCoin, FbID, IsFBB, IsInsta, TotMatch, MatchWin;

    // public bool AutoConnect = true;

    // public byte Version = 1;

    // public bool ConnectInUpdate = true;

    // void Awake()
    // {
    //     // instt = this;
    // }

    // public virtual void Start()
    // {
    //     bts = (byte)PlayerPrefs.GetInt(ApiConstant.num_of_player_room_is);
    //     playerInd = new ExitGames.Client.Photon.Hashtable();
    //     PlayName = new ExitGames.Client.Photon.Hashtable();
    //     PlayerCoin = new ExitGames.Client.Photon.Hashtable();
    //     FbID = new ExitGames.Client.Photon.Hashtable();
    //     IsFBB = new ExitGames.Client.Photon.Hashtable();
    //     IsInsta = new ExitGames.Client.Photon.Hashtable();
    //     TotMatch = new ExitGames.Client.Photon.Hashtable();
    //     MatchWin = new ExitGames.Client.Photon.Hashtable();
    //     PhotonNetwork.autoJoinLobby = true;    // we join randomly. always. no need to join a lobby to get the list of rooms.
    // }

    // public virtual void Update()
    // {

    //     if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
    //     {
    //         Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

    //         ConnectInUpdate = false;
    //         PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
    //     }

    //     if (PhotonNetwork.playerList.Length == PlayerPrefs.GetInt(ApiConstant.num_of_player_room_is) && PhotonNetwork.room.IsOpen)
    //     {
    //         Multi_Manager.inst.CloseDisplay();

    //         Playarea.instt.Multiplayer_Set_SafeZone();
    //         Playarea.instt.Multiplayer_Set_Cardd();
    //         Playarea.instt.WaitScreenObj.SetActive(false);
    //         PhotonNetwork.room.open = false;
    //         Playarea.instt.letsPlay = true;

    //     }
    // }

    // public virtual void OnConnectedToMaster()
    // {
    //     Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
    //     PhotonNetwork.JoinRandomRoom();
    // }

    // public virtual void OnJoinedLobby()
    // {
    //     Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
    //     PhotonNetwork.JoinRandomRoom();
    // }

    // public virtual void OnPhotonRandomJoinFailed()
    // {
    //     Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: " +
    //     "PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
    //     Create_Room();
    // }
    // // the following methods are implemented to give you some context. re-implement them as needed.

    // public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    // {
    //     Debug.Log("Failed to connect to server" + cause);
    // }

    // public void OnJoinedRoom()
    // {
    //     PlayerPrefs.SetInt(ApiConstant.Total_Match, PlayerPrefs.GetInt(ApiConstant.Total_Match) + 1);

    //     //======================
    //     TotMatch.Add("Total_Match_play", PlayerPrefs.GetInt(ApiConstant.Total_Match).ToString());
    //     PhotonNetwork.player.SetCustomProperties(TotMatch);

    //     MatchWin.Add("Total_Match_winnn", PlayerPrefs.GetInt(ApiConstant.No_of_time_win).ToString());
    //     PhotonNetwork.player.SetCustomProperties(MatchWin);
    //     //======================

    //     playerInd.Add("save_indx", PhotonNetwork.playerList.Length.ToString());
    //     PhotonNetwork.player.SetCustomProperties(playerInd);

    //     PlayName.Add("save_palyer_name", PlayerPrefs.GetString(ApiConstant.playerName_1));
    //     PhotonNetwork.player.SetCustomProperties(PlayName);



    //     PlayerCoin.Add("save_palyer_coin_val", PlayerPrefs.GetInt(ApiConstant.TotalGold).ToString());
    //     PhotonNetwork.player.SetCustomProperties(PlayerCoin);

    //     IsFBB.Add("Save_login_type", GameManager.instt.LoginTypeNo.ToString());
    //     PhotonNetwork.player.SetCustomProperties(IsFBB);

    //     if (GameManager.instt.FacebookProfileLik != "")
    //     {
    //         FbID.Add("save_FaceBook_ID", GameManager.instt.FacebookProfileLik);
    //         PhotonNetwork.player.SetCustomProperties(FbID);
    //     }
    //     if (GameManager.instt.InstProfileLik != "")
    //     {
    //         IsInsta.Add("save_insta_profile_url", GameManager.instt.InstProfileLik);
    //         PhotonNetwork.player.SetCustomProperties(IsInsta);
    //     }

    //     Multi_Manager.inst.SetPayerName_miltiplayer();
    //     Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
    // }

    // public void Create_Room()
    // {


    //     if (Playarea.instt.IsPrivateTable)
    //     {
    //         if (PhotonNetwork.GetRoomList().Length > 0)
    //         {
    //             foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
    //             {
    //                 if (roomInfo.MaxPlayers == PlayerPrefs.GetInt(ApiConstant.num_of_player_room_is) && roomInfo.MaxPlayers != roomInfo.PlayerCount && roomInfo.IsOpen &&
    //                     roomInfo.name == enterPrivateTableCode.text)
    //                 {

    //                     //========= private table =========

    //                     if (enterPrivateTableCode.text != "")
    //                     {
    //                         string TAbVal = enterPrivateTableCode.text.Substring(4, 4);
    //                         int Val = int.Parse(TAbVal);
    //                         Debug.Log("Val = " + Val);

    //                         if (PlayerPrefs.GetInt(ApiConstant.TotalGold) >= Val)
    //                         {
    //                             PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - Val);
    //                             Debug.Log("create room");
    //                             PhotonNetwork.JoinRandomRoom();
    //                             return;
    //                         }
    //                         else
    //                         {
    //                             GameManager.instt.Msg_BackToLevl_Tx.text = "You don't have enough money ";
    //                             GameManager.instt.Msg_BackToLevl.SetActive(true);
    //                             return;
    //                         }
    //                     }
    //                     //==================
    //                 }
    //             }
    //         }

    //         if (!GameManager.instt.IsPrivateTableCreat)
    //         {
    //             GameManager.instt.Msg_BackToLevl_Tx.text = "You enter wrong code ";
    //             GameManager.instt.Msg_BackToLevl.SetActive(true);
    //             return;
    //         }

    //         //=========== generate random code ==============  enterPrivate TableCode.text
    //         var chars = "ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789";
    //         var stringChars = new char[4];
    //         var random = new UnityEngine.Random();

    //         for (int i = 0; i < stringChars.Length; i++)
    //         {
    //             stringChars[i] = chars[UnityEngine.Random.Range(0, chars.Length)];
    //         }
    //         var finalString = new String(stringChars);

    //         codeTx.GetComponent<Text>().text = "Code  :  " + finalString + "" + GameManager.instt.PrivateTableVal;
    //         codeTx.SetActive(true);
    //         //=========================
    //         string rmd = finalString + "" + GameManager.instt.PrivateTableVal;
    //         Debug.Log("finalString = = " + rmd);
    //         PhotonNetwork.CreateRoom(rmd, new RoomOptions() { MaxPlayers = bts }, null);
    //     }
    //     else
    //     {
    //         if (PhotonNetwork.GetRoomList().Length > 0)
    //         {
    //             foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
    //             {
    //                 if (roomInfo.MaxPlayers == PlayerPrefs.GetInt(ApiConstant.num_of_player_room_is) && roomInfo.MaxPlayers != roomInfo.PlayerCount && roomInfo.IsOpen &&
    //                     roomInfo.name.Substring(0, bidDIGIT) == BidAmout.ToString())
    //                 {
    //                     Debug.Log("create room");
    //                     PhotonNetwork.JoinRandomRoom();
    //                     return;
    //                 }
    //             }
    //         }
    //         string rmd = "" + BidAmout + "room" + PhotonNetwork.GetRoomList().Length + 1.ToString();
    //         PhotonNetwork.CreateRoom(rmd, new RoomOptions() { MaxPlayers = bts }, null);
    //     }
    // }

    // public void OnReceivedRoomListUpdate()
    // {
    //     Debug.Log("OnReceivedRoomListUpdate");
    //     Create_Room();
    // }

    // void OnPhotonPlayerDisconnected(PhotonPlayer player)
    // {
    //     string PlayerNo = (string)player.CustomProperties["save_indx"];
    //     int indx = int.Parse(PlayerNo);
    //     Playarea.instt.PassTurn();

    //     Playarea.instt.playerPinObj[indx - 1].SetActive(false);
    //     Playarea.instt.PlayerHeaderInfoObj[indx - 1].SetActive(false);

    //     if (PhotonNetwork.playerList.Length == 1)
    //     {
    //         PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) + BidAmout * 2);
    //         PlayerPrefs.SetInt(ApiConstant.No_of_time_win, PlayerPrefs.GetInt(ApiConstant.No_of_time_win) + 1);
    //         GameManager.instt.Msg_BackToLevl_Tx.text = "You Win...";
    //         GameManager.instt.Msg_BackToLevl.SetActive(true);

    //         return;
    //     }

    //     GameManager.instt.MsgText.text = "player " + indx + " disconnect ";
    //     GameManager.instt.messageDisplay.SetActive(true);

    //     Disconnecte_Check(indx);
    // }

    // void OnDisconnectedFromPhoton()
    // {
    //     string PlayerNo = (string)PhotonNetwork.player.CustomProperties["save_indx"];
    //     int indx = int.Parse(PlayerNo);
    //     Debug.Log(" ind  = " + indx);
    //     GameManager.instt.Msg_BackToLevl_Tx.text = "Network connection fail";
    //     GameManager.instt.Msg_BackToLevl.SetActive(true);
    //     Playarea.instt.playerPinObj[indx - 1].SetActive(false);
    //     Playarea.instt.PlayerHeaderInfoObj[indx - 1].SetActive(false);
    // }

    // void Disconnecte_Check(int val)
    // {
    //     switch (val)
    //     {
    //         case 1:
    //             Playarea.instt.IsPlayer_1_disconnected = true;
    //             break;
    //         case 2:
    //             Playarea.instt.IsPlayer_2_disconnected = true;
    //             break;
    //         case 3:
    //             Playarea.instt.IsPlayer_3_disconnected = true;
    //             break;
    //         case 4:
    //             Playarea.instt.IsPlayer_4_disconnected = true;
    //             break;
    //     }
    // }


