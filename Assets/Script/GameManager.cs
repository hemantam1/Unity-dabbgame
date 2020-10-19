using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveConstantVal;
using System.Linq;
using Sfs2X.Entities.Data;
using Sfs2X.Entities;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Image playerInfoImg;
    [HideInInspector]
    public Text[] Playerinfo;
    [HideInInspector]
    public ChooseLevel chooseLvL;
    public Image[] PlayerImg;
    [HideInInspector]
    public GameObject Msg_BackToLevl;
    [HideInInspector]
    public Text Msg_BackToLevl_Tx;
    [HideInInspector]
    public Text ContinueWithFBTx;
    [HideInInspector]
    public Text[] PlayerGoldTx;
    [HideInInspector]
    public Image ChooseLevel_PlayerImg;
    [HideInInspector]
    public static GameManager instt;
    [HideInInspector]
    public GameObject enterCodeObj;
    [HideInInspector]
    public GameObject messageDisplay;
    [HideInInspector]
    public Text MsgText;
    [HideInInspector]
    public Sprite femaleImg, maleImg;
    [HideInInspector]
    public Image playerImg;
    [HideInInspector]
    public Text PlayAsGuest_playername;
    [HideInInspector]
    public Button male, female;
    [HideInInspector]
    public Playarea playarea_SCript;
    [HideInInspector]
    public GameObject environment_obj;
    [HideInInspector]
    public GameObject gameWin;
    [HideInInspector]
    public Text gameWinnerIS;
    [HideInInspector]
    public GameObject celebrityOBJ;
    [HideInInspector]
    public GameObject playerInfo;
    [HideInInspector]
    public GameObject emojiObj;
    [HideInInspector]
    public GameObject playarea;
    [HideInInspector]
    public GameObject characterPurObj;
    [HideInInspector]
    public GameObject dicePurObj;
    [HideInInspector]
    public GameObject storeObj;
    [HideInInspector]
    public GameObject settingObj;
    [HideInInspector]
    public GameObject luckDiceObj;
    [HideInInspector]
    public GameObject chatRoomObj;
    [HideInInspector]
    public GameObject videoWatchObj;
    [HideInInspector]
    public GameObject loginType;
    [HideInInspector]
    public GameObject loginWithFB;
    [HideInInspector]
    public GameObject celebrityLogin;
    [HideInInspector]
    public GameObject guestLogin;
    [HideInInspector]
    public GameObject chooseLevel;
    [HideInInspector]
    public GameObject privateTable;
    [HideInInspector]
    public GameObject playOffline;
    [HideInInspector]
    public GameObject goldPurchase;
    [HideInInspector]
    public GameObject splashScreen;
    [HideInInspector]
    public int SelectGender;
    [HideInInspector]
    public bool IsPrivateTableCreat;
    [HideInInspector]
    public int PrivateTableVal;
    [HideInInspector]
    public int LoginTypeNo;
    [HideInInspector]
    public Image OfflinePanel;

    [HideInInspector]
    public Sprite[] splashAnim;
    [HideInInspector]
    public Image spashscreenImg;

    [HideInInspector]
    public string FacebookProfileLik;
    [HideInInspector]
    public string InstProfileLik;

    //	public Playarea pa;

    int ct_splash;

    [HideInInspector]
    public bool play_offline = false;

    void Awake()
    {
        //		PlayerPrefs.DeleteAll ();
        instt = this;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("Dabb_Game_One_timeCall") == 0)
        {
            print("Game Manager Start Call ");
            PlayerPrefs.SetInt("Dabb_Game_One_timeCall", 100);
            PlayerPrefs.SetInt(ApiConstant.TotalGold, 5000);
            //			PlayerPrefs.SetInt (ApiConstant.CurrentEnvironment, 2);
            PlayerPrefs.SetInt(ApiConstant.CurrentEnvironment, 1);  // Vishal Change
        }
        PlayerPrefs.SetInt(ApiConstant.multiplayerGame, 0);

        Set_Player_Gold_tx();

        if (!ApiConstant.OneTimeCall)
        {
            ApiConstant.OneTimeCall = true;
            StartCoroutine(StartSplashAnimation());
        }
        else
        {
            splashScreen.SetActive(false);
            chooseLevel.SetActive(true);
        }

        PlayerPrefs.SetString("DeviceID", SystemInfo.deviceUniqueIdentifier);
        PlayerPrefs.SetInt("Gems", 100);

        //		pa = GetComponent<Playarea> ();
        //		pa.enabled = true;
    }

    public void Cloase_Msg_alert()
    {
        messageDisplay.SetActive(false);
    }

    void Update()
    {

    }

    public bool Check_Internet_connection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("no internet");
            return false;
        }
        return true;
    }

    public void Gender_male()
    {
        SelectGender = 1;
        male.interactable = false;
        female.interactable = true;
    }

    public void Gender_Female()
    {
        SelectGender = 2;
        male.interactable = true;
        female.interactable = false;
    }

    public void Buy_Environment()
    {
        ButtonClickSound();
        environment_obj.SetActive(true);
        storeObj.SetActive(false);
    }

    public void Close_enviornment()
    {
        CloseBTClick();
        environment_obj.SetActive(false);
        storeObj.SetActive(true);
    }

    //========== snake 88 animation  ================

    public void PlaySPlashAnim()
    {
        StartCoroutine(StartSplashAnimation());
    }

    IEnumerator StartSplashAnimation()
    {
        yield return new WaitForSeconds(0.05f);
        if (ct_splash == 37)
        {
            Invoke("SpashScreen", 1f);
            ct_splash = 0;
            StopCoroutine(StartSplashAnimation());
        }
        else
        {
            spashscreenImg.sprite = splashAnim[ct_splash];
            ct_splash++;
            StartCoroutine(StartSplashAnimation());
        }
    }

    //========================================================

    public void Celebrity_click()
    {
        ButtonClickSound();
        celebrityOBJ.SetActive(true);
    }

    public GameObject ProfileImageButton;

    public void ProfileImageButtonDisplay()
    {
        ProfileImageButton.SetActive(true);
    }

    public void Close_Celebrity_click()
    {
        CloseBTClick();
        celebrityOBJ.SetActive(false);
    }

    public void Close_PlayerInfo_click()
    {
        CloseBTClick();
        playerInfo.SetActive(false);
    }

    public void PlayerInfo_click(int val)
    {
        ButtonClickSound();
        switch (val)
        {

            case 1:
                playerInfoImg.sprite = PlayerImg[0].sprite;
                Playerinfo[0].text = "" + Playarea.instt.playarea_text[0].text;
                Playerinfo[1].text = "" + PlayerGoldTx[0].text;
                Playerinfo[2].text = "" + Playarea.instt.GemsAllPlayer[0].text;
                Playerinfo[3].text = "" + ApiConstant.Total_Match_1;
                Playerinfo[4].text = "" + ApiConstant.No_of_time_win_1;
                Playerinfo[5].text = "" + (ApiConstant.Total_Match_1 - ApiConstant.No_of_time_win_1);
                if (ApiConstant.No_of_time_win_1 > 0)
                {
                    Debug.Log("1111");
                    Playerinfo[6].text = "" + (((float)ApiConstant.No_of_time_win_1 / (float)ApiConstant.Total_Match_1) * 100) + " % ";
                }
                else
                {
                    Playerinfo[6].text = " 0 %";
                }

                break;
            case 2:
                playerInfoImg.sprite = PlayerImg[1].sprite;
                Playerinfo[0].text = "" + Playarea.instt.playarea_text[1].text;
                Playerinfo[1].text = "" + PlayerGoldTx[1].text;
                Playerinfo[2].text = "" + Playarea.instt.GemsAllPlayer[1].text;
                Playerinfo[3].text = "" + ApiConstant.Total_Match_2;
                Playerinfo[4].text = "" + ApiConstant.No_of_time_win_2;
                Playerinfo[5].text = "" + (ApiConstant.Total_Match_2 - ApiConstant.No_of_time_win_2);

                if (ApiConstant.No_of_time_win_2 > 0)
                {
                    Playerinfo[6].text = "" + ((float)(ApiConstant.No_of_time_win_2 / (float)ApiConstant.Total_Match_2) * 100) + " % ";
                }
                else
                {
                    Playerinfo[6].text = " 0 %";
                }
                break;
            case 3:
                playerInfoImg.sprite = PlayerImg[2].sprite;
                Playerinfo[0].text = "" + Playarea.instt.playarea_text[2].text;
                Playerinfo[1].text = "" + PlayerGoldTx[2].text;
                Playerinfo[2].text = "" + Playarea.instt.GemsAllPlayer[2].text;
                Playerinfo[3].text = "" + ApiConstant.Total_Match_3;
                Playerinfo[4].text = "" + ApiConstant.No_of_time_win_3;
                Playerinfo[5].text = "" + (ApiConstant.Total_Match_3 - ApiConstant.No_of_time_win_3);

                if (ApiConstant.No_of_time_win_3 > 0)
                {
                    Playerinfo[6].text = "" + ((float)(ApiConstant.No_of_time_win_3 / (float)ApiConstant.Total_Match_3) * 100) + " % ";
                }
                else
                {
                    Playerinfo[6].text = " 0 %";
                }
                break;
            case 4:
                playerInfoImg.sprite = PlayerImg[3].sprite;
                Playerinfo[0].text = "" + Playarea.instt.playarea_text[3].text;
                Playerinfo[1].text = "" + PlayerGoldTx[3].text;
                Playerinfo[2].text = "" + Playarea.instt.GemsAllPlayer[3].text;
                Playerinfo[3].text = "" + ApiConstant.Total_Match_4;
                Playerinfo[4].text = "" + ApiConstant.No_of_time_win_4;
                Playerinfo[5].text = "" + (ApiConstant.Total_Match_4 - ApiConstant.No_of_time_win_4);

                if (ApiConstant.No_of_time_win_4 > 0)
                {
                    Playerinfo[6].text = "" + ((float)(ApiConstant.No_of_time_win_4 / (float)ApiConstant.Total_Match_4) * 100) + " % ";
                }
                else
                {
                    Playerinfo[6].text = " 0 %";
                }
                break;
        }

        playerInfo.SetActive(true);
    }

    public void PlayerInfo_click_MainPage()
    {
        ButtonClickSound();

        if (!PlayerPrefs.HasKey("DeviceID"))
        {
            PlayerPrefs.SetString("DeviceID", SystemInfo.deviceUniqueIdentifier);
        }
        //			playerInfoImg.sprite = PlayerImg [0].sprite;
        GameManager.instt.OfflinePanel.sprite = CharacterPurchase.instance.AllImageChar[CharacterPurchase.instance.playerSelect];
        Playerinfo[0].text = "" + PlayerPrefs.GetString("TempPlayerName").ToString();
        Playerinfo[1].text = "" + PlayerGoldTx[0].text;
        Playerinfo[2].text = "" + PlayerPrefs.GetInt("Gems").ToString();
        Playerinfo[3].text = "" + PlayerTotalMatchCheck();
        Playerinfo[4].text = "" + PlayerTotalWinCheck();
        Playerinfo[5].text = "" + (ApiConstant.Total_Match_1 - ApiConstant.No_of_time_win_1);
        if (ApiConstant.No_of_time_win_1 > 0)
        {
            Debug.Log("1111");
            Playerinfo[6].text = "" + (((float)ApiConstant.No_of_time_win_1 / (float)ApiConstant.Total_Match_1) * 100) + " % ";
        }
        else
        {
            Playerinfo[6].text = " 0 %";
        }
        playerInfo.SetActive(true);
    }

    int PlayerTotalMatchCheck()
    {
        int TotalMatchIS = 0;
        if (PlayerPrefs.GetInt(ApiConstant.Total_Match) != null)
        {
            TotalMatchIS = PlayerPrefs.GetInt(ApiConstant.Total_Match);
        }
        else
        {
            TotalMatchIS = 0;
        }
        return TotalMatchIS;
    }

    int PlayerTotalWinCheck()
    {
        int TotalWinMatchIS = 0;
        if (PlayerPrefs.GetInt(ApiConstant.No_of_time_win) != null)
        {
            TotalWinMatchIS = PlayerPrefs.GetInt(ApiConstant.No_of_time_win);
        }
        else
        {
            TotalWinMatchIS = 0;
        }
        return TotalWinMatchIS;
    }

    public void Emoji_click()
    {
        ButtonClickSound();
        emojiObj.SetActive(true);
    }

    public void Close_Emoji_click()
    {

    }

    public void Logout_click()
    {
        ButtonClickSound();

        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
        {
            //PhotonNetwork.Disconnect ();
        }
        ApiConstant.OneTimeCall = false;
        Application.LoadLevel("Dabb Game");
    }

    public void Player_Playoffline_click()
    {
        play_offline = true;
        playarea_SCript.IsPrivateTable_with_create_room = false;
        playarea_SCript.IsPrivateTable = false;
        //playarea_SCript.GetComponent<ConnectAndJoinRandom> ().enabled = false;
        playarea_SCript.letsPlay = true;
        ButtonClickSound();
        playarea_SCript.Set_player_name();
        playarea_SCript.num_of_Player_is = PlayerPrefs.GetInt(ApiConstant.offlinePlayerSelection) - 1;
        playarea_SCript.PlayerDecide(PlayerPrefs.GetInt(ApiConstant.offlinePlayerSelection) - 1);
        BackgroundPlay();
        playarea.SetActive(true);
        playOffline.SetActive(false);
        CharacterPurchase.instance.SetCharacterInGamePlay();
    }

    public void Private_Table_join()
    {
        playarea_SCript.IsPrivateTable = true;
        if (!Check_Internet_connection())
        {
            messageDisplay.SetActive(true);
            return;
        }
        //PhotonNetwork.LeaveRoom ();
        playarea_SCript.WaitScreenObj.SetActive(true);
        //playarea_SCript.GetComponent<ConnectAndJoinRandom> ().enabled = true;
        PlayerPrefs.SetInt(ApiConstant.num_of_player_room_is, 2);
        PlayerPrefs.SetInt(ApiConstant.multiplayerGame, 1);
        playarea_SCript.num_of_Player_is = 1;
        ButtonClickSound();
        BackgroundPlay();
        playarea.SetActive(true);
        chooseLevel.SetActive(false);
    }

    public void Player_PrivateTable_click()
    {
        enterCodeObj.SetActive(true);
    }

    public void Player_4Player_click()
    {

        if (!Check_Internet_connection())
        {
            messageDisplay.SetActive(true);
            return;
        }
        //PhotonNetwork.LeaveRoom ();
        playarea_SCript.IsPrivateTable_with_create_room = false;
        playarea_SCript.IsPrivateTable = false;
        playarea_SCript.WaitScreenObj.SetActive(true);
        //playarea_SCript.GetComponent<ConnectAndJoinRandom> ().enabled = true;
        PlayerPrefs.SetInt(ApiConstant.num_of_player_room_is, 4);
        PlayerPrefs.SetInt(ApiConstant.multiplayerGame, 1);
        playarea_SCript.num_of_Player_is = 3;
        ButtonClickSound();
        BackgroundPlay();
        playarea.SetActive(true);
        chooseLevel.SetActive(false);
    }

    public void Player_1Vs1_click()
    {
        if (!Check_Internet_connection())
        {
            messageDisplay.SetActive(true);
            return;
        }
        if (!chooseLvL.Check1_vs_1_bidAmot())
        {
            return;
        }
        //PhotonNetwork.LeaveRoom ();

        playarea_SCript.IsPrivateTable_with_create_room = false;
        playarea_SCript.IsPrivateTable = false;

        playarea_SCript.WaitScreenObj.SetActive(true);
        //playarea_SCript.GetComponent<ConnectAndJoinRandom> ().enabled = true;
        PlayerPrefs.SetInt(ApiConstant.num_of_player_room_is, 2);
        PlayerPrefs.SetInt(ApiConstant.multiplayerGame, 1);
        playarea_SCript.num_of_Player_is = 1;
        ButtonClickSound();
        BackgroundPlay();
        playarea.SetActive(true);
        chooseLevel.SetActive(false);
    }

    public void Player_Team2VSTeam2_click()
    {

        if (!Check_Internet_connection())
        {
            messageDisplay.SetActive(true);
            return;
        }
        //PhotonNetwork.LeaveRoom ();
        playarea_SCript.IsPrivateTable = false;
        playarea_SCript.WaitScreenObj.SetActive(true);
        //playarea_SCript.GetComponent<ConnectAndJoinRandom> ().enabled = true;
        PlayerPrefs.SetInt(ApiConstant.num_of_player_room_is, 4);
        PlayerPrefs.SetInt(ApiConstant.multiplayerGame, 1);
        playarea_SCript.num_of_Player_is = 3;
        ButtonClickSound();
        BackgroundPlay();
        playarea.SetActive(true);
        chooseLevel.SetActive(false);
    }

    public void SpashScreen()
    {
        splashScreen.SetActive(false);
        loginType.SetActive(true);
    }

    public void Buy_Dice_click()
    {
        ButtonClickSound();
        dicePurObj.SetActive(true);
    }

    public void Close_Dice_click()
    {
        CloseBTClick();
        dicePurObj.SetActive(false);
    }

    public void Buy_Character_click()
    {
        ButtonClickSound();
        characterPurObj.SetActive(true);
    }

    public void Close_Character_click()
    {
        CloseBTClick();
        characterPurObj.SetActive(false);
    }

    public void Store_Click()
    {
        ButtonClickSound();
        storeObj.SetActive(true);
    }

    public void Close_Store_Click()
    {
        CloseBTClick();
        storeObj.SetActive(false);
    }

    public void Setting_Click()
    {
        ButtonClickSound();
        settingObj.SetActive(true);
    }

    public void Close_Setting_Click()
    {
        CloseBTClick();
        settingObj.SetActive(false);
    }

    public void LuckyDice_click()
    {
        ButtonClickSound();
        luckDiceObj.SetActive(true);
    }

    public void Close_LuckyDice_click()
    {
        CloseBTClick();
        luckDiceObj.SetActive(false);
    }

    public void Chat_Click()
    {
        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) != 1)
        {
            MsgText.text = "You can't chat because You are Playing offline mode";
            messageDisplay.SetActive(true);
            return;
        }
        ButtonClickSound();
        chatRoomObj.SetActive(true);
    }

    public void Close_Chat_Click()
    {
        CloseBTClick();
        chatRoomObj.SetActive(false);
    }

    public void Watch_Video_Click()
    {
        print("Video Button Pressed");
        ButtonClickSound();
        videoWatchObj.SetActive(true);
    }

    public void Close_Watch_Video_Click()
    {
        CloseBTClick();
        videoWatchObj.SetActive(false);
    }

    public void GoldPurchaseClick()
    {
        ButtonClickSound();
        goldPurchase.SetActive(true);
    }

    public void Close_GoldPurchaseClick()
    {
        CloseBTClick();
        goldPurchase.SetActive(false);
    }

    public void BackTOLevel()
    {

        ButtonClickSound();
        privateTable.SetActive(false);
        playOffline.SetActive(false);
        chooseLevel.SetActive(true);

    }

    public void PrivateTableClick()
    {
        ButtonClickSound();
        privateTable.SetActive(true);
        chooseLevel.SetActive(false);
    }

    public void PlayOfflineClick()
    {
        ButtonClickSound();
        playOffline.SetActive(true);
        chooseLevel.SetActive(false);
    }

    public void LoginWithFBClick()
    {
        ButtonClickSound();
        loginType.SetActive(false);
        loginWithFB.SetActive(true);
    }

    public void LoginWithGuestClick()
    {
        ButtonClickSound();
        loginType.SetActive(false);
        guestLogin.SetActive(true);
    }

    public void CelebrityLoginClick()
    {

        ButtonClickSound();
        loginType.SetActive(false);
        celebrityLogin.SetActive(true);
    }

    public void AllLoginBackClick()
    {
        ButtonClickSound();
        loginWithFB.SetActive(false);
        guestLogin.SetActive(false);
        celebrityLogin.SetActive(false);
        loginType.SetActive(true);
    }

    public void Continue_Login()
    {
        ButtonClickSound();
        loginWithFB.SetActive(false);
        guestLogin.SetActive(false);
        celebrityLogin.SetActive(false);
        chooseLevel.SetActive(true);
    }

    public void Continue_Login_as_Guest()
    {
        if (PlayAsGuest_playername.text == "")
        {
            MsgText.text = "Please enter your name";
            messageDisplay.SetActive(true);
            return;
        }

        Login(SystemInfo.deviceUniqueIdentifier, SelectGender.ToString(), PlayAsGuest_playername.text);
        //PlayerPrefs.SetString(ApiConstant.playerName_1, PlayAsGuest_playername.text);
        //PlayerPrefs.SetString("TempPlayerName", PlayAsGuest_playername.text);
        playarea_SCript.PlayArea_Name_Set();
        ButtonClickSound();
    }

    void AfterLogin()
    {
        loginWithFB.SetActive(false);
        guestLogin.SetActive(false);
        celebrityLogin.SetActive(false);
        chooseLevel.SetActive(true);
        if (SelectGender == 1)
        {
            playerImg.sprite = maleImg;
            ChooseLevel_PlayerImg.sprite = maleImg;
        }
        else if (SelectGender == 2)
        {
            playerImg.sprite = femaleImg;
            ChooseLevel_PlayerImg.sprite = femaleImg;
        }
    }

    void Login(string userId, string avatarurl, string userName)
    {
        SFS.Connect(() =>
        {
            SFS.Login(userId, "", "DabbZone", () =>
            {
                SFSObject obj = new SFSObject();
                obj.PutUtfString("userName", userName);
                obj.PutUtfString("avatarurl", avatarurl);
                obj.PutUtfString("userId", userId);
                SFS.SendExtensionRequest("OnLogin", obj);

            }, (string error) => { });
        }, (string error) => { });
    }

    void OnEnable()
    {
        SFS.ExtensionResponseReceived += OnExtensionResponseRecieved;
    }

    void OnDisable()
    {
        SFS.ExtensionResponseReceived -= OnExtensionResponseRecieved;
    }

    void OnExtensionResponseRecieved(string cmd, SFSObject dataObject, Room room)
    {
        switch (cmd)
        {
            case "OnLogin":
                Debug.Log(dataObject);
                AfterLogin();
                break;
        }
    }

    public void Continue_Login_with_insttttt()
    {
        playarea_SCript.PlayArea_Name_Set();
        loginWithFB.SetActive(false);
        guestLogin.SetActive(false);
        celebrityLogin.SetActive(false);
        chooseLevel.SetActive(true);
    }

    public void BackgroundPlay()
    {
        Soundmanager.instance.Bg_Music_Play();
    }

    public void ButtonClickSound()
    {
        Soundmanager.instance.Play_ButtonClick();
    }

    public void CloseBTClick()
    {
        Soundmanager.instance.Play_closeClick();
    }

    public void Private_Table_Create()
    {
        privateTable.SetActive(true);
    }

    public void Private_Table_EnterCode()
    {
        IsPrivateTableCreat = false;
        enterCodeObj.SetActive(false);
        playarea_SCript.IsPrivateTable_with_create_room = false;
        Private_Table_join();
    }

    public void Private_1000()
    {
        if (PlayerPrefs.GetInt(ApiConstant.TotalGold) >= 1000)
        {
            PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - 1000);
            Set_Player_Gold_tx();
            PrivateTableVal = 1000;
            Create_Privat();
        }
        else
        {
            MsgText.text = "You don't have enough money ";
            messageDisplay.SetActive(true);
        }
    }

    public void Private_2000()
    {
        if (PlayerPrefs.GetInt(ApiConstant.TotalGold) >= 2000)
        {
            PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - 2000);
            Set_Player_Gold_tx();
            PrivateTableVal = 2000;
            Create_Privat();
        }
        else
        {
            MsgText.text = "You don't have enough money ";
            messageDisplay.SetActive(true);
        }
    }

    public void Private_2800()
    {
        if (PlayerPrefs.GetInt(ApiConstant.TotalGold) >= 2800)
        {
            PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - 2800);
            Set_Player_Gold_tx();
            PrivateTableVal = 2800;
            Create_Privat();
        }
        else
        {
            MsgText.text = "You don't have enough money ";
            messageDisplay.SetActive(true);
        }
    }

    public void Private_5000()
    {
        if (PlayerPrefs.GetInt(ApiConstant.TotalGold) >= 5000)
        {
            PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - 5000);
            Set_Player_Gold_tx();
            PrivateTableVal = 5000;
            Create_Privat();
        }
        else
        {
            MsgText.text = "You don't have enough money ";
            messageDisplay.SetActive(true);
        }
    }

    public void Private_7000()
    {
        if (PlayerPrefs.GetInt(ApiConstant.TotalGold) >= 7000)
        {
            PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - 7000);
            Set_Player_Gold_tx();
            PrivateTableVal = 7000;
            Create_Privat();
        }
        else
        {
            MsgText.text = "You don't have enough money ";
            messageDisplay.SetActive(true);
        }
    }

    public void Private_9000()
    {
        if (PlayerPrefs.GetInt(ApiConstant.TotalGold) >= 9000)
        {
            PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - 9000);
            Set_Player_Gold_tx();
            PrivateTableVal = 9000;
            Create_Privat();
        }
        else
        {
            MsgText.text = "You don't have enough money ";
            messageDisplay.SetActive(true);
        }
    }

    public void Close_EnterCOde()
    {
        enterCodeObj.SetActive(false);
    }

    void Create_Privat()
    {
        privateTable.SetActive(false);
        IsPrivateTableCreat = true;
        enterCodeObj.SetActive(false);
        playarea_SCript.IsPrivateTable_with_create_room = true;
        Private_Table_join();
    }



    public void Set_Player_Gold_tx()
    {

        PlayerGoldTx[0].text = "" + FormatCash(PlayerPrefs.GetInt(ApiConstant.TotalGold));
        PlayerGoldTx[4].text = "" + FormatCash(PlayerPrefs.GetInt(ApiConstant.TotalGold));
        PlayerGoldTx[1].text = "" + FormatCash(PlayerPrefs.GetInt(ApiConstant.TotalGold_2));
        PlayerGoldTx[2].text = "" + FormatCash(PlayerPrefs.GetInt(ApiConstant.TotalGold_3));
        PlayerGoldTx[3].text = "" + FormatCash(PlayerPrefs.GetInt(ApiConstant.TotalGold_4));
    }

    public void Close_BackToLevel()
    {
        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
        {
            //PhotonNetwork.Disconnect ();
        }
        Application.LoadLevel("Dabb Game");

    }

    //	[MenuItem ("Tools/Clear PlayerPref")]
    //	private static void NewMenuOption ()
    //	{
    //		PlayerPrefs.DeleteAll ();
    //	}

    public static string FormatCash(double Value)
    {
        if (Value >= 10000000000000000)
        {
            return (Value / 1000000000000000D).ToString("0.#AA");
        }
        if (Value >= 1000000000000000)
        {
            return (Value / 1000000000000000D).ToString("0.##AA");
        }
        if (Value >= 10000000000000)
        {
            return (Value / 1000000000000D).ToString("0.#BB");
        }
        if (Value >= 1000000000000)
        {
            return (Value / 1000000000000D).ToString("0.##BB");
        }
        if (Value >= 10000000000)
        {
            return (Value / 1000000000D).ToString("0.#B");
        }
        if (Value >= 1000000000)
        {
            return (Value / 1000000000D).ToString("0.##B");
        }

        if (Value >= 100000000)
        {
            return (Value / 1000000D).ToString("0.#M");
        }
        if (Value >= 1000000)
        {
            return (Value / 1000000D).ToString("0.##M");
        }
        if (Value >= 100000)
        {
            return (Value / 1000D).ToString("0.#K");
        }
        if (Value >= 1000)
        {
            return (Value / 1000D).ToString("0.##K");
        }

        return Value.ToString("#,0");
    }
}

//public static class AbbrevationUtility
//{
//	private static readonly SortedDictionary<long, string> abbrevations = new SortedDictionary<long, string> {
//		{ 1000,"K" },
//		{ 1000000, "M" },
//		{ 1000000000, "B" },
//		{ 1000000000000,"T" }
//	};
//
//	public static string AbbreviateNumber (float number)
//	{
//		for (int i = abbrevations.Count - 1; i >= 0; i--) {
//			KeyValuePair<long, string> pair = abbrevations.ElementAt (i);
//			if (Mathf.Abs (number) >= pair.Key) {
//				int roundedNumber = Mathf.FloorToInt (number / pair.Key);
//				return roundedNumber.ToString () + pair.Value;
//			}
//		}
//		return number.ToString ();
//	}
//}