using SaveConstantVal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

////  1 - green  , 2 - red , 3 - yellow  , 4 - blue


public class Playarea : MonoBehaviour
{
    public GameObject[] NewCharAnimObj;
    public GameObject[] GivemoreChance_CardBT;
    [HideInInspector] public GameObject[] Skip_CardBT;
    [HideInInspector] public GameObject CardMessage, turnskipObj, RollAginObj;
    [HideInInspector] public Text CardMsgProvide;
    [HideInInspector] public static Playarea instt;
    [HideInInspector] public GameObject[] AllCardObj;
    [HideInInspector] public GameObject curreentconten;
    [HideInInspector] public GameObject[] prefebmsg;
    [HideInInspector] public InputField inputfieldtext;
    [HideInInspector] public Sprite[] emo;
    [HideInInspector] public GameObject emojiobject;

    [HideInInspector] public GameObject[] DummyObj;
    [HideInInspector] public GameObject[] DummyObjCard;
    [HideInInspector] public GameObject WaitScreenObj;
    [HideInInspector] public Text[] GemsAllPlayer;
    [HideInInspector] public Text[] playarea_text;
    [HideInInspector] public Text[] SetPlayerName;
    [HideInInspector] public GameObject[] playerPinObj;
    [HideInInspector] public GameObject[] PlayerHeaderInfoObj;
    [HideInInspector] public Text[] undo_tx;
    [HideInInspector] public Image[] undofillbar;
    [HideInInspector] public Text[] skipText;
    [HideInInspector] public GameObject[] Skip_BT;

    [HideInInspector] public GameObject[] safeZoneArea;
    [HideInInspector] public GameObject[] playerTurnAnim;
    [HideInInspector] public GameObject PlayerDiceAnim;
    [HideInInspector] public Button Dicebt;

    public GameObject[] Player1Manager;
    public GameObject[] Player2Manager;
    public GameObject[] Player3Manager;
    public GameObject[] Player4Manager;

    [Header("DICE ANIMATION PLAY")]
    [HideInInspector] public Image diceImg;
    [HideInInspector] public Sprite[] diceAllImage;
    [HideInInspector] public Sprite[] oneTosixImg;
    [HideInInspector] public bool isSamePlayerTurn, IsRepetePutBackCheck, Is_3rd_Card_SkipTurnActive, Is_4th_RollTime;
    [HideInInspector] public int DiceValIS;


    int diceCt;
    int diceCt2;
    [HideInInspector] public int playerTurn, RandomValForCar;
    [HideInInspector] public int num_of_Player_is;
    [HideInInspector] public int switchPlayer_no1, switch_indx1, switchPlayer_no2, switch_indx2;

    int p1_remainStep;
    int p2_remainStep;

    [HideInInspector] public int skip_player1;
    [HideInInspector] public int skip_player2;
    [HideInInspector] public int skip_player3;
    [HideInInspector] public int skip_player4;

    int undo_player1;
    int undo_player2;
    int undo_player3;
    int undo_player4;

    [HideInInspector] public bool isStopAnim;
    [HideInInspector] public bool TurnPass_multiplayer;
    [HideInInspector] public bool IsPrivateTable;
    [HideInInspector] public bool IsPrivateTable_with_create_room;
    [HideInInspector] public int WhichPlayer_Turn_Skip, WhichPlayerRollAgin;

    [HideInInspector] public bool IsPlayer_1_disconnected;
    [HideInInspector] public bool IsPlayer_2_disconnected;
    [HideInInspector] public bool IsPlayer_3_disconnected;
    [HideInInspector] public bool IsPlayer_4_disconnected;

    [Header("Store Panel")]
    public GameObject characterSelectButton;

    public GameObject CountDown_Image;

    public Animator Anim;

    public Coroutine a;

    void Awake()
    {
        instt = this;
        CountDown_Image.SetActive(false);
    }

    void OnEnable()
    {
        if (this.gameObject.activeSelf)
        {
            characterSelectButton.transform.GetComponent<Button>().interactable = false;
            characterSelectButton.SetActive(false);
        }
    }

    void OnDisable()
    {
        if (!this.gameObject.activeSelf)
        {
            characterSelectButton.transform.GetComponent<Button>().interactable = true;
            characterSelectButton.SetActive(true);
        }
    }



    void Start()
    {
        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) != 1)
        {
            GenerateRandomVal_Card();
        }

        skip_player1 = 4;
        skip_player2 = 4;
        skip_player3 = 4;
        skip_player4 = 4;

        undo_player1 = 3;
        undo_player2 = 3;
        undo_player3 = 3;
        undo_player4 = 3;

        PlayerPrefs.SetInt(ApiConstant.TotalGoEMS, 100);
        PlayerPrefs.SetInt(ApiConstant.TotalGoEMS_2, 100);
        PlayerPrefs.SetInt(ApiConstant.TotalGoEMS_3, 100);
        PlayerPrefs.SetInt(ApiConstant.TotalGoEMS_4, 100);

        GemsAllPlayer[0].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGoEMS);
        GemsAllPlayer[1].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGoEMS_2);
        GemsAllPlayer[2].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGoEMS_3);
        GemsAllPlayer[3].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGoEMS_4);

        StartCoroutine(HighlightTurn());

        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) != 1)
        {
            reshuffle(safeZoneArea);
            OnActiveSafeZone();
            reshuffle_2(AllCardObj);
            OnActiveCard();
        }
        else
        {
            DummyObj = new GameObject[5];
            DummyObjCard = new GameObject[5];
        }
    }

    private float m_TimeToChange = 3;
    public static bool GameStarted { get; set; } = false;
    void Update()
    {
        if (!FST_Gameplay.IsMaster)//because we are doing timer here, this should only happen on master, moreso, we shpuld make a PROPER timer based on custom room props, saving issues when master handovers occur
            return;

        if (!GameStarted)
            return;

        if (m_TimeToChange > 0)
        {
            m_TimeToChange -= Time.deltaTime;
            //			print ("Time To Change  " + TimeToChange);
            return;
        }

        if (m_TimeToChange < 30)
            m_TimeToChange = 30;

        Debug.LogFormat("Player{0}_AI()", playerTurn);

        switch (playerTurn)
        {
            case 0:
                Player1_AI();
                break;

            case 1:
                Player2_AI();
                break;

            case 2:
                Player3_AI();
                break;

            case 3:
                Player4_AI();
                break;
        }

        PassTurn();
    }

    public void Check_AnyPlayer_is_Disconnected_form_Game()
    {
        if ((playerTurn == 0 && IsPlayer_1_disconnected)
            || (playerTurn == 1 && IsPlayer_2_disconnected)
            || (playerTurn == 2 && IsPlayer_3_disconnected)
            || (playerTurn == 3 && IsPlayer_4_disconnected))

            PassTurn();
    }

    //========== Dice Anim play =========
    public void Dice_Click()
    {
        if (!GameStarted)
            return;

        PlayerDiceAnim.SetActive(false);

        if (FST_Gameplay.IsMultiplayer)
        {
            if (!GameManager.instt.Check_Internet_connection())
            {
                GameManager.instt.messageDisplay.SetActive(true);
                return;
            }

            if (IsMyTurn)
                Multi_Manager.inst.MultiplayerDiceClicked();

            return;
        }

        // ===== normal player ======
        Soundmanager.instance.Play_DiceRollSound();
        for (int i = 0; i < 4; i++)
        {
            Skip_BT[i].SetActive(false);
        }
        Dicebt.enabled = false;
        StartCoroutine("PlayDiceAnim");
    }

    public void ReceiveDiceClick()
    {
        isStopAnim = false;
        TurnPass_multiplayer = false;

        Soundmanager.instance.Play_DiceRollSound();
        for (int i = 0; i < 4; i++)
        {
            Skip_BT[i].SetActive(false);
        }
        Dicebt.enabled = false;
        StartCoroutine("PlayDiceAnim");
    }

    //=================================

    IEnumerator PlayDiceAnim()
    {
        yield return new WaitForSeconds(0.05f);
        if (diceCt == 8)
        {
            DiceValIS = UnityEngine.Random.Range(0, 6);

            Multi_Manager.inst.SetDiceValmultiplayer(DiceValIS);

            diceImg.sprite = oneTosixImg[DiceValIS];

            diceCt = 0;
            //==========  Player turn ===========

            if (DiceValIS == 5)
            {
                isSamePlayerTurn = true;
            }
            else
            {
                isSamePlayerTurn = false;
            }
            //==================================

            if (Is_4th_RollTime)
            {
                Is_4th_RollTime = false;
                StopCoroutine("PlayDiceAnim");
                Check_For_Roll_other_PlayerDice();
            }
            Active_Undo_obj();
            ProvideValue();
            StopCoroutine("PlayDiceAnim");
        }
        else
        {
            diceImg.sprite = diceAllImage[diceCt];
            diceCt++;
            StartCoroutine("PlayDiceAnim");
        }
    }

    public void SetDiceVal_After_Anim(int val)
    {
        diceImg.sprite = oneTosixImg[val];
        if (val == 5)
        {

            isSamePlayerTurn = true;
        }
        else
        {
            isSamePlayerTurn = false;
        }
        Active_Undo_obj();
    }


    public void Undo_Bt_click()
    {
        switch (playerTurn)
        {
            case 0:
                if (FST_Gameplay.IsMultiplayer)
                {
                    if (PlayerID == playerTurn + 1)
                    {
                        Multi_Manager.inst.Save_Undoo();
                    }

                }
                else
                {
                    if (undo_player1 > 0 && PlayerPrefs.GetInt(ApiConstant.TotalGold) >= 1000)
                    {

                        PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - 1000);
                        GemsAllPlayer[0].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGold);

                        Close_All_Animation();
                        undo_player1--;
                        undo_tx[0].text = "" + undo_player1;
                        isSamePlayerTurn = true;
                        Dicebt.enabled = true;
                        undofillbar[0].fillAmount = 1;
                        undofillbar[0].gameObject.SetActive(false);
                    }
                    else
                    {
                        GameManager.instt.goldPurchase.SetActive(true);
                    }
                }

                break;
            case 1:
                if (FST_Gameplay.IsMultiplayer)
                {
                    if (PlayerID == playerTurn + 1)
                    {
                        Multi_Manager.inst.Save_Undoo();
                    }
                }
                else
                {
                    if (undo_player2 > 0 && PlayerPrefs.GetInt(ApiConstant.TotalGold_2) >= 1000)
                    {

                        PlayerPrefs.SetInt(ApiConstant.TotalGold_2, PlayerPrefs.GetInt(ApiConstant.TotalGold_2) - 1000);
                        GemsAllPlayer[1].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGold_2);

                        Close_All_Animation();
                        undo_player2--;
                        undo_tx[1].text = "" + undo_player2;
                        isSamePlayerTurn = true;
                        Dicebt.enabled = true;
                        undofillbar[1].gameObject.SetActive(false);
                        undofillbar[1].fillAmount = 1;
                    }
                    else
                    {
                        GameManager.instt.goldPurchase.SetActive(true);
                    }
                }

                break;
            case 2:
                if (FST_Gameplay.IsMultiplayer)
                {
                    if (PlayerID == playerTurn + 1)
                    {
                        Multi_Manager.inst.Save_Undoo();
                    }
                }
                else
                {
                    if (undo_player3 > 0 && PlayerPrefs.GetInt(ApiConstant.TotalGold_3) >= 1000)
                    {

                        PlayerPrefs.SetInt(ApiConstant.TotalGold_3, PlayerPrefs.GetInt(ApiConstant.TotalGold_3) - 1000);
                        GemsAllPlayer[2].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGold_3);

                        Close_All_Animation();
                        undo_player3--;
                        undo_tx[2].text = "" + undo_player3;
                        isSamePlayerTurn = true;
                        Dicebt.enabled = true;
                        undofillbar[2].fillAmount = 1;
                        undofillbar[2].gameObject.SetActive(false);
                    }
                    else
                    {
                        GameManager.instt.goldPurchase.SetActive(true);
                    }
                }

                break;
            case 3:
                if (FST_Gameplay.IsMultiplayer)
                {
                    if (PlayerID == playerTurn + 1)
                    {
                        Multi_Manager.inst.Save_Undoo();
                    }
                }
                else
                {
                    if (undo_player4 > 0 && PlayerPrefs.GetInt(ApiConstant.TotalGold_4) >= 1000)
                    {

                        PlayerPrefs.SetInt(ApiConstant.TotalGold_4, PlayerPrefs.GetInt(ApiConstant.TotalGold_4) - 1000);
                        GemsAllPlayer[3].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGold_4);

                        Close_All_Animation();
                        undo_player4--;
                        undo_tx[3].text = "" + undo_player4;
                        isSamePlayerTurn = true;
                        Dicebt.enabled = true;
                        undofillbar[3].fillAmount = 1;
                        undofillbar[3].gameObject.SetActive(false);
                    }
                    else
                    {
                        GameManager.instt.goldPurchase.SetActive(true);
                    }
                }
                break;
        }
    }

    public void Multiplayer_Undo_Bt_click()
    {

        switch (playerTurn)
        {
            case 0:

                if (undo_player1 > 0 && PlayerPrefs.GetInt(ApiConstant.TotalGold) >= 1000)
                {
                    Debug.Log("y  -6");
                    PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) - 1000);
                    GemsAllPlayer[0].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGold);

                    Close_All_Animation();
                    undo_player1--;
                    undo_tx[0].text = "" + undo_player1;
                    isSamePlayerTurn = true;
                    Dicebt.enabled = true;
                    undofillbar[0].fillAmount = 1;
                    undofillbar[0].gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("y  -7");
                    GameManager.instt.goldPurchase.SetActive(true);
                }
                break;
            case 1:

                if (undo_player2 > 0 && PlayerPrefs.GetInt(ApiConstant.TotalGold_2) >= 1000)
                {
                    Debug.Log("y  -3");
                    PlayerPrefs.SetInt(ApiConstant.TotalGold_2, PlayerPrefs.GetInt(ApiConstant.TotalGold_2) - 1000);
                    GemsAllPlayer[1].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGold_2);

                    Close_All_Animation();
                    undo_player2--;
                    undo_tx[1].text = "" + undo_player2;
                    isSamePlayerTurn = true;
                    Dicebt.enabled = true;
                    undofillbar[1].gameObject.SetActive(false);
                    undofillbar[1].fillAmount = 1;
                }
                else
                {
                    GameManager.instt.goldPurchase.SetActive(true);
                }
                break;
            case 2:
                if (undo_player3 > 0 && PlayerPrefs.GetInt(ApiConstant.TotalGold_3) >= 1000)
                {

                    PlayerPrefs.SetInt(ApiConstant.TotalGold_3, PlayerPrefs.GetInt(ApiConstant.TotalGold_3) - 1000);
                    GemsAllPlayer[2].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGold_3);
                    Close_All_Animation();
                    undo_player3--;
                    undo_tx[2].text = "" + undo_player3;
                    isSamePlayerTurn = true;
                    Dicebt.enabled = true;
                    undofillbar[2].fillAmount = 1;
                    undofillbar[2].gameObject.SetActive(false);
                }
                else
                {
                    GameManager.instt.goldPurchase.SetActive(true);
                }
                break;
            case 3:
                if (undo_player4 > 0 && PlayerPrefs.GetInt(ApiConstant.TotalGold_4) >= 1000)
                {
                    PlayerPrefs.SetInt(ApiConstant.TotalGold_4, PlayerPrefs.GetInt(ApiConstant.TotalGold_4) - 1000);
                    GemsAllPlayer[3].text = "" + PlayerPrefs.GetInt(ApiConstant.TotalGold_4);
                    Close_All_Animation();
                    undo_player4--;
                    undo_tx[3].text = "" + undo_player4;
                    isSamePlayerTurn = true;
                    Dicebt.enabled = true;
                    undofillbar[3].fillAmount = 1;
                    undofillbar[3].gameObject.SetActive(false);
                }
                else
                {
                    GameManager.instt.goldPurchase.SetActive(true);
                }

                break;
        }
    }

    public void Active_Undo_obj()
    {
        for (int i = 0; i < 4; i++)
        {
            undofillbar[i].gameObject.SetActive(false);
        }
        undofillbar[playerTurn].gameObject.GetComponent<Image>().fillAmount = 1;
        undofillbar[playerTurn].gameObject.SetActive(true);
    }

    private int playerTurnValue = -1;

    public void ProvideValue()
    {
        switch (playerTurn)
        {
            case 0:
                playerTurnValue = 0;
                //============================= player 1 ==========================================

                p1_remainStep = (100 - Player1Manager[0].GetComponent<PlayerManager>().Position) - 1;
                p2_remainStep = (100 - Player1Manager[1].GetComponent<PlayerManager>().Position) - 1;

                if ((Player1Manager[0].GetComponent<PlayerManager>().isFinishline && !Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5) ||
                    (Player1Manager[1].GetComponent<PlayerManager>().isFinishline && !Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5))
                {
                    PassTurn();
                    break;
                }

                if (Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard ||
                    Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard ||
                    DiceValIS == 5)
                {

                    if (((Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player1Manager[0].GetComponent<PlayerManager>().isFinishline &&
                        p1_remainStep >= DiceValIS) || (!Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)) ||

                        ((Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player1Manager[1].GetComponent<PlayerManager>().isFinishline &&
                        p2_remainStep >= DiceValIS) || (!Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)))
                    {

                        SkipHandler(playerTurn);
                        Multi_Manager.inst.Transmit_DisplaySkipM(playerTurn);

                        if (!Player1Manager[0].GetComponent<PlayerManager>().isFinishline &&
                            p1_remainStep >= DiceValIS)
                        {

                            Player1Manager[0].GetComponent<Animation>().Play("Turnn");
                            Player1Manager[0].GetComponent<Image>().raycastTarget = true;
                        }

                        if (!Player1Manager[1].GetComponent<PlayerManager>().isFinishline &&
                            p2_remainStep >= DiceValIS)
                        {

                            Player1Manager[1].GetComponent<Animation>().Play("Turnn");
                            Player1Manager[1].GetComponent<Image>().raycastTarget = true;
                        }


                        if (!Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5)
                        {
                            Player1Manager[0].GetComponent<Animation>().Stop("Turnn");
                            Player1Manager[0].GetComponent<Image>().raycastTarget = false;
                        }

                        if (!Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5)
                        {
                            Player1Manager[1].GetComponent<Animation>().Stop("Turnn");
                            Player1Manager[1].GetComponent<Image>().raycastTarget = false;
                        }
                    }
                    else
                    {
                        PassTurn();
                    }
                }
                else
                {
                    PassTurn();
                }
                //			Invoke ("Player1_AI", 3f);
                a = StartCoroutine(SeenCountDown(3));
                //=======================================================================
                break;

            case 1:

                playerTurnValue = 1;

                //============================= player 2 ==========================================
                p1_remainStep = (100 - Player2Manager[0].GetComponent<PlayerManager>().Position) - 1;
                p2_remainStep = (100 - Player2Manager[1].GetComponent<PlayerManager>().Position) - 1;

                if ((Player2Manager[0].GetComponent<PlayerManager>().isFinishline && !Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5) ||
                    (Player2Manager[1].GetComponent<PlayerManager>().isFinishline && !Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5))
                {
                    PassTurn();
                    break;
                }
                if (Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard ||
                    Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard ||
                    DiceValIS == 5)
                {

                    if (((Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player2Manager[0].GetComponent<PlayerManager>().isFinishline &&
                        p1_remainStep >= DiceValIS) || (!Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)) ||

                        ((Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player2Manager[1].GetComponent<PlayerManager>().isFinishline &&
                        p2_remainStep >= DiceValIS) || (!Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)))
                    {

                        SkipHandler(playerTurn);
                        Multi_Manager.inst.Transmit_DisplaySkipM(playerTurn);
                        if (!Player2Manager[0].GetComponent<PlayerManager>().isFinishline &&
                            p1_remainStep >= DiceValIS)
                        {

                            Player2Manager[0].GetComponent<Animation>().Play("Turnn");
                            Player2Manager[0].GetComponent<Image>().raycastTarget = true;
                        }
                        if (!Player2Manager[1].GetComponent<PlayerManager>().isFinishline &&
                            p2_remainStep >= DiceValIS)
                        {

                            Player2Manager[1].GetComponent<Animation>().Play("Turnn");
                            Player2Manager[1].GetComponent<Image>().raycastTarget = true;
                        }

                        if (!Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5)
                        {
                            Player2Manager[0].GetComponent<Animation>().Stop("Turnn");
                            Player2Manager[0].GetComponent<Image>().raycastTarget = false;
                        }

                        if (!Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5)
                        {
                            Player2Manager[1].GetComponent<Animation>().Stop("Turnn");
                            Player2Manager[1].GetComponent<Image>().raycastTarget = false;
                        }
                    }
                    else
                    {
                        PassTurn();
                        break;
                    }
                }
                else
                {
                    PassTurn();
                }
                a = StartCoroutine(SeenCountDown(3));
                //			Invoke ("Player2_AI", 3f);
                //==================================
                break;

            case 2:
                playerTurnValue = 2;
                //============================= player 3 ==========================================
                p1_remainStep = (100 - Player3Manager[0].GetComponent<PlayerManager>().Position) - 1;
                p2_remainStep = (100 - Player3Manager[1].GetComponent<PlayerManager>().Position) - 1;

                if ((Player3Manager[0].GetComponent<PlayerManager>().isFinishline && !Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5) ||
                    (Player3Manager[1].GetComponent<PlayerManager>().isFinishline && !Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5))
                {
                    PassTurn();
                    break;
                }

                if (Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard ||
                    Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard ||
                    DiceValIS == 5)
                {

                    if (((Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player3Manager[0].GetComponent<PlayerManager>().isFinishline &&
                        p1_remainStep >= DiceValIS) || (!Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)) ||

                        ((Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player3Manager[1].GetComponent<PlayerManager>().isFinishline &&
                        p2_remainStep >= DiceValIS) || (!Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)))
                    {

                        SkipHandler(playerTurn);
                        Multi_Manager.inst.Transmit_DisplaySkipM(playerTurn);


                        if (!Player3Manager[0].GetComponent<PlayerManager>().isFinishline &&
                            p1_remainStep >= DiceValIS)
                        {

                            Player3Manager[0].GetComponent<Animation>().Play("Turnn");
                            Player3Manager[0].GetComponent<Image>().raycastTarget = true;
                        }
                        if (!Player3Manager[1].GetComponent<PlayerManager>().isFinishline &&
                            p2_remainStep >= DiceValIS)
                        {

                            Player3Manager[1].GetComponent<Animation>().Play("Turnn");
                            Player3Manager[1].GetComponent<Image>().raycastTarget = true;
                        }

                        if (!Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5)
                        {
                            Player3Manager[0].GetComponent<Animation>().Stop("Turnn");
                            Player3Manager[0].GetComponent<Image>().raycastTarget = false;
                        }

                        if (!Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5)
                        {
                            Player3Manager[1].GetComponent<Animation>().Stop("Turnn");
                            Player3Manager[1].GetComponent<Image>().raycastTarget = false;
                        }
                    }
                    else
                    {
                        PassTurn();
                        break;
                    }
                }
                else
                {
                    PassTurn();
                }

                //=======================================================================
                a = StartCoroutine(SeenCountDown(3));
                //			Invoke ("Player3_AI", 3f);

                break;
            case 3:

                playerTurnValue = 3;

                //============================= player 3 ==========================================

                p1_remainStep = (100 - Player4Manager[0].GetComponent<PlayerManager>().Position) - 1;
                p2_remainStep = (100 - Player4Manager[1].GetComponent<PlayerManager>().Position) - 1;

                if ((Player4Manager[0].GetComponent<PlayerManager>().isFinishline && !Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5) ||
                    (Player4Manager[1].GetComponent<PlayerManager>().isFinishline && !Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5))
                {
                    PassTurn();
                    break;
                }

                if (Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard ||
                    Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard ||
                    DiceValIS == 5)
                {

                    if (((Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player4Manager[0].GetComponent<PlayerManager>().isFinishline &&
                        p1_remainStep >= DiceValIS) || (!Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)) ||

                        ((Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player4Manager[1].GetComponent<PlayerManager>().isFinishline &&
                        p2_remainStep >= DiceValIS) || (!Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)))
                    {

                        SkipHandler(playerTurn);
                        Multi_Manager.inst.Transmit_DisplaySkipM(playerTurn);


                        if (!Player4Manager[0].GetComponent<PlayerManager>().isFinishline &&
                            p1_remainStep >= DiceValIS)
                        {

                            Player4Manager[0].GetComponent<Animation>().Play("Turnn");
                            Player4Manager[0].GetComponent<Image>().raycastTarget = true;
                        }
                        if (!Player4Manager[1].GetComponent<PlayerManager>().isFinishline &&
                            p2_remainStep >= DiceValIS)
                        {

                            Player4Manager[1].GetComponent<Animation>().Play("Turnn");
                            Player4Manager[1].GetComponent<Image>().raycastTarget = true;
                        }

                        if (!Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5)
                        {
                            Player4Manager[0].GetComponent<Animation>().Stop("Turnn");
                            Player4Manager[0].GetComponent<Image>().raycastTarget = false;
                        }

                        if (!Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS != 5)
                        {
                            Player4Manager[1].GetComponent<Animation>().Stop("Turnn");
                            Player4Manager[1].GetComponent<Image>().raycastTarget = false;
                        }
                    }
                    else
                    {
                        PassTurn();
                        break;
                    }
                }
                else
                {
                    PassTurn();
                }
                //=======================================================================
                a = StartCoroutine(SeenCountDown(3));
                //			Invoke ("Player4_AI", 3f);

                break;
        }

    }

    public void StopCorutineFunction()
    {
        StopCoroutine("SeenCountDown");
        StopAllCoroutines();
    }


    #region  ====================== All Player AI(Auto Moving) =========================

    public void Player1_AI()
    {
        //		new WaitForSeconds (3f);
        int number = Random.Range(0, 2);

        if ((Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard))
        {
            if (!Player1Manager[0].GetComponent<PlayerManager>().isFinishline && p1_remainStep >= DiceValIS)
            {
                number = 0;
            }
        }

        if ((Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard))
        {
            if (!Player1Manager[1].GetComponent<PlayerManager>().isFinishline && p2_remainStep >= DiceValIS)
            {
                number = 1;
            }
        }

        if (((Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player1Manager[0].GetComponent<PlayerManager>().isFinishline &&
            p1_remainStep >= DiceValIS) || (!Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)) && ((Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player1Manager[1].GetComponent<PlayerManager>().isFinishline &&
            p2_remainStep >= DiceValIS) || (!Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)))
        {

            number = Random.Range(0, 2);

            //			print ("Random Number In Last Condition Is : " + number);
        }
        Player1Manager[number].GetComponent<PlayerManager>().Move_Click();
    }

    public void Player2_AI()
    {
        //		new WaitForSeconds (3f);
        int number = Random.Range(0, 2);

        if ((Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard))
        {
            if (!Player2Manager[0].GetComponent<PlayerManager>().isFinishline && p1_remainStep >= DiceValIS)
            {
                number = 0;
            }
        }

        if ((Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard))
        {
            if (!Player2Manager[1].GetComponent<PlayerManager>().isFinishline && p2_remainStep >= DiceValIS)
            {
                number = 1;
            }
        }

        if (((Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player2Manager[0].GetComponent<PlayerManager>().isFinishline &&
            p1_remainStep >= DiceValIS) || (!Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)) &&
            ((Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player2Manager[1].GetComponent<PlayerManager>().isFinishline &&
            p2_remainStep >= DiceValIS) || (!Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)))
        {

            number = Random.Range(0, 2);

            //			print ("Random Number In Last Condition Is : " + number);
        }
        Player2Manager[number].GetComponent<PlayerManager>().Move_Click();
    }

    public void Player3_AI()
    {
        //		new WaitForSeconds (3f);
        int number = Random.Range(0, 2);

        if ((Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard))
        {
            if (!Player3Manager[0].GetComponent<PlayerManager>().isFinishline && p1_remainStep >= DiceValIS)
            {
                number = 0;
            }
        }

        if ((Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard))
        {
            if (!Player3Manager[1].GetComponent<PlayerManager>().isFinishline && p2_remainStep >= DiceValIS)
            {
                number = 1;
            }
        }

        if (((Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player3Manager[0].GetComponent<PlayerManager>().isFinishline &&
            p1_remainStep >= DiceValIS) || (!Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)) &&
            ((Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player3Manager[1].GetComponent<PlayerManager>().isFinishline &&
            p2_remainStep >= DiceValIS) || (!Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)))
        {

            number = Random.Range(0, 2);

            print("Random Number In Last Condition Is : " + number);
        }
        Player3Manager[number].GetComponent<PlayerManager>().Move_Click();
    }

    public void Player4_AI()
    {
        //		new WaitForSeconds (3f);
        int number = Random.Range(0, 2);

        if ((Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard))
        {
            if (!Player4Manager[0].GetComponent<PlayerManager>().isFinishline && p1_remainStep >= DiceValIS)
            {
                number = 0;
            }
        }

        if ((Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard))
        {
            if (!Player4Manager[1].GetComponent<PlayerManager>().isFinishline && p2_remainStep >= DiceValIS)
            {
                number = 1;
            }
        }

        if (((Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player4Manager[0].GetComponent<PlayerManager>().isFinishline &&
            p1_remainStep >= DiceValIS) || (!Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)) &&
            ((Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player4Manager[1].GetComponent<PlayerManager>().isFinishline &&
            p2_remainStep >= DiceValIS) || (!Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)))
        {

            number = Random.Range(0, 2);

            print("Random Number In Last Condition Is : " + number);
        }
        Player4Manager[number].GetComponent<PlayerManager>().Move_Click();
    }


    public IEnumerator SeenCountDown(int second)
    {

        if (!Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
            !Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
            !Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
            !Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard)
        {
            yield return null;
        }
        if (Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard || Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard ||
            Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard || Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard ||
            Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard || Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard ||
            Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard || Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard)
        {

            yield return new WaitForSeconds(second);
            CountDown_Image.SetActive(true);
            Anim.CrossFade("CountDownAnim", 0f);
            m_TimeToChange = 3;
            yield return new WaitForSeconds(3);
            CountDown_Image.SetActive(false);
            //			abc = true;

            //			if (!abc) {
            switch (playerTurnValue)
            {
                case 0:
                    Player1_AI();
                    break;

                case 1:
                    Player2_AI();
                    break;

                case 2:
                    Player3_AI();
                    break;

                case 3:
                    Player4_AI();
                    break;
            }
            //				abc = true;
            //			}
            print("++++++++++++++++++");
        }

        if (!Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
            !Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
            !Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
            !Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard && !Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard && DiceValIS == 5)
        {

            yield return new WaitForSeconds(second);
            CountDown_Image.SetActive(true);
            Anim.CrossFade("CountDownAnim", 0f);
            m_TimeToChange = 3;
            yield return new WaitForSeconds(3);
            CountDown_Image.SetActive(false);
            //			abc = true;
            //			print (abc);
            //			if (!abc) {
            switch (playerTurnValue)
            {
                case 0:
                    Player1_AI();
                    break;

                case 1:
                    Player2_AI();
                    break;

                case 2:
                    Player3_AI();
                    break;

                case 3:
                    Player4_AI();
                    break;
            }
            //				abc = true;
            //			}
        }
    }

    #endregion


    Color m_Green = Color.green;
    Color m_Blue = Color.blue;
    Color m_Yellow = Color.yellow;
    Color m_Red = Color.red;
    bool initColors = false;
    IEnumerator HighlightTurn()
    {
        if (!initColors)
        {
            initColors = true;

            if (ColorUtility.TryParseHtmlString("#09FF00C0", out Color c))
                m_Green = c;

            if (ColorUtility.TryParseHtmlString("#FF0000C0", out c))
                m_Red = c;

            if (ColorUtility.TryParseHtmlString("#FBFF00C0", out c))
                m_Yellow = c;

            if (ColorUtility.TryParseHtmlString("#0000FFC0", out c))
                m_Blue = c;
        }

        yield return new WaitForSeconds(0.5f);
        //		print ("Player Turn  HighlightTurn :" + playerTurn);
        for (int i = 0; i < 4; i++)
        {
            playerTurnAnim[i].SetActive(playerTurn == i);
        }
        PlayerDiceAnim.SetActive(true);

        PlayerDiceAnim.GetComponent<Image>().color = playerTurn == 0 ? m_Green : playerTurn == 1 ? m_Red : playerTurn == 2 ? m_Yellow : m_Blue;

        switch (playerTurn)
        {
            case 0:
                print("Player 1's Turn");
                break;

            case 1:
                print("Player 2's Turn");
                break;

            case 2:
                print("Player 3's Turn");
                break;

            case 3:
                print("Player 4's Turn");
                break;
        }
    }

    public void PassTurn()
    {
        if (!GameStarted)
            return;

        //========= card 4 check  ========

        //		if (playerTurn == WhichPlayerRollAgin && Is_4th_RollTime) {
        //			Is_4th_RollTime = false;
        //			isSamePlayerTurn = true;
        //			Open_Card_Popup ("player " + (playerTurn + 1) + " will roll his dice again......");
        //		}

        //===================================

        if (IsMyTurn/*FST_Gameplay.IsMaster*/)
        {
            Debug.Log("was my turn, passing turns now. is online multiplayer = " + FST_Gameplay.IsMultiplayer);

            if (FST_Gameplay.IsMultiplayer)
                OnlineTurn();//notify others of the turn

            Close_All_Animation();
            Check_turn();
            StartCoroutine(HighlightTurn());
            Dicebt.enabled = true;

        }
#if UNITY_MOBILE
        if (FST_Gameplay.IsMultiplayer && PlayerPrefs.GetInt(ApiConstant.vibration_check) == 0)
        {
            if (PlayerID == playerTurn + 1)
            {
                Handheld.Vibrate();
            }
        }
#endif
    }

    void Check_turn()
    {
        if (!isSamePlayerTurn)
        {
            if (playerTurn == num_of_Player_is)
            {
                playerTurn = 0;
                //===================
                if (Is_3rd_Card_SkipTurnActive && playerTurn == WhichPlayer_Turn_Skip)
                {

                    Is_3rd_Card_SkipTurnActive = false;
                    if (FST_Gameplay.IsMultiplayer)
                    {
                        Open_Card_Popup("player " + (playerTurn + 1) + "  turn skip.....");
                        Multi_Manager.inst.CardPopUp("player " + (playerTurn + 1) + "  turn skip.....");
                    }
                    else
                    {
                        Debug.Log("yes - 3");
                        Open_Card_Popup("player " + (playerTurn + 1) + "  turn skip.....");
                    }

                    print("Check Turn Skip IF Called");
                    Check_turn();

                }
                //===================

            }
            else
            {

                playerTurn++;
                if (Is_3rd_Card_SkipTurnActive && playerTurn == WhichPlayer_Turn_Skip)
                {
                    Is_3rd_Card_SkipTurnActive = false;
                    if (FST_Gameplay.IsMultiplayer)
                    {
                        Open_Card_Popup("player " + (playerTurn + 1) + "  turn skip.....");
                        Multi_Manager.inst.CardPopUp("player " + (playerTurn + 1) + "  turn skip.....");
                    }
                    else
                    {
                        Open_Card_Popup("player " + (playerTurn + 1) + "  turn skip.....");
                    }
                    print("Check Turn Skip From Else IF Called");
                    Check_turn();
                }
            }
        }

        if (FST_Gameplay.IsMultiplayer)
        {
            Check_AnyPlayer_is_Disconnected_form_Game();
        }
    }

    public void Close_All_Animation()
    {

        for (int i = 0; i < 4; i++)
        {
            undofillbar[i].gameObject.SetActive(false);
        }

        Player1Manager[0].GetComponent<Animation>().Play("normal_position");
        Player1Manager[0].GetComponent<Image>().raycastTarget = false;
        Player1Manager[1].GetComponent<Animation>().Play("normal_position");
        Player1Manager[1].GetComponent<Image>().raycastTarget = false;

        Player2Manager[0].GetComponent<Animation>().Play("normal_position");
        Player2Manager[0].GetComponent<Image>().raycastTarget = false;
        Player2Manager[1].GetComponent<Animation>().Play("normal_position");
        Player2Manager[1].GetComponent<Image>().raycastTarget = false;

        Player3Manager[0].GetComponent<Animation>().Play("normal_position");
        Player3Manager[0].GetComponent<Image>().raycastTarget = false;
        Player3Manager[1].GetComponent<Animation>().Play("normal_position");
        Player3Manager[1].GetComponent<Image>().raycastTarget = false;

        Player4Manager[0].GetComponent<Animation>().Play("normal_position");
        Player4Manager[0].GetComponent<Image>().raycastTarget = false;
        Player4Manager[1].GetComponent<Animation>().Play("normal_position");
        Player4Manager[1].GetComponent<Image>().raycastTarget = false;
    }

    public void Skip_1()
    {
        isSamePlayerTurn = false;
        if (FST_Gameplay.IsMultiplayer)
        {
            if (skip_player1 == 0)
            {
            }
            else if (PlayerID == playerTurn + 1)
            {
                skip_player1--;
                Multi_Manager.inst.Save_SkipVal(1, skip_player1);
                skipText[0].text = "" + skip_player1;
                Skip_BT[0].SetActive(false);
                Multi_Manager.inst.Multiplayer_skipBt();
                PassTurn();
            }

        }
        else
        {
            if (skip_player1 == 0)
            {
            }
            else
            {
                skip_player1--;
                skipText[0].text = "" + skip_player1;
                Skip_BT[0].SetActive(false);
                StopCorutineFunction();
                PassTurn();
            }
        }
    }

    public void Skip_2()
    {
        isSamePlayerTurn = false;
        if (FST_Gameplay.IsMultiplayer)
        {
            if (skip_player2 == 0)
            {
            }
            else if (PlayerID == playerTurn + 1)
            {
                skip_player2--;
                Multi_Manager.inst.Save_SkipVal(2, skip_player2);
                skipText[1].text = "" + skip_player2;
                Skip_BT[1].SetActive(false);
                Multi_Manager.inst.Multiplayer_skipBt();
                PassTurn();
            }

        }
        else
        {
            if (skip_player2 == 0)
            {
            }
            else
            {
                skip_player2--;
                skipText[1].text = "" + skip_player2;
                Skip_BT[1].SetActive(false);
                StopCorutineFunction();
                PassTurn();
            }
        }
    }

    public void Skip_3()
    {
        isSamePlayerTurn = false;
        if (FST_Gameplay.IsMultiplayer)
        {
            if (skip_player3 == 0)
            {
            }
            else if (skip_player3 != 0 && PlayerID == playerTurn + 1)
            {
                skip_player3--;
                Multi_Manager.inst.Save_SkipVal(3, skip_player3);
                skipText[2].text = "" + skip_player3;
                Skip_BT[2].SetActive(false);
                Multi_Manager.inst.Multiplayer_skipBt();
                PassTurn();
            }
        }
        else
        {
            if (skip_player3 == 0)
            {
            }
            else
            {
                skip_player3--;
                skipText[2].text = "" + skip_player3;
                Skip_BT[2].SetActive(false);
                StopCorutineFunction();
                PassTurn();
            }
        }
    }

    public void Skip_4()
    {
        isSamePlayerTurn = false;
        if (FST_Gameplay.IsMultiplayer)
        {
            if (skip_player4 == 0)
            {
            }
            else if (PlayerID == playerTurn + 1)
            {
                skip_player4--;
                Multi_Manager.inst.Save_SkipVal(4, skip_player4);
                skipText[3].text = "" + skip_player4;
                Skip_BT[3].SetActive(false);
                Multi_Manager.inst.Multiplayer_skipBt();
                PassTurn();
            }

        }
        else
        {
            if (skip_player4 == 0)
            {
            }
            else
            {
                skip_player4--;
                skipText[3].text = "" + skip_player4;
                Skip_BT[3].SetActive(false);
                StopCorutineFunction();
                PassTurn();
            }
        }
    }

    public void SkipHandler(int val)
    {
        for (int i = 0; i < 4; i++)
        {
            Skip_BT[i].SetActive(false);
        }

        switch (val + 1)
        {
            case 1:
                if (skip_player1 != 0)
                {
                    Skip_BT[val].SetActive(true);
                    skipText[0].text = "" + skip_player1;
                }
                break;

            case 2:
                if (skip_player2 != 0)
                {
                    Skip_BT[val].SetActive(true);
                    skipText[1].text = "" + skip_player2;
                }
                break;

            case 3:
                if (skip_player3 != 0)
                {
                    Skip_BT[val].SetActive(true);
                    skipText[2].text = "" + skip_player3;
                }
                break;

            case 4:
                if (skip_player4 != 0)
                {
                    Skip_BT[val].SetActive(true);
                    skipText[3].text = "" + skip_player4;
                }
                break;
        }
    }

    //====== check more player on same position

    public void Check_PlayerOnSamePos(GameObject plyr)
    {
        int ct = 0;

        //====1====
        if (Player1Manager[0].GetComponent<PlayerManager>().Position == plyr.GetComponent<PlayerManager>().Position)
        {
            ct++;
        }

        if (Player1Manager[1].GetComponent<PlayerManager>().Position == plyr.GetComponent<PlayerManager>().Position)
        {
            ct++;
        }
        //====2====
        if (Player2Manager[0].GetComponent<PlayerManager>().Position == plyr.GetComponent<PlayerManager>().Position)
        {
            ct++;
        }

        if (Player2Manager[1].GetComponent<PlayerManager>().Position == plyr.GetComponent<PlayerManager>().Position)
        {
            ct++;
        }
        //====3====
        if (Player3Manager[1].GetComponent<PlayerManager>().Position == plyr.GetComponent<PlayerManager>().Position)
        {
            ct++;
        }
        //====2====
        if (Player3Manager[0].GetComponent<PlayerManager>().Position == plyr.GetComponent<PlayerManager>().Position)
        {
            ct++;
        }
        //====4====
        if (Player4Manager[1].GetComponent<PlayerManager>().Position == plyr.GetComponent<PlayerManager>().Position)
        {
            ct++;
        }
        //====2====
        if (Player4Manager[0].GetComponent<PlayerManager>().Position == plyr.GetComponent<PlayerManager>().Position)
        {
            ct++;
        }

        providePOS(plyr, ct);
        //==========
    }

    void providePOS(GameObject plyr, int val)
    {
        switch (val)
        {
            case 2:
                plyr.transform.position = new Vector2(plyr.transform.position.x + 28, plyr.transform.position.y - 28);
                break;
            case 3:
                plyr.transform.position = new Vector2(plyr.transform.position.x - 28, plyr.transform.position.y - 28);
                break;
            case 4:
                plyr.transform.position = new Vector2(plyr.transform.position.x + 56, plyr.transform.position.y);
                break;
            case 5:
                plyr.transform.position = new Vector2(plyr.transform.position.x - 28, plyr.transform.position.y);
                break;
        }
    }

    public void PlayerDecide(int val)
    {
        for (int i = 0; i < 4; i++)
        {
            playerPinObj[i].SetActive(false);
            PlayerHeaderInfoObj[i].SetActive(false);
        }
        for (int i = 0; i <= val; i++)
        {
            playerPinObj[i].SetActive(true);
            PlayerHeaderInfoObj[i].SetActive(true);
        }
    }

    public void Set_player_name()
    {
        if (PlayerPrefs.GetString(ApiConstant.playerName_1) == null)
        {
            PlayerPrefs.SetString(ApiConstant.playerName_1, "You");
        }
        else if (SetPlayerName[0].text != "")
        {
            PlayerPrefs.SetString(ApiConstant.playerName_1, SetPlayerName[0].text);
        }

        if (SetPlayerName[1].text == "")
        {
            PlayerPrefs.SetString(ApiConstant.playerName_2, "Player 2");
        }
        else
        {
            PlayerPrefs.SetString(ApiConstant.playerName_2, SetPlayerName[1].text);
        }

        if (SetPlayerName[2].text == "")
        {
            PlayerPrefs.SetString(ApiConstant.playerName_3, "Player 3");
        }
        else
        {
            PlayerPrefs.SetString(ApiConstant.playerName_3, SetPlayerName[2].text);
        }

        if (SetPlayerName[3].text == "")
        {
            PlayerPrefs.SetString(ApiConstant.playerName_4, "Player 4");
        }
        else
        {
            PlayerPrefs.SetString(ApiConstant.playerName_4, SetPlayerName[3].text);
        }
        PlayArea_Name_Set();
    }

    public void PlayArea_Name_Set()
    {
        playarea_text[4].text = PlayerPrefs.GetString(ApiConstant.playerName_1);
        playarea_text[0].text = PlayerPrefs.GetString(ApiConstant.playerName_1);
        playarea_text[1].text = PlayerPrefs.GetString(ApiConstant.playerName_2);
        playarea_text[2].text = PlayerPrefs.GetString(ApiConstant.playerName_3);
        playarea_text[3].text = PlayerPrefs.GetString(ApiConstant.playerName_4);
    }

    //===========  Check safe zone area    ====================

    void reshuffle(GameObject[] obj)
    {
        for (int t = 0; t < obj.Length; t++)
        {
            GameObject tmp = obj[t];
            int r = UnityEngine.Random.Range(t, obj.Length);
            obj[t] = obj[r];
            obj[r] = tmp;
        }
        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == 1)
            {
                Multi_Manager.inst.Set_Safe_Zone(
                    safeZoneArea[5].gameObject.name,
                    safeZoneArea[6].gameObject.name,
                    safeZoneArea[7].gameObject.name,
                    safeZoneArea[8].gameObject.name,
                    safeZoneArea[9].gameObject.name);
            }
        }
    }

    void reshuffle_2(GameObject[] obj)
    {
        for (int t = 0; t < obj.Length; t++)
        {
            GameObject tmp = obj[t];
            int r = UnityEngine.Random.Range(t, obj.Length);
            obj[t] = obj[r];
            obj[r] = tmp;
        }
        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == 1)
            {
                Multi_Manager.inst.Set_Que_Zone(
                    AllCardObj[5].gameObject.name,
                    AllCardObj[6].gameObject.name,
                    AllCardObj[7].gameObject.name,
                    AllCardObj[8].gameObject.name,
                    AllCardObj[9].gameObject.name);
            }
        }
    }

    void OnActiveSafeZone()
    {
        for (int i = 5; i < 10; i++)
        {
            safeZoneArea[i].SetActive(true);
        }
    }

    void OnActiveCard()
    {
        for (int i = 5; i < 10; i++)
        {
            AllCardObj[i].SetActive(true);
        }
    }

    public bool IsPlayerOnSafeZone(int pos)
    {
        for (int i = 5; i < 10; i++)
        {
            if (safeZoneArea[i].gameObject.name == pos.ToString())
            {
                return true;
            }
        }
        return false;
    }

    public bool IsPlayerOnCardPos(int pos)
    {
        for (int i = 5; i < 10; i++)
        {
            if (AllCardObj[i].gameObject.name == pos.ToString())
            {
                return true;
            }
        }
        return false;
    }

    public void Multiplayer_Set_SafeZone()
    {
        if (PlayerID == 1)
        {
            reshuffle(safeZoneArea);
        }
    }

    public void Multiplayer_Set_Cardd()
    {
        if (PlayerID == 1)
        {
            reshuffle_2(AllCardObj);
            RandomValForCar = Random.Range(1, 5);
            Multi_Manager.inst.CardValueSet_multi(RandomValForCar);
        }


    }

    public void Display_question_In_multiplayer(string one, string two, string three, string four, string five)
    {
        //==== 1 ===
        for (int j = 0; j < 11; j++)
        {
            if (AllCardObj[j].gameObject.name == one.ToString())
            {
                DummyObjCard[0] = AllCardObj[j].gameObject;
                break;
            }
        }
        //==== 2 ===
        for (int a = 0; a < 11; a++)
        {
            if (AllCardObj[a].gameObject.name == two.ToString())
            {
                DummyObjCard[1] = AllCardObj[a].gameObject;
                break;
            }
        }

        //==== 3 ===
        for (int c = 0; c < 11; c++)
        {
            if (AllCardObj[c].gameObject.name == three.ToString())
            {
                DummyObjCard[2] = AllCardObj[c].gameObject;
                break;
            }
        }
        //==== 4 ===
        for (int d = 0; d < 11; d++)
        {
            if (AllCardObj[d].gameObject.name == four.ToString())
            {
                DummyObjCard[3] = AllCardObj[d].gameObject;
                break;
            }
        }
        //==== 5 ===
        for (int e = 0; e < 11; e++)
        {
            if (AllCardObj[e].gameObject.name == five)
            {
                DummyObjCard[4] = AllCardObj[e].gameObject;
                break;
            }
        }
        //======================
        AllCardObj[5] = DummyObjCard[0];
        AllCardObj[6] = DummyObjCard[1];
        AllCardObj[7] = DummyObjCard[2];
        AllCardObj[8] = DummyObjCard[3];
        AllCardObj[9] = DummyObjCard[4];

        Invoke("OnActiveCard", 1.5f);
    }

    public void Display_SafeZone_In_multiplayer(string one, string two, string three, string four, string five)
    {
        //==== 1 ===
        for (int j = 0; j < 17; j++)
        {
            if (safeZoneArea[j].gameObject.name == one.ToString())
            {
                DummyObj[0] = safeZoneArea[j].gameObject;
                break;
            }
        }
        //==== 2 ===
        for (int a = 0; a < 17; a++)
        {
            if (safeZoneArea[a].gameObject.name == two.ToString())
            {
                DummyObj[1] = safeZoneArea[a].gameObject;
                break;
            }
        }

        //==== 3 ===
        for (int c = 0; c < 17; c++)
        {
            if (safeZoneArea[c].gameObject.name == three.ToString())
            {
                DummyObj[2] = safeZoneArea[c].gameObject;
                break;
            }
        }
        //==== 4 ===
        for (int d = 0; d < 17; d++)
        {
            if (safeZoneArea[d].gameObject.name == four.ToString())
            {
                DummyObj[3] = safeZoneArea[d].gameObject;
                break;
            }
        }
        //==== 5 ===
        for (int e = 0; e < 17; e++)
        {
            if (safeZoneArea[e].gameObject.name == five)
            {
                DummyObj[4] = safeZoneArea[e].gameObject;
                break;
            }
        }
        //======================
        safeZoneArea[5] = DummyObj[0];
        safeZoneArea[6] = DummyObj[1];
        safeZoneArea[7] = DummyObj[2];
        safeZoneArea[8] = DummyObj[3];
        safeZoneArea[9] = DummyObj[4];

        Invoke("OnActiveSafeZone", 1.5f);
    }

    //================  chattt ======================
    public void msgdata()
    {
        switch (PlayerID)
        {
            case 1:
                Multi_Manager.inst.sendmesg(playarea_text[0].text, inputfieldtext.text);
                break;
            case 2:
                Multi_Manager.inst.sendmesg(playarea_text[1].text, inputfieldtext.text);
                break;
            case 3:
                Multi_Manager.inst.sendmesg(playarea_text[2].text, inputfieldtext.text);
                break;
            case 4:
                Multi_Manager.inst.sendmesg(playarea_text[3].text, inputfieldtext.text);
                break;
        }
        inputfieldtext.text = "";
    }


    public void emojiedata(int no)
    {
        switch (PlayerID)
        {

            case 1:
                Multi_Manager.inst.imojisimg(playarea_text[0].text, no);

                break;
            case 2:

                Multi_Manager.inst.imojisimg(playarea_text[1].text, no);
                break;
            case 3:

                Multi_Manager.inst.imojisimg(playarea_text[2].text, no);
                break;
            case 4:

                Multi_Manager.inst.imojisimg(playarea_text[3].text, no);
                break;
        }
        emojiobject.SetActive(false);
        inputfieldtext.text = "";
    }

    public void messeage(string name, string msg, int languagenumber)
    {

        print("Language Is From Playarea Message " + languagenumber);

        GameObject obj = (GameObject)Instantiate(prefebmsg[languagenumber]);
        print(obj.gameObject.name);
        obj.transform.SetParent(curreentconten.transform, false);
        obj.transform.GetChild(0).GetComponent<Text>().text = name + ":";
        obj.transform.GetChild(1).gameObject.SetActive(false);
        obj.transform.GetChild(2).GetComponent<Text>().text = msg;
    }

    public void emojis(string nm, int img, int languagenumber)
    {
        print("Language Is From Playarea Emoji " + languagenumber);
        GameObject obj = (GameObject)Instantiate(prefebmsg[languagenumber]);
        print(obj.gameObject.name);
        obj.transform.SetParent(curreentconten.transform, false);
        obj.transform.GetChild(0).GetComponent<Text>().text = nm + ":";
        obj.transform.GetChild(1).GetComponent<Image>().sprite = emo[img];
        obj.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void Open_Card_Popup(string msg)
    {
        CardMessage.SetActive(true);
        CardMsgProvide.text = "" + msg;
    }

    public void Close_Card_Popup()
    {
        CardMessage.SetActive(false);
    }

    public void Check_For_swich_posss()
    {
        // ======= playe 1 ========

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == playerTurn + 1)
            {
                if (Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player1Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player1Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player1Manager[0].GetComponent<Image>().raycastTarget = true;
                    Player1Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

                }
                if (Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player1Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player1Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player1Manager[1].GetComponent<Image>().raycastTarget = true;
                    Player1Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

                }
            }

        }
        else
        {
            if (Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player1Manager[0].GetComponent<PlayerManager>().isFinishline)
            {

                Player1Manager[0].GetComponent<Animation>().Play("Turnn");
                Player1Manager[0].GetComponent<Image>().raycastTarget = true;
                Player1Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

            }
            if (Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player1Manager[1].GetComponent<PlayerManager>().isFinishline)
            {

                Player1Manager[1].GetComponent<Animation>().Play("Turnn");
                Player1Manager[1].GetComponent<Image>().raycastTarget = true;
                Player1Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

            }
        }





        //========playe 2 ===============

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == playerTurn + 1)
            {
                if (Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player2Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player2Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player2Manager[0].GetComponent<Image>().raycastTarget = true;
                    Player2Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

                }
                if (Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player2Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player2Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player2Manager[1].GetComponent<Image>().raycastTarget = true;
                    Player2Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

                }
            }
        }
        else
        {
            if (Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player2Manager[0].GetComponent<PlayerManager>().isFinishline)
            {

                Player2Manager[0].GetComponent<Animation>().Play("Turnn");
                Player2Manager[0].GetComponent<Image>().raycastTarget = true;
                Player2Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

            }
            if (Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player2Manager[1].GetComponent<PlayerManager>().isFinishline)
            {

                Player2Manager[1].GetComponent<Animation>().Play("Turnn");
                Player2Manager[1].GetComponent<Image>().raycastTarget = true;
                Player2Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

            }
        }



        //========playe 3 ===============

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == playerTurn + 1)
            {
                if (Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player3Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player3Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player3Manager[0].GetComponent<Image>().raycastTarget = true;
                    Player3Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

                }
                if (Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player3Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player3Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player3Manager[1].GetComponent<Image>().raycastTarget = true;
                    Player3Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

                }
            }
        }
        else
        {
            if (Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player3Manager[0].GetComponent<PlayerManager>().isFinishline)
            {

                Player3Manager[0].GetComponent<Animation>().Play("Turnn");
                Player3Manager[0].GetComponent<Image>().raycastTarget = true;
                Player3Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

            }
            if (Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player3Manager[1].GetComponent<PlayerManager>().isFinishline)
            {

                Player3Manager[1].GetComponent<Animation>().Play("Turnn");
                Player3Manager[1].GetComponent<Image>().raycastTarget = true;
                Player3Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

            }
        }



        //========playe 4 ===============

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == playerTurn + 1)
            {
                if (Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player4Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player4Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player4Manager[0].GetComponent<Image>().raycastTarget = true;
                    Player4Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

                }
                if (Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player4Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player4Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player4Manager[1].GetComponent<Image>().raycastTarget = true;
                    Player4Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

                }
            }
        }
        else
        {
            if (Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player4Manager[0].GetComponent<PlayerManager>().isFinishline)
            {

                Player4Manager[0].GetComponent<Animation>().Play("Turnn");
                Player4Manager[0].GetComponent<Image>().raycastTarget = true;
                Player4Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

            }
            if (Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player4Manager[1].GetComponent<PlayerManager>().isFinishline)
            {

                Player4Manager[1].GetComponent<Animation>().Play("Turnn");
                Player4Manager[1].GetComponent<Image>().raycastTarget = true;
                Player4Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = true;

            }
        }



        //=======================
    }

    public void OffSwitchBoolen()
    {
        Player1Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = false;
        Player1Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = false;

        Player2Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = false;
        Player2Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = false;

        Player3Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = false;
        Player3Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = false;

        Player4Manager[0].GetComponent<PlayerManager>().IsSwitchPositionTime = false;
        Player4Manager[1].GetComponent<PlayerManager>().IsSwitchPositionTime = false;


    }

    public void Exchange_pos()
    {

        switch (switchPlayer_no1)
        {
            case 10:

                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Position = switch_indx2;
                Playarea.instt.Player1Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx2].transform.position;
                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                Exchange_pos_2();
                break;
            case 11:
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().Position = switch_indx2;
                Playarea.instt.Player1Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx2].transform.position;
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                Exchange_pos_2();
                break;
            case 20:
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().Position = switch_indx2;
                Playarea.instt.Player2Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx2].transform.position;
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                Exchange_pos_2();
                break;
            case 21:
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().Position = switch_indx2;
                Playarea.instt.Player2Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx2].transform.position;
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                Exchange_pos_2();
                break;
            case 30:
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().Position = switch_indx2;
                Playarea.instt.Player3Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx2].transform.position;
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                Exchange_pos_2();
                break;
            case 31:
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().Position = switch_indx2;
                Playarea.instt.Player3Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx2].transform.position;
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                Exchange_pos_2();
                break;
            case 40:
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().Position = switch_indx2;
                Playarea.instt.Player4Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx2].transform.position;
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                Exchange_pos_2();
                break;
            case 41:
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().Position = switch_indx2;
                Playarea.instt.Player4Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx2].transform.position;
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                Exchange_pos_2();
                break;
        }
    }

    public void Exchange_pos_2()
    {
        switch (switchPlayer_no2)
        {
            case 10:

                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Position = switch_indx1;
                Playarea.instt.Player1Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx1].transform.position;
                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                break;
            case 11:
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().Position = switch_indx1;
                Playarea.instt.Player1Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx1].transform.position;
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                break;
            case 20:
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().Position = switch_indx1;
                Playarea.instt.Player2Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx1].transform.position;
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                break;
            case 21:
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().Position = switch_indx1;
                Playarea.instt.Player2Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx1].transform.position;
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                break;
            case 30:
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().Position = switch_indx1;
                Playarea.instt.Player3Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx1].transform.position;
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                break;
            case 31:
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().Position = switch_indx1;
                Playarea.instt.Player3Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx1].transform.position;
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;
                break;
            case 40:
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().Position = switch_indx1;
                Playarea.instt.Player4Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx1].transform.position;
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;
                break;
            case 41:
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().Position = switch_indx1;
                Playarea.instt.Player4Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[switch_indx1].transform.position;
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;
                break;
        }

        PassTurn();
        OffSwitchBoolen();
    }

    //==============  card option 2 - put back 3 5 7  =======================

    public void PutBackOpen(int Put_back_val)
    {
        //===== player 1 ========
        if (FST_Gameplay.IsMultiplayer)
        {            if (PlayerID == playerTurn + 1)
            {
                if (Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player1Manager[0].GetComponent<PlayerManager>().isFinishline &&
                    Player1Manager[0].GetComponent<PlayerManager>().Position > Put_back_val)
                {

                    Player1Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player1Manager[0].GetComponent<Image>().raycastTarget = true;
                    Player1Manager[0].GetComponent<PlayerManager>().IsPutBackTime = true;

                }
                if (Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player1Manager[1].GetComponent<PlayerManager>().isFinishline &&
                    Player1Manager[1].GetComponent<PlayerManager>().Position > Put_back_val)
                {

                    Player1Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player1Manager[1].GetComponent<Image>().raycastTarget = true;
                    Player1Manager[1].GetComponent<PlayerManager>().IsPutBackTime = true;

                }
            }
        }
        else
        {

            if (Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player1Manager[0].GetComponent<PlayerManager>().isFinishline &&
                Player1Manager[0].GetComponent<PlayerManager>().Position > Put_back_val)
            {

                Player1Manager[0].GetComponent<Animation>().Play("Turnn");
                Player1Manager[0].GetComponent<Image>().raycastTarget = true;
                Player1Manager[0].GetComponent<PlayerManager>().IsPutBackTime = true;

            }
            if (Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player1Manager[1].GetComponent<PlayerManager>().isFinishline &&
                Player1Manager[1].GetComponent<PlayerManager>().Position > Put_back_val)
            {

                Player1Manager[1].GetComponent<Animation>().Play("Turnn");
                Player1Manager[1].GetComponent<Image>().raycastTarget = true;
                Player1Manager[1].GetComponent<PlayerManager>().IsPutBackTime = true;

            }

        }

        //===== player 2 ========

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == playerTurn + 1)
            {
                if (Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player2Manager[0].GetComponent<PlayerManager>().isFinishline &&
                    Player2Manager[0].GetComponent<PlayerManager>().Position > Put_back_val)
                {

                    Player2Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player2Manager[0].GetComponent<Image>().raycastTarget = true;
                    Player2Manager[0].GetComponent<PlayerManager>().IsPutBackTime = true;

                }
                if (Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player2Manager[1].GetComponent<PlayerManager>().isFinishline &&
                    Player2Manager[1].GetComponent<PlayerManager>().Position > Put_back_val)
                {

                    Player2Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player2Manager[1].GetComponent<Image>().raycastTarget = true;
                    Player2Manager[1].GetComponent<PlayerManager>().IsPutBackTime = true;

                }
            }
        }
        else
        {

            if (Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player2Manager[0].GetComponent<PlayerManager>().isFinishline &&
                Player2Manager[0].GetComponent<PlayerManager>().Position > Put_back_val)
            {

                Player2Manager[0].GetComponent<Animation>().Play("Turnn");
                Player2Manager[0].GetComponent<Image>().raycastTarget = true;
                Player2Manager[0].GetComponent<PlayerManager>().IsPutBackTime = true;

            }
            if (Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player2Manager[1].GetComponent<PlayerManager>().isFinishline &&
                Player2Manager[1].GetComponent<PlayerManager>().Position > Put_back_val)
            {

                Player2Manager[1].GetComponent<Animation>().Play("Turnn");
                Player2Manager[1].GetComponent<Image>().raycastTarget = true;
                Player2Manager[1].GetComponent<PlayerManager>().IsPutBackTime = true;

            }

        }

        //===== player 3 ========

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == playerTurn + 1)
            {
                if (Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player3Manager[0].GetComponent<PlayerManager>().isFinishline &&
                    Player3Manager[0].GetComponent<PlayerManager>().Position > Put_back_val)
                {

                    Player3Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player3Manager[0].GetComponent<Image>().raycastTarget = true;
                    Player3Manager[0].GetComponent<PlayerManager>().IsPutBackTime = true;

                }
                if (Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player3Manager[1].GetComponent<PlayerManager>().isFinishline &&
                    Player3Manager[1].GetComponent<PlayerManager>().Position > Put_back_val)
                {

                    Player3Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player3Manager[1].GetComponent<Image>().raycastTarget = true;
                    Player3Manager[1].GetComponent<PlayerManager>().IsPutBackTime = true;

                }
            }
        }
        else
        {
            if (Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player3Manager[0].GetComponent<PlayerManager>().isFinishline &&
                Player3Manager[0].GetComponent<PlayerManager>().Position > Put_back_val)
            {

                Player3Manager[0].GetComponent<Animation>().Play("Turnn");
                Player3Manager[0].GetComponent<Image>().raycastTarget = true;
                Player3Manager[0].GetComponent<PlayerManager>().IsPutBackTime = true;

            }
            if (Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player3Manager[1].GetComponent<PlayerManager>().isFinishline &&
                Player3Manager[1].GetComponent<PlayerManager>().Position > Put_back_val)
            {

                Player3Manager[1].GetComponent<Animation>().Play("Turnn");
                Player3Manager[1].GetComponent<Image>().raycastTarget = true;
                Player3Manager[1].GetComponent<PlayerManager>().IsPutBackTime = true;

            }
        }

        //===== player 4 ========

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == playerTurn + 1)
            {
                if (Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player4Manager[0].GetComponent<PlayerManager>().isFinishline &&
                    Player4Manager[0].GetComponent<PlayerManager>().Position > Put_back_val)
                {

                    Player4Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player4Manager[0].GetComponent<Image>().raycastTarget = true;
                    Player4Manager[0].GetComponent<PlayerManager>().IsPutBackTime = true;

                }
                if (Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player4Manager[1].GetComponent<PlayerManager>().isFinishline &&
                    Player4Manager[1].GetComponent<PlayerManager>().Position > Put_back_val)
                {

                    Player4Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player4Manager[1].GetComponent<Image>().raycastTarget = true;
                    Player4Manager[1].GetComponent<PlayerManager>().IsPutBackTime = true;

                }
            }
        }
        else
        {
            if (Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player4Manager[0].GetComponent<PlayerManager>().isFinishline &&
                Player4Manager[0].GetComponent<PlayerManager>().Position > Put_back_val)
            {

                Player4Manager[0].GetComponent<Animation>().Play("Turnn");
                Player4Manager[0].GetComponent<Image>().raycastTarget = true;
                Player4Manager[0].GetComponent<PlayerManager>().IsPutBackTime = true;

            }
            if (Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                !Player4Manager[1].GetComponent<PlayerManager>().isFinishline &&
                Player4Manager[1].GetComponent<PlayerManager>().Position > Put_back_val)
            {

                Player4Manager[1].GetComponent<Animation>().Play("Turnn");
                Player4Manager[1].GetComponent<Image>().raycastTarget = true;
                Player4Manager[1].GetComponent<PlayerManager>().IsPutBackTime = true;

            }
        }
    }

    public void OffPurBackBoolen()
    {
        Player1Manager[0].GetComponent<PlayerManager>().IsPutBackTime = false;
        Player1Manager[1].GetComponent<PlayerManager>().IsPutBackTime = false;

        Player2Manager[0].GetComponent<PlayerManager>().IsPutBackTime = false;
        Player2Manager[1].GetComponent<PlayerManager>().IsPutBackTime = false;

        Player3Manager[0].GetComponent<PlayerManager>().IsPutBackTime = false;
        Player3Manager[1].GetComponent<PlayerManager>().IsPutBackTime = false;

        Player4Manager[0].GetComponent<PlayerManager>().IsPutBackTime = false;
        Player4Manager[1].GetComponent<PlayerManager>().IsPutBackTime = false;


    }

    public void Put_IntoBack_pos()
    {
        int Curval;
        switch (switchPlayer_no1)
        {
            case 10:

                Curval = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Position - switch_indx1;

                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Position = Curval;
                Playarea.instt.Player1Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[Curval].transform.position;
                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().CheckBackData();
                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                if (!IsPlayerOnCardPos(Curval))
                {
                    PassTurn();
                }
                break;
            case 11:
                Curval = Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().Position - switch_indx1;

                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().Position = Curval;
                Playarea.instt.Player1Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[Curval].transform.position;
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().CheckBackData();
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                if (!IsPlayerOnCardPos(Curval))
                {
                    PassTurn();
                }
                break;
            case 20:
                Curval = Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().Position - switch_indx1;

                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().Position = Curval;
                Playarea.instt.Player2Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[Curval].transform.position;
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().CheckBackData();
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                if (!IsPlayerOnCardPos(Curval))
                {
                    PassTurn();
                }
                break;
            case 21:
                Curval = Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().Position - switch_indx1;

                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().Position = Curval;
                Playarea.instt.Player2Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[Curval].transform.position;
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().CheckBackData();
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                if (!IsPlayerOnCardPos(Curval))
                {
                    PassTurn();
                }
                break;
            case 30:
                Curval = Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().Position - switch_indx1;

                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().Position = Curval;
                Playarea.instt.Player3Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[Curval].transform.position;
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().CheckBackData();
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                if (!IsPlayerOnCardPos(Curval))
                {
                    PassTurn();
                }
                break;
            case 31:
                Curval = Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().Position - switch_indx1;

                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().Position = Curval;
                Playarea.instt.Player3Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[Curval].transform.position;
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().CheckBackData();
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                if (!IsPlayerOnCardPos(Curval))
                {
                    PassTurn();
                }
                break;
            case 40:
                Curval = Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().Position - switch_indx1;

                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().Position = Curval;
                Playarea.instt.Player4Manager[0].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[Curval].transform.position;
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().CheckBackData();
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                if (!IsPlayerOnCardPos(Curval))
                {
                    PassTurn();
                }
                break;
            case 41:
                Curval = Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().Position - switch_indx1;

                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().Position = Curval;
                Playarea.instt.Player4Manager[1].transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Waypoint[Curval].transform.position;
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().CheckBackData();
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().GetComponent<Animation>().Stop("Turnn");
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().GetComponent<Image>().raycastTarget = false;

                if (!IsPlayerOnCardPos(Curval))
                {
                    PassTurn();
                }
                break;
        }
        OffPurBackBoolen();

    }

    public static int PlayerID = -99;


    public void Check_For_Roll_other_PlayerDice()
    {
        // ======= playe 1 ========

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == (playerTurn + 1) && WhichPlayerRollAgin == 0)
            {
                if (Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player1Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player1Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player1Manager[0].GetComponent<Image>().raycastTarget = true;
                }
                if (Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player1Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player1Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player1Manager[1].GetComponent<Image>().raycastTarget = true;
                }
            }

        }
        else
        {
            if (playerTurn == 0 || WhichPlayerRollAgin == 0)
            {
                if (Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player1Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player1Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player1Manager[0].GetComponent<Image>().raycastTarget = true;
                }
                if (Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player1Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player1Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player1Manager[1].GetComponent<Image>().raycastTarget = true;
                }
            }
        }


        //========playe 2 ===============

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == (playerTurn + 1) && WhichPlayerRollAgin == 1)
            {
                if (Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player2Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player2Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player2Manager[0].GetComponent<Image>().raycastTarget = true;
                }
                if (Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player2Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player2Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player2Manager[1].GetComponent<Image>().raycastTarget = true;
                }
            }
        }
        else
        {
            if (playerTurn == 1 || WhichPlayerRollAgin == 1)
            {
                if (Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player2Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player2Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player2Manager[0].GetComponent<Image>().raycastTarget = true;
                }
                if (Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player2Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player2Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player2Manager[1].GetComponent<Image>().raycastTarget = true;
                }
            }
        }

        //========playe 3 ===============

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == (playerTurn + 1) && WhichPlayerRollAgin == 2)
            {
                if (Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player3Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player3Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player3Manager[0].GetComponent<Image>().raycastTarget = true;
                }
                if (Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player3Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player3Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player3Manager[1].GetComponent<Image>().raycastTarget = true;
                }
            }
        }
        else
        {
            if (playerTurn == 2 || WhichPlayerRollAgin == 2)
            {
                if (Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player3Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player3Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player3Manager[0].GetComponent<Image>().raycastTarget = true;
                }
                if (Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player3Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player3Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player3Manager[1].GetComponent<Image>().raycastTarget = true;
                }
            }
        }



        //========playe 4 ===============

        if (FST_Gameplay.IsMultiplayer)
        {
            if (PlayerID == (playerTurn + 1) && WhichPlayerRollAgin == 3)
            {
                if (Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player4Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player4Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player4Manager[0].GetComponent<Image>().raycastTarget = true;
                }
                if (Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player4Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player4Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player4Manager[1].GetComponent<Image>().raycastTarget = true;
                }
            }
        }
        else
        {
            if (playerTurn == 3 || WhichPlayerRollAgin == 3)
            {
                if (Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player4Manager[0].GetComponent<PlayerManager>().isFinishline)
                {

                    Player4Manager[0].GetComponent<Animation>().Play("Turnn");
                    Player4Manager[0].GetComponent<Image>().raycastTarget = true;
                }
                if (Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard &&
                    !Player4Manager[1].GetComponent<PlayerManager>().isFinishline)
                {

                    Player4Manager[1].GetComponent<Animation>().Play("Turnn");
                    Player4Manager[1].GetComponent<Image>().raycastTarget = true;
                }
            }
        }

        //=======================
    }


    public void Open_3rd_Card_skipturn()
    {
        turnskipObj.SetActive(true);
    }

    public void Close_3rd_Card_skipturn(int val)
    {
        turnskipObj.SetActive(false);
        if (FST_Gameplay.IsMultiplayer)
        {
            WhichPlayer_Turn_Skip = val;
            Open_Card_Popup("player  " + (val + 1) + " will skip his turn one time");
            Multi_Manager.inst.CardPopUp("Player" + (val + 1) + " turn will be skipped by player  " + (playerTurn + 1));
            Multi_Manager.inst.Card3Multi_skip(val);
        }
        else
        {
            WhichPlayer_Turn_Skip = val;
            Open_Card_Popup("player " + (playerTurn + 1) + " skip player " + (val + 1) + " turn one time");
        }
    }
    //------------------------------------------------------------------------------------------- New Function Created By Vishal
    public void Close_3rd_Card_skipturn()
    {
        turnskipObj.SetActive(false);
    }
    //--------------------------------------------------------------------------------------------------- Finish This Function
    public void Open_4th_Card_RollAgin()
    {
        RollAginObj.SetActive(true);
    }

    public void Close_4th_Card_RollAgin(int val)
    {
        RollAginObj.SetActive(false);
        if (FST_Gameplay.IsMultiplayer)
        {
            WhichPlayerRollAgin = val;
            Open_Card_Popup("Player" + (playerTurn + 1) + " taking player  " + (val + 1) + " turn...");
            Multi_Manager.inst.CardPopUp("Player" + (playerTurn + 1) + " taking player  " + (val + 1) + " turn...");

        }
        else
        {
            WhichPlayerRollAgin = val;
            Open_Card_Popup("Player" + (playerTurn + 1) + " taking player  " + (val + 1) + " turn...");
        }
    }

    public void GenerateRandomVal_Card()
    {

        if (FST_Gameplay.IsMultiplayer)
        {
            if (FST_Gameplay.IsMaster)
            {
                RandomValForCar = Random.Range(1, 5);
                Multi_Manager.inst.CardValueSet_multi(RandomValForCar);
            }

        }
        else
        {
            RandomValForCar = Random.Range(1, 5);
        }
    }

    public int CheckNoOFPlayerINSameBox(int indx)
    {
        int v = 0;

        //==========  Player 1  ===============
        if (Player1Manager[0].GetComponent<PlayerManager>().Position == indx)
        {
            v++;
        }
        if (Player1Manager[1].GetComponent<PlayerManager>().Position == indx)
        {
            v++;
        }
        //==========  Player 2  ===============
        if (Player2Manager[0].GetComponent<PlayerManager>().Position == indx)
        {
            v++;
        }
        if (Player2Manager[1].GetComponent<PlayerManager>().Position == indx)
        {
            v++;
        }
        //==========  Player 3  ===============
        if (Player3Manager[0].GetComponent<PlayerManager>().Position == indx)
        {
            v++;
        }
        if (Player3Manager[1].GetComponent<PlayerManager>().Position == indx)
        {
            v++;
        }

        //==========  Player 4  ===============

        if (Player4Manager[0].GetComponent<PlayerManager>().Position == indx)
        {
            v++;
        }
        if (Player4Manager[1].GetComponent<PlayerManager>().Position == indx)
        {
            v++;
        }

        return v;
    }
    bool IsMyTurn { get {
            return //(int)PhotonNetwork.LocalPlayer.CustomProperties["save_indx"] == playerTurn
                   PlayerID - 1 == playerTurn
                     || !FST_Gameplay.IsMultiplayer; } }//could use actor numbers here, should really be pun id
    private void OnlineTurn()
    {
        Debug.Log("my turn = " + IsMyTurn);
        //    if (!FST_Gameplay.IsMaster/*PunTurnManager.IsFinishedByMe*/)
        //    return;
        playerTurn++;
        if (playerTurn >= PhotonNetwork.CurrentRoom.PlayerCount)
            playerTurn = 0;
        Debug.Log("OnlineTurn(), result = " + playerTurn);
        Multi_Manager.inst.Multiplayer_turn_pass(playerTurn);

        Debug.Log("my turn = " + IsMyTurn);
    }

    public void ReceiveTurn(int turn)
    {
        playerTurn = turn;

        Debug.Log("ReceiveTurn(), result = " + turn);
        Close_All_Animation();
        Check_turn();
        StartCoroutine(HighlightTurn());
        Dicebt.enabled = true;
    }
    //========================================================
}
