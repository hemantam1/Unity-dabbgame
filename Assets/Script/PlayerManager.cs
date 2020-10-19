using SaveConstantVal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public Vector3 savePinVal;
    public Button Dicebt;
    public int Position;
    public Transform[] Waypoint;
    public GameObject[] LadderAnimation;

    PlayerManager playermanager;

    public int PlayerIndex;
    public int Pno;

    public bool PlayerOnGameBoard;
    public bool isFinishline, IsSwitchPositionTime, IsPutBackTime;


    public void SetControllerReference(PlayerManager pm)
    {
        playermanager = pm;
    }

    void Start()
    {
        Position = 0;
        savePinVal = transform.position;
    }


    void Update()
    {

    }

    public void Move_Click()
    {
        print("asdjalksjdlkajskljdaklsjdklj");
        //		StopCoroutine (Playarea.instt.he ());

        StopCoroutine(Playarea.instt.a);
        Playarea.instt.StopCorutineFunction();
        //		StopAllCoroutines ();
        Playarea.instt.Anim.enabled = false;
        Playarea.instt.CountDown_Image.SetActive(false);
        Playarea.instt.abc = false;
        Playarea.instt.CancelInvoke("Player1_AI");
        Playarea.instt.CancelInvoke("Player2_AI");
        Playarea.instt.CancelInvoke("Player3_AI");
        Playarea.instt.CancelInvoke("Player4_AI");

        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
        {
            if (IsPutBackTime)
            {
                Playarea.instt.switchPlayer_no1 = Pno;
                Playarea.instt.Put_IntoBack_pos();
                Multi_Manager.inst.putBack_poss_multi(Playarea.instt.switchPlayer_no1, Playarea.instt.switch_indx1);


            }
            else if (IsSwitchPositionTime)
            {
                Playarea.instt.switchPlayer_no2 = Pno;
                Playarea.instt.switch_indx2 = Position;
                Playarea.instt.Exchange_pos();
                Multi_Manager.inst.Exchange_pos_multi(Playarea.instt.switchPlayer_no1, Playarea.instt.switch_indx1, Playarea.instt.switchPlayer_no2, Playarea.instt.switch_indx2);
            }
            else
            {

                Multi_Manager.inst.Multiplayer_Move_player(Pno, Playarea.instt.DiceValIS);
            }

        }
        else
        {
            if (IsPutBackTime)
            {
                Playarea.instt.switchPlayer_no1 = Pno;
                Playarea.instt.Put_IntoBack_pos();

            }
            else if (IsSwitchPositionTime)
            {
                Playarea.instt.switchPlayer_no2 = Pno;
                Playarea.instt.switch_indx2 = Position;
                Playarea.instt.Exchange_pos();

            }
            else
            {
                Playarea.instt.IsRepetePutBackCheck = true;
                Playarea.instt.Close_All_Animation();
                for (int i = 0; i < 4; i++)
                {
                    Playarea.instt.Skip_BT[i].SetActive(false);
                }
                if (PlayerOnGameBoard && Playarea.instt.DiceValIS >= 0 && !isFinishline)
                {

                    MoveObj(Playarea.instt.DiceValIS + 1);
                    Playarea.instt.DiceValIS = 0;
                }
                else if (!PlayerOnGameBoard && Playarea.instt.DiceValIS == 5 && !isFinishline)
                {
                    Dicebt.enabled = true;
                    PlayerOnGameBoard = true;
                    Position = 1;
                    transform.position = Waypoint[1].transform.position;
                    Playarea.instt.DiceValIS = 0;
                    //					Soundmanager.instance.Kill_Opponent ();
                    GetComponent<Animation>().Stop("Turnn");
                    GetComponent<Image>().raycastTarget = false;

                    Playarea.instt.PassTurn();
                }
            }
        }
    }

    public void multiplayer_all_palyerMove()
    {
        //		Debug.Log ("p11");
        Playarea.instt.IsRepetePutBackCheck = true;
        Playarea.instt.Close_All_Animation();
        for (int i = 0; i < 4; i++)
        {
            Playarea.instt.Skip_BT[i].SetActive(false);
        }

        if (PlayerOnGameBoard && Playarea.instt.DiceValIS >= 0 && !isFinishline)
        {

            MoveObj(Playarea.instt.DiceValIS + 1);
            Playarea.instt.DiceValIS = 0;

        }
        else if (!PlayerOnGameBoard && Playarea.instt.DiceValIS == 5 && !isFinishline)
        {
            Dicebt.enabled = true;
            PlayerOnGameBoard = true;
            Position = 1;
            transform.position = Waypoint[1].transform.position;
            Playarea.instt.DiceValIS = 0;
            GetComponent<Animation>().Stop("Turnn");
            GetComponent<Image>().raycastTarget = false;
            Playarea.instt.PassTurn();

        }
    }


    public void MoveObj(int val)
    {
        //		Debug.Log ("p22");
        int remainVal = 100 - Position;

        if (remainVal >= val)
        {
            StartCoroutine(StartMovingObj(val));
        }
        else
        {

            Playarea.instt.PassTurn();
        }
    }

    public void CheckBackData()
    {
        StartCoroutine(StartMovingObj(0));
    }

    public void CheckAttackZone()
    {
        //================== player 1 =================

        if (PlayerIndex != Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().PlayerIndex &&
            PlayerIndex != Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().PlayerIndex)
        {

            if (!Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().isFinishline &&
                Position == Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Position)
            {

                Playarea.instt.Player1Manager[0].gameObject.transform.position = Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().savePinVal;
                Soundmanager.instance.Kill_Opponent();
                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Position = 0;
                Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard = false;
                Playarea.instt.isSamePlayerTurn = true;

            }
            if (!Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().isFinishline &&
                Position == Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().Position)
            {

                Playarea.instt.Player1Manager[1].gameObject.transform.position = Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().savePinVal;
                Soundmanager.instance.Kill_Opponent();
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().Position = 0;
                Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard = false;
                Playarea.instt.isSamePlayerTurn = true;

            }
        }
        else
        {
            SetPositionAfterMove();
        }
        //================== player 2 =================

        if (PlayerIndex != Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().PlayerIndex &&
            PlayerIndex != Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().PlayerIndex)
        {

            if (!Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().isFinishline &&
                Position == Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().Position)
            {

                Playarea.instt.Player2Manager[0].gameObject.transform.position = Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().savePinVal;
                Soundmanager.instance.Kill_Opponent();
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().Position = 0;
                Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard = false;
                Playarea.instt.isSamePlayerTurn = true;
            }
            if (!Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().isFinishline &&
                Position == Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().Position)
            {

                Playarea.instt.Player2Manager[1].gameObject.transform.position = Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().savePinVal;
                Soundmanager.instance.Kill_Opponent();
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().Position = 0;
                Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard = false;
                Soundmanager.instance.Kill_Opponent();
                Playarea.instt.isSamePlayerTurn = true;
            }
        }
        else
        {
            SetPositionAfterMove();
        }
        //================== player 3 =================

        if (PlayerIndex != Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().PlayerIndex &&
            PlayerIndex != Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().PlayerIndex)
        {

            if (!Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().isFinishline &&
                Position == Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().Position)
            {

                Playarea.instt.Player3Manager[0].gameObject.transform.position = Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().savePinVal;
                Soundmanager.instance.Kill_Opponent();
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().Position = 0;
                Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard = false;
                Playarea.instt.isSamePlayerTurn = true;

            }
            if (!Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().isFinishline &&
                Position == Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().Position)
            {
                Playarea.instt.Player3Manager[1].gameObject.transform.position = Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().savePinVal;
                Soundmanager.instance.Kill_Opponent();
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().Position = 0;
                Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard = false;
                Playarea.instt.isSamePlayerTurn = true;
            }
        }
        else
        {
            SetPositionAfterMove();
        }
        //================== player 4 =================

        if (PlayerIndex != Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().PlayerIndex &&
            PlayerIndex != Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().PlayerIndex)
        {

            if (!Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().isFinishline &&
                Position == Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().Position)
            {

                Playarea.instt.Player4Manager[0].gameObject.transform.position = Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().savePinVal;
                Soundmanager.instance.Kill_Opponent();
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().Position = 0;
                Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().PlayerOnGameBoard = false;
                Playarea.instt.isSamePlayerTurn = true;
            }

            if (!Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().isFinishline &&
                Position == Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().Position)
            {

                Playarea.instt.Player4Manager[1].gameObject.transform.position = Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().savePinVal;
                Soundmanager.instance.Kill_Opponent();
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().Position = 0;
                Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().PlayerOnGameBoard = false;
                Playarea.instt.isSamePlayerTurn = true;
            }
        }
        else
        {
            SetPositionAfterMove();
        }
        //===================================
    }

    IEnumerator StartMovingObj(int Val)
    {
        //		Debug.Log ("p333");
        if (Val != 0)
        {
            Val--;
            Position++;
            GetComponent<Animation>().Stop("jump");
            GetComponent<Animation>().Play("jump");

            while (transform.position != Waypoint[Position].transform.position)
            {
                //				Debug.Log ("p44");
                transform.position = Vector3.MoveTowards(transform.position, Waypoint[Position].transform.position, Time.deltaTime * 4f);
                yield return new WaitForEndOfFrame();
            }
            Soundmanager.instance.Play_Jump();
            StartCoroutine(StartMovingObj(Val));
        }
        else
        {
            Debug.LogError("PlayerIndex " + PlayerIndex);
            switch (Position)
            {
               
                case 3:
                    //============================
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[0].GetComponent<ActivePlayer>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[0].GetComponent<ActivePlayer>().playerNO = Pno;
                    LadderAnimation[0].SetActive(true);
                    Position = 21;
                    transform.position = Waypoint[Position].transform.position;
                    //StopCoroutine (StartMovingObj (0));
                    //============================
                    break;

                case 90:
                    //============================
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[6].GetComponent<ActivePlayer>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[6].GetComponent<ActivePlayer>().playerNO = Pno;
                    LadderAnimation[6].SetActive(true);
                    Position = 91;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                case 8:
                    //============================
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[1].GetComponent<ActivePlayer>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[1].GetComponent<ActivePlayer>().playerNO = Pno;
                    LadderAnimation[1].SetActive(true);
                    Position = 30;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                case 28:
                    //============================
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[2].GetComponent<ActivePlayer>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[2].GetComponent<ActivePlayer>().playerNO = Pno;
                    LadderAnimation[2].SetActive(true);
                    Position = 84;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                case 58:
                    //============================
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[3].GetComponent<ActivePlayer>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[3].GetComponent<ActivePlayer>().playerNO = Pno;
                    LadderAnimation[3].SetActive(true);
                    Position = 77;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                case 75:
                    //============================
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[4].GetComponent<ActivePlayer>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[4].GetComponent<ActivePlayer>().playerNO = Pno;
                    LadderAnimation[4].SetActive(true);
                    Position = 86;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                case 80:
                    //============================
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[5].GetComponent<ActivePlayer>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[5].GetComponent<ActivePlayer>().playerNO = Pno;
                    LadderAnimation[5].SetActive(true);
                    Playarea.instt.isSamePlayerTurn = true;
                    Position = 100;
                    isFinishline = true;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                //  snakeeeeeeeee
                case 17:
                    //============================
                    Soundmanager.instance.Play_SnakeBitsound();
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[7].GetComponent<Active_snake>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[7].GetComponent<Active_snake>().playerNO = Pno;
                    LadderAnimation[7].SetActive(true);
                    Position = 13;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                case 52:
                    //============================
                    Soundmanager.instance.Play_SnakeBitsound();
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[8].GetComponent<Active_snake>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[8].GetComponent<Active_snake>().playerNO = Pno;
                    LadderAnimation[8].SetActive(true);
                    Position = 29;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                case 57:
                    //============================
                    Soundmanager.instance.Play_SnakeBitsound();
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[9].GetComponent<Active_snake>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[9].GetComponent<Active_snake>().playerNO = Pno;
                    LadderAnimation[9].SetActive(true);
                    Position = 40;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================

                    break;

                case 62:
                    //============================
                    Soundmanager.instance.Play_SnakeBitsound();
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[10].GetComponent<Active_snake>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[10].GetComponent<Active_snake>().playerNO = Pno;
                    LadderAnimation[10].SetActive(true);
                    Position = 22;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                case 88:
                    Soundmanager.instance.Play_SnakeBitsound();
                    //============================
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[11].GetComponent<Active_snake>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[11].GetComponent<Active_snake>().playerNO = Pno;
                    LadderAnimation[11].SetActive(true);
                    Position = 18;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));
                    //============================
                    break;

                case 97:
                    //============================
                    Soundmanager.instance.Play_SnakeBitsound();
                    GetComponent<Image>().enabled = false;
                    LadderAnimation[12].GetComponent<Active_snake>().CurrentPlayerIS = PlayerIndex;
                    LadderAnimation[12].GetComponent<Active_snake>().playerNO = Pno;
                    LadderAnimation[12].SetActive(true);
                    Position = 79;
                    transform.position = Waypoint[Position].transform.position;
                    StopCoroutine(StartMovingObj(0));

                    //============================
                    break;

                default:
                    Dicebt.enabled = true;
                    if (!Playarea.instt.IsPlayerOnSafeZone(Position))
                    {
                        CheckAttackZone();
                    }
                    else
                    {
                        SetPositionAfterMove();
                        Soundmanager.instance.Play_SafeZone();
                    }

                    if (Position == 100)
                    {
                        Playarea.instt.isSamePlayerTurn = true;
                        isFinishline = true;
                        Soundmanager.instance.Play_playerReach100();
                    }

                    //  card check 
                    if (Playarea.instt.IsPlayerOnCardPos(Position) && Playarea.instt.IsRepetePutBackCheck)
                    {
                        Playarea.instt.IsRepetePutBackCheck = false;
                        Check_Random_QuestionMark();
                        Soundmanager.instance.Play_SafeZone();
                    }
                    else
                    {

                        Playarea.instt.PassTurn();
                    }

                    //				Playarea.instt.PassTurn ();
                    break;
            }


            Check_Winner();
        }
    }

    void DelayOnCloasePlayerImg()
    {
        GetComponent<Image>().enabled = false;
    }

    void Check_Winner()
    {
        int idd = 0;
        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
        {
            string pid = "1";
            //string pid = (string)PhotonNetwork.player.CustomProperties["save_indx"];
            idd = int.Parse(pid);
        }

        //===== player 1 win =========

        if (Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().Position == 100 &&
            Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().Position == 100)
        {

            UIManager.instance.gameManager.gameWinnerIS.text = "" + Playarea.instt.playarea_text[0].text + "  win";
            UIManager.instance.gameManager.gameWin.SetActive(true);
            Soundmanager.instance.Play_LevelComplete();

            if (idd == 1)
            {
                PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) + /*ConnectAndJoinRandom.instt.BidAmout **/ 2);
                PlayerPrefs.SetInt(ApiConstant.No_of_time_win, PlayerPrefs.GetInt(ApiConstant.No_of_time_win) + 1);
            }

        }

        //===== player 2 win =========

        if (Playarea.instt.Player2Manager[0].GetComponent<PlayerManager>().Position == 100 &&
            Playarea.instt.Player2Manager[1].GetComponent<PlayerManager>().Position == 100)
        {

            UIManager.instance.gameManager.gameWinnerIS.text = "" + Playarea.instt.playarea_text[1].text + "  win";
            UIManager.instance.gameManager.gameWin.SetActive(true);
            Soundmanager.instance.Play_LevelComplete();
            if (idd == 2)
            {
                PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) + /*ConnectAndJoinRandom.instt.BidAmout **/ 2);
                PlayerPrefs.SetInt(ApiConstant.No_of_time_win, PlayerPrefs.GetInt(ApiConstant.No_of_time_win) + 1);
            }

        }

        //===== player 3 win =========

        if (Playarea.instt.Player3Manager[0].GetComponent<PlayerManager>().Position == 100 &&
            Playarea.instt.Player3Manager[1].GetComponent<PlayerManager>().Position == 100)
        {
            UIManager.instance.gameManager.gameWinnerIS.text = "" + Playarea.instt.playarea_text[2].text + "  win";
            UIManager.instance.gameManager.gameWin.SetActive(true);
            Soundmanager.instance.Play_LevelComplete();

            if (idd == 3)
            {
                PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) + /*ConnectAndJoinRandom.instt.BidAmout **/ 2);
                PlayerPrefs.SetInt(ApiConstant.No_of_time_win, PlayerPrefs.GetInt(ApiConstant.No_of_time_win) + 1);
            }

        }


        //===== player 4 win =========

        if (Playarea.instt.Player4Manager[0].GetComponent<PlayerManager>().Position == 100 &&
            Playarea.instt.Player4Manager[1].GetComponent<PlayerManager>().Position == 100)
        {

            UIManager.instance.gameManager.gameWinnerIS.text = "" + Playarea.instt.playarea_text[3].text + "  win";
            UIManager.instance.gameManager.gameWin.SetActive(true);
            Soundmanager.instance.Play_LevelComplete();
            if (idd == 4)
            {
                PlayerPrefs.SetInt(ApiConstant.TotalGold, PlayerPrefs.GetInt(ApiConstant.TotalGold) + /*ConnectAndJoinRandom.instt.BidAmout **/ 2);
                PlayerPrefs.SetInt(ApiConstant.No_of_time_win, PlayerPrefs.GetInt(ApiConstant.No_of_time_win) + 1);
            }
        }

        //=============================
    }

    //==========  open card check   ===================

    public void Check_Random_QuestionMark()
    {
        string PlayerNo;
        int indx = 0;
        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
        {
            //PlayerNo = (string)PhotonNetwork.player.CustomProperties["save_indx"];
            PlayerNo = "1";
            indx = int.Parse(PlayerNo);
        }

        //		switch (4) {
        switch (Playarea.instt.RandomValForCar)
        {
            case 1:

                //=============  1  ====== switch pos ============ 

                Playarea.instt.switchPlayer_no1 = Pno;
                Playarea.instt.switch_indx1 = Position;

                if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
                {

                    if (PlayerIndex == indx)
                    {
                        Playarea.instt.Open_Card_Popup("Select any player and switch your position .....");
                        Multi_Manager.inst.CardPopUp("player " + PlayerIndex + "  will switch his position with any other player . is that you ? please close the popup menu and check..");
                    }
                }
                else
                {
                    Playarea.instt.Open_Card_Popup("Select any player and switch your position .....");
                }
                Playarea.instt.Check_For_swich_posss();

                //===============================
                break;

            case 2:
                //===============  2  ========= put back =======
                int putBackk = UnityEngine.Random.Range(1, 4);
                switch (putBackk)
                {
                    case 1:
                        putBackk = 3;
                        break;

                    case 2:
                        putBackk = 5;
                        break;

                    case 3:
                        putBackk = 7;
                        break;
                }
                Playarea.instt.switch_indx1 = putBackk;
                if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
                {

                    if (indx == Playarea.instt.playerTurn + 1)
                    {
                        Playarea.instt.Open_Card_Popup("Select any player and put back  " + putBackk + "  step");
                        Multi_Manager.inst.CardPopUp("player " + PlayerIndex + " can put back of any player  " + putBackk + " step . Is that you ? please close the popup menu and check..");
                    }

                }
                else
                {
                    Playarea.instt.Open_Card_Popup("Select any player and put back  " + putBackk + "  step");
                }
                Playarea.instt.PutBackOpen(putBackk);

                //===============================
                break;

            case 3:

                //============= 3  ===== skip player turn =============
                if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
                {

                    if (PlayerIndex == indx)
                    {
                        Playarea.instt.Is_3rd_Card_SkipTurnActive = true;

                        //==========
                        for (int i = 0; i < 4; i++)
                        {
                            Playarea.instt.Skip_CardBT[i].SetActive(false);
                        }
                        print("IF For Loop Called And Player Is " + Playarea.instt.num_of_Player_is);
                        for (int i = 0; i <= Playarea.instt.num_of_Player_is; i++)
                        {
                            print("slkdjfksjdklfjslkj");
                            if (i != Playarea.instt.playerTurn)
                            {
                                Playarea.instt.Skip_CardBT[i].SetActive(true);
                            }
                        }
                        //===========
                        Playarea.instt.Open_3rd_Card_skipturn();
                    }
                }
                else
                {
                    Playarea.instt.Is_3rd_Card_SkipTurnActive = true;
                    //==========
                    for (int i = 0; i < 4; i++)
                    {
                        Playarea.instt.Skip_CardBT[i].SetActive(false);
                    }
                    for (int i = 0; i <= Playarea.instt.num_of_Player_is; i++)
                    {
                        //----------------------------------------------------------------------------------------Vishal Created Code
                        if (i == 0)
                        { // for Only Single Player
                            Playarea.instt.Skip_CardBT[i].SetActive(true);
                        }
                        //------------------------------------------------------------------------------------------End Of Code
                        if (i != Playarea.instt.playerTurn)
                        {
                            Playarea.instt.Skip_CardBT[i].SetActive(true);
                        }
                    }
                    //===========
                    Playarea.instt.Open_3rd_Card_skipturn();
                }
                //===============================
                break;

            case 4:
                //================ 4 ======== roll agin =======

                if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
                {

                    if (PlayerIndex == indx)
                    {
                        Playarea.instt.Is_4th_RollTime = true;

                        //==========
                        for (int i = 0; i < 4; i++)
                        {
                            Playarea.instt.GivemoreChance_CardBT[i].SetActive(false);
                        }
                        for (int i = 0; i <= Playarea.instt.num_of_Player_is; i++)
                        {
                            Playarea.instt.GivemoreChance_CardBT[i].SetActive(true);

                        }
                        //===========

                        Playarea.instt.Open_4th_Card_RollAgin();
                    }
                }
                else
                {
                    Playarea.instt.Is_4th_RollTime = true;
                    //==========
                    for (int i = 0; i < 4; i++)
                    {
                        Playarea.instt.GivemoreChance_CardBT[i].SetActive(false);
                    }
                    for (int i = 0; i <= Playarea.instt.num_of_Player_is; i++)
                    {
                        Playarea.instt.GivemoreChance_CardBT[i].SetActive(true);

                    }
                    //===========
                    Playarea.instt.Open_4th_Card_RollAgin();
                }
                //===============================
                break;
        }
        //=======
        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
        {
            Playarea.instt.GenerateRandomVal_Card();
        }
        else
        {
            Playarea.instt.GenerateRandomVal_Card();
        }
        //=======

    }

    void SetPositionAfterMove()
    {
        int val = Playarea.instt.CheckNoOFPlayerINSameBox(Position);
        float xx, yy;
        //		Debug.Log ("call = " + val);
        switch (val)
        {

            case 2:
                //			Debug.Log ("two");
                xx = transform.localPosition.x - 40;
                transform.localPosition = new Vector2(xx, transform.localPosition.y);
                break;
            case 3:
                //			Debug.Log ("three");
                yy = transform.localPosition.y - 25;
                transform.localPosition = new Vector2(transform.localPosition.x, yy);
                break;
            case 4:
                //			Debug.Log ("four");
                xx = transform.localPosition.x - 40;
                yy = transform.localPosition.y - 25;
                transform.localPosition = new Vector2(xx, yy);
                break;
            case 5:

                break;

        }
    }

    //=============================================
}
