using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
namespace Multiplayer
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager instance;


        [SerializeField]
        private string GameVersion = " 0.0.1";//tip remove this and just use Application.version

        string _nickmname = "";
        [SerializeField]
        string NickName { get { if (NickName == "") _nickmname = "Player"; return _nickmname; } }

        private void Awake()
        {
            instance = this;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            GameManager.StartOneVsOnematch += OneVsOne;
        }
        public override void OnDisable()
        {
            GameManager.StartOneVsOnematch -= OneVsOne;
            base.OnDisable();
        }
        // Start is called before the first frame update
        void Start()
        {
            if (PhotonNetwork.IsConnected)
                return;

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
            RoomOptions opt = new RoomOptions
            {

                MaxPlayers = 2
            };
            roomname = "RoomFor2" /*+ PhotonNetwork.CountOfRooms*/;

            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("TempPlayerName");

            FST_Gameplay.IsMultiplayer = true;

            PhotonNetwork.JoinOrCreateRoom(roomname, opt, TypedLobby.Default);
        }
        public void TwoVsTwo()
        {
            if (!PhotonNetwork.IsConnected)
            {
                Debug.Log("Not connected to server ");
                return;
            }
            RoomOptions opt = new RoomOptions
            {
                MaxPlayers = 4
            };
            roomname = "RoomFor4" /*+ PhotonNetwork.CountOfRooms*/;

            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("TempPlayerName");

            FST_Gameplay.IsMultiplayer = true;

            PhotonNetwork.JoinOrCreateRoom(roomname, opt, TypedLobby.Default);
        }


        /// <summary>
        /// PUN Call Backs
        /// </summary>

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("player entered > " + newPlayer.NickName);
            //make this depend on matchtype...
            if (PhotonNetwork.CurrentRoom.PlayerCount > (byte)1)
            {
                Playarea.GameStarted = true;
                Playarea.instt.WaitScreenObj.SetActive(false);
            //    PhotonNetwork.LoadLevel("GamePlay");
            }

            PlayerProfiles[newPlayer.ActorNumber-1].SetActive(true);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            FST_Gameplay.IsMaster = PhotonNetwork.IsMasterClient;
        }
        public GameObject[] PlayerProfiles;
        public override void OnJoinedRoom()
        {
            Debug.Log("actor number = " + PhotonNetwork.LocalPlayer.ActorNumber);
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { ["save_indx"] = PhotonNetwork.LocalPlayer.ActorNumber });
            Playarea.PlayerID = PhotonNetwork.LocalPlayer.ActorNumber;
            FST_Gameplay.IsMaster = PhotonNetwork.IsMasterClient;

            Debug.Log("Joined > " + PhotonNetwork.CurrentRoom.Name);
            //make this depend on matchtype...
            if (PhotonNetwork.CurrentRoom.PlayerCount > (byte)1)
            {
                Playarea.GameStarted = true;
                Playarea.instt.WaitScreenObj.SetActive(false);
             //   PhotonNetwork.LoadLevel("GamePlay");
            }

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                PlayerProfiles[PhotonNetwork.PlayerList[i].ActorNumber - 1].SetActive(true);
            }

        //   PlayerProfiles[PhotonNetwork.LocalPlayer.ActorNumber-1].SetActive(true);
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected, cause: " + cause);
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Room created: " + roomname);

        }
        public void OnCreatedRoomFailed()
        {
            Debug.Log("Room Cannot be created");
        }
        private void OnApplicationQuit()
        {
            PhotonNetwork.Disconnect();
        }
    }
}