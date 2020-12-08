using System;
using UnityEngine;
using System.Collections;
using SaveConstantVal;
using UnityEngine.UI;
using Facebook.Unity;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class ConnectAndJoin : MonoBehaviourPunCallbacks
{
    public static ConnectAndJoin instt;
    public int BidAmout, bidDIGIT;
    public GameObject codeTx;
    public Text enterPrivateTableCode;
    byte bts;
    ExitGames.Client.Photon.Hashtable playerInd, PlayName, PlayerCoin, FbID, IsFBB, IsInsta, TotMatch, MatchWin;

    public bool AutoConnect = true;

    public byte Version = 1;

    public bool ConnectInUpdate = true;

    void Awake()
    {
        instt = this;
    }

    public virtual void Start()
    {
        bts = (byte)PlayerPrefs.GetInt(ApiConstant.num_of_player_room_is);
        playerInd = new ExitGames.Client.Photon.Hashtable();
        PlayName = new ExitGames.Client.Photon.Hashtable();
        PlayerCoin = new ExitGames.Client.Photon.Hashtable();
        FbID = new ExitGames.Client.Photon.Hashtable();
        IsFBB = new ExitGames.Client.Photon.Hashtable();
        IsInsta = new ExitGames.Client.Photon.Hashtable();
        TotMatch = new ExitGames.Client.Photon.Hashtable();
        MatchWin = new ExitGames.Client.Photon.Hashtable();

    }

    public virtual void Update()
    {

        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.IsConnected)
        {
            Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings();
        }

        if (PhotonNetwork.PlayerList.Length == PlayerPrefs.GetInt(ApiConstant.num_of_player_room_is) && PhotonNetwork.CurrentRoom.IsOpen)
        {
        //    Multi_Manager.inst.CloseDisplay();

            Playarea.instt.Multiplayer_Set_SafeZone();
            Playarea.instt.Multiplayer_Set_Cardd();
            Playarea.instt.WaitScreenObj.SetActive(false);
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Playarea.GameStarted = true;

        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        //	PhotonNetwork.JoinRandomRoom ();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: " +
         "PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
      //  Create_Room();
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
       // Create_Room();
    }
    // the following methods are implemented to give you some context. re-implement them as needed.


    public override void OnJoinedRoom()
    {
        PlayerPrefs.SetInt(ApiConstant.Total_Match, PlayerPrefs.GetInt(ApiConstant.Total_Match) + 1);

        //======================
        TotMatch.Add("Total_Match_play", PlayerPrefs.GetInt(ApiConstant.Total_Match).ToString());
        PhotonNetwork.LocalPlayer.SetCustomProperties(TotMatch);

        MatchWin.Add("Total_Match_winnn", PlayerPrefs.GetInt(ApiConstant.No_of_time_win).ToString());
        PhotonNetwork.LocalPlayer.SetCustomProperties(MatchWin);
        //======================

        playerInd.Add("save_indx", PhotonNetwork.PlayerList.Length.ToString());
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerInd);

        PlayName.Add("save_palyer_name", PlayerPrefs.GetString(ApiConstant.playerName_1));
        PhotonNetwork.LocalPlayer.SetCustomProperties(PlayName);



        PlayerCoin.Add("save_palyer_coin_val", PlayerPrefs.GetInt(ApiConstant.TotalGold).ToString());
        PhotonNetwork.LocalPlayer.SetCustomProperties(PlayerCoin);

        IsFBB.Add("Save_login_type", GameManager.instt.LoginTypeNo.ToString());
        PhotonNetwork.LocalPlayer.SetCustomProperties(IsFBB);

        if (GameManager.instt.FacebookProfileLik != "")
        {
            FbID.Add("save_FaceBook_ID", GameManager.instt.FacebookProfileLik);
            PhotonNetwork.LocalPlayer.SetCustomProperties(FbID);
        }
        if (GameManager.instt.InstProfileLik != "")
        {
            IsInsta.Add("save_insta_profile_url", GameManager.instt.InstProfileLik);
            PhotonNetwork.LocalPlayer.SetCustomProperties(IsInsta);
        }

        Multi_Manager.inst.SetPayerName_miltiplayer();
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
    }

    public void Create_Room()
    {

        if (Playarea.instt.IsPrivateTable)
        {
            if (cachedRooms != null && cachedRooms.Count > 0)
            {
                foreach (RoomInfo roomInfo in cachedRooms)
                {
                    if (roomInfo.MaxPlayers == PlayerPrefs.GetInt(ApiConstant.num_of_player_room_is) && roomInfo.MaxPlayers != roomInfo.PlayerCount && roomInfo.IsOpen &&
                        roomInfo.Name == enterPrivateTableCode.text)
                    {

                        //========= private table =========

                        if (enterPrivateTableCode.text != "")
                        {
                            string TAbVal = enterPrivateTableCode.text.Substring(4, 4);
                            int Val = int.Parse(TAbVal);
                            Debug.Log("Val = " + Val);

                            if (PlayerPrefs.GetInt(ApiConstant.TotalGold) >= Val)
                            {
                                PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - Val);
                                Debug.Log("create room");
                                PhotonNetwork.JoinRandomRoom();
                                return;
                            }
                            else
                            {
                                GameManager.instt.Msg_BackToLevl_Tx.text = "You don't have enough money ";
                                GameManager.instt.Msg_BackToLevl.SetActive(true);
                                return;
                            }
                        }
                        //==================
                    }
                }
            }

            if (!GameManager.instt.IsPrivateTableCreat)
            {
                GameManager.instt.Msg_BackToLevl_Tx.text = "You enter wrong code ";
                GameManager.instt.Msg_BackToLevl.SetActive(true);
                return;
            }

            //=========== generate random code ==============  enterPrivate TableCode.text
            var chars = "ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789";
            var stringChars = new char[4];
            var random = new UnityEngine.Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[UnityEngine.Random.Range(0, chars.Length)];
            }
            var finalString = new String(stringChars);

            codeTx.GetComponent<Text>().text = "Code  :  " + finalString + "" + GameManager.instt.PrivateTableVal;
            codeTx.SetActive(true);
            //=========================
            string rmd = finalString + "" + GameManager.instt.PrivateTableVal;
            Debug.Log("finalString = = " + rmd);
            PhotonNetwork.CreateRoom(rmd, new RoomOptions() { MaxPlayers = bts }, null);
        }
        else
        {
            if (cachedRooms.Count > 0)
            {
                foreach (RoomInfo roomInfo in cachedRooms)
                {
                    if (roomInfo.MaxPlayers == PlayerPrefs.GetInt(ApiConstant.num_of_player_room_is) && roomInfo.MaxPlayers != roomInfo.PlayerCount && roomInfo.IsOpen &&
                        roomInfo.Name.Substring(0, bidDIGIT) == BidAmout.ToString())
                    {
                        Debug.Log("create room");
                        PhotonNetwork.JoinRandomRoom();
                        return;
                    }
                }
            }
            string rmd = "" + BidAmout + "room" + cachedRooms.Count + 1.ToString();
            PhotonNetwork.CreateRoom(rmd, new RoomOptions() { MaxPlayers = bts }, null);
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        cachedRooms = roomList;
        Debug.Log("OnReceivedRoomListUpdate");
    }
    private List<RoomInfo> cachedRooms;

    void OnPhotonPlayerDisconnected(Player player)
    {
        string PlayerNo = (string)player.CustomProperties["save_indx"];
        int indx = int.Parse(PlayerNo);
        Playarea.instt.PassTurn();

        Playarea.instt.playerPinObj[indx - 1].SetActive(false);
        Playarea.instt.PlayerHeaderInfoObj[indx - 1].SetActive(false);

        if (PhotonNetwork.PlayerList.Length == 1)
        {
            PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) + BidAmout * 2);
            PlayerPrefs.SetInt(ApiConstant.No_of_time_win, PlayerPrefs.GetInt(ApiConstant.No_of_time_win) + 1);
            GameManager.instt.Msg_BackToLevl_Tx.text = "You Win...";
            GameManager.instt.Msg_BackToLevl.SetActive(true);

            return;
        }

        GameManager.instt.MsgText.text = "player " + indx + " disconnect ";
        GameManager.instt.messageDisplay.SetActive(true);

        Disconnecte_Check(indx);
    }

    void OnDisconnectedFromPhoton()
    {
        string PlayerNo = (string)PhotonNetwork.LocalPlayer.CustomProperties["save_indx"];
        int indx = int.Parse(PlayerNo);
        Debug.Log(" ind  = " + indx);
        GameManager.instt.Msg_BackToLevl_Tx.text = "Network connection fail";
        GameManager.instt.Msg_BackToLevl.SetActive(true);
        Playarea.instt.playerPinObj[indx - 1].SetActive(false);
        Playarea.instt.PlayerHeaderInfoObj[indx - 1].SetActive(false);
    }

    void Disconnecte_Check(int val)
    {
        switch (val)
        {
            case 1:
                Playarea.instt.IsPlayer_1_disconnected = true;
                break;
            case 2:
                Playarea.instt.IsPlayer_2_disconnected = true;
                break;
            case 3:
                Playarea.instt.IsPlayer_3_disconnected = true;
                break;
            case 4:
                Playarea.instt.IsPlayer_4_disconnected = true;
                break;
        }
    }
}
